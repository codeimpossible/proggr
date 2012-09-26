/*
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

proggr client-side worker library
(c) Jared Barboza, 2012
Manages and executes a queue of tasks in a client web browser.

=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

## Basic workflow for a Worker
* Start Worker
  * Worker checks task list
    * if no tasks, check with server
      * if no tasks, sleep
    * if tasks, get first and start it
      * each step of the task is a call to "work"
      * each step should report a status (pass/fail)
        * if success, the step will be marked as completed successfully
        * if fail, the work will stop for the current task and it will be pushed into the failed tasks list to be retried when there is nothing to do.
*/

// Task: a task to run within the worker.
// @name = the friendly name of the task, will be shown in the UI
// @fn = the actual work to be performed when the task is executed
var Task = {
    makeTask: function (name, fn) {
        fn.title = name;
        return fn;
    }
};

// Worker: represents a client-side worker drone
// @$ = scoped reference to jQuery
// @global = scoped reference to window
// @undefined = scoped reference to undefined
var Worker = (function ($, global, undefined) {
    var percentage = 0;

    $(global).resize(function () {
        var max_width = $(this).width();
        percentage = max_width / 100;
        // TODO: resize the progress using the new value for percent
    });

    var ux = new (function () {
        this.setCurrentTask = function (task) {
            $('#currentTask').html(task.title);
        };
    })();

    // return the _real_ constructor
    // @worker_id = the GUID that represents this worker in proggr, used to authenticate a worker against the server
    // @_ux = a reference to a UX object. if null or undefined then the ux var above will be substituted. This is really only used by our unit tests.
    return function Worker(worker_id, _ux) {
        var id = worker_id, worker_ux = _ux || ux;

        // each worker should have its own queue
        var queue = new (function (undefined) {
            var _q = [], _size = 0;

            this.push = function (task) {
                _q.push(task);
                _size++;
                return task;
            };

            this.next = function () {
                _size--;
                return _q.shift();
            };

            this.peek = function () {
                return _q[0] || undefined;
            };

            this.getLength = function () {
                return _size;
            };
        })();

        var log = function () {
            if (console && console.log) console.log.call(console, arguments);
        };

        return {
            failed_tasks: [],
            enqueue: function( task ) {
                queue.push(task);
            },

            queueSize: function() {
                return queue.getLength();
            },

            // loops through
            nextTask: function () {
                var parent = this;
                if (!queue.peek()) {
                    // TODO: get another task for us to run

                    // wait 10 seconds and then check the tasks list
                    worker_ux.setCurrentTask("No work, sleeping...");
                    setTimeout(parent.nextTask, 10 * 1000);
                    return;
                }

                var task = queue.next();

                worker_ux.setCurrentTask(task);

                setTimeout(function runner() {
                    if (task(ux)) {
                        parent.taskCompleted(task);
                    } else {
                        parent.taskFailed(task);
                    }
                }, 500);

                return task;
            },

            taskCompleted: function (task) {
                // TODO: add the task to the history...
                this.nextTask();
            },

            taskFailed: function (task) {
                this.failed_tasks.push(task);
                // TODO: add the task to the history...?
                // TODO: add code to show failed tasks?

                // launch the next task
                this.nextTask();
            }
        };
    };
})(jQuery, window)
