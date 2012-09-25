var Worker = (function ($, global, undefined) {
    var $progress = $("#progress");
    var $history = $('#history');

    var percentage = 0;

    $(window).resize(function () {
        var max_width = $('window').width();
        percentage = max_width / 100;
        // TODO: resize the progress using the new value for percent
    });

    var sleepingTask = {
        name: "Sleeping",
        work: function (handler) {
            var parent = this;
            handler.progress.maxSteps = 10;
            var currentStep = 1;
            setTimeout(function () {
                currentStep++;
                handler.progress.stepCompleted(currentStep);
                if (currentStep < handler.progress.maxSteps) {
                    parent.work(handler);
                }
            }, 3000);
        }
    };

    return function() {
        return {
            tasks: {
                list: [],
                registerTask: function (task) {
                    this.list.push(task);
                },
                next: function () {
                    return this.list.shift();
                }
            },
            history: {
                el: $history,
                add: function (message) {
                    this.el.append($("<li />", { text: message }));

                    $('#currentTask').html(message);
                }
            },
            progress: {
                el: $progress,
                maxSteps: 0,
                reset: function () {
                    this.el.width(0);
                },
                stepCompleted: function ( step ) {
                    this.el.width(percentage * step);
                }
            },
            work: function () {
                var task = this.tasks.next();
                if (task) {
                    var what = task.name;
                    var parent = this;

                    console.log(task);

                    // set progress on current task to 0
                    parent.progress.reset();

                    // set the current task desc into history
                    parent.history.add(what);

                    // start the task
                    task.work(this);

                    parent.history.add("Sleeping");
                }
                // when the task is done, wait and start again
                setTimeout(function () {
                    parent.work(progress, history);
                }, 1000);
            },
            start: function () {
                this.work();
            }
        };
    };
})(jQuery, window);

var worker = new Worker();