/*
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

var Task = (function ($, global, undefined) {
    return function Task(name, fn) {
        this.makeTask = function( name, fn ) {
            var task = fn;
            task.name = name;
            return task;
        };
    };
})(jQuery, window);

var Worker = (function ($, global, undefined) {
    var percentage = 0;

    $(global).resize(function () {
        var max_width = $(this).width();
        percentage = max_width / 100;
        // TODO: resize the progress using the new value for percent
    });

    var queue = new (function(){
        var _q = [];

        this.push = function(task) {
            _q.push(task);
        };

        this.next = function() {
            _q.shift();
        };

        this.peek = function() {
            return _q[0] || undefined;
        }
    })();

    var ux = new (function () {
        this.setCurrentTask = function (task) {
            $('#currentTask').html(task.name);
        };
    })();

    // return the constructor
    return function Worker() {
        return {
            failed_tasks: [],
            enqueue: function( task ) {
                queue.push(task);
            },
            
            // loops through
            loop: function() {
                var work = function () {
                    var task = queue.next();

                    ux.setCurrentTask(task);

                    if (task(ux)) {
                        this.taskCompleted(task);
                    } else {
                        this.taskFailed(task);
                    }
                };
            },

            taskCompleted: function(task) {

            },

            taskFailed: function(task) {
                failed_tasks.push(task);

                // TODO: add code to show failed tasks?
            }
        };
    };
})(jQuery, window)
