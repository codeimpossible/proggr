var Worker = (function ($, global, undefined) {
    var $progress = $("#progress");
    var $history = $('#history');

    var percentage = 0;

    $(window).resize(function () {
        var max_width = $('window').width();
        percentage = max_width / 100;
        // TODO: resize the progress using the new value for percent
    });

    return function() {
        return {
            tasks: {
                list: [],
                registerTask: function (name, task) {
                    this.list.push({ name: name, task: task });
                },
                next: function () {
                    return this.list.shift();
                }
            },
            history: {
                el: $history,
                add: function (message) {
                    this.el.append($("<li />", { text: message }));
                }
            },
            progress: {
                el: $progress,
                maxSteps: 0,
                stepCompleted: function ( step ) {
                    this.el.width(percentage * step);
                }
            },
            work: function () {
                var task = this.tasks.next();
                if (task) {
                    var what = task.name;
                    var work = task.work;
                    var parent = this;

                    // set progress on current task to 0
                    progress.report(0);

                    // set the current task desc into history
                    history.append($("<li />", { text: what }));

                    // start the task
                    work(this);

                    history.append($("<li />", { text: "Sleeping" }));
                }
                // when the task is done, wait and start again
                setTimeout(function () {
                    parent.work(progress, history);
                }, 1000);
            },
            start: function () {
                this.work($progress, $history);
            }
        };
    };
})(jQuery, window);

var worker = new Worker();