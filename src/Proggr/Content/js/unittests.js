(function ($, worker) {
    var id = "proggr";
    var isIE = (/msie/i.test(navigator.userAgent) && !/opera/i.test(navigator.userAgent));
    var fx = {
        shouldBeTrue: function (expr, msg) {
            if (!expr) {
                throw { wassuccess: expr, message: msg || null };
            }
        },

        shouldBeFalse: function (expr, msg) {
            if (expr) {
                throw { wassuccess: !expr, message: msg || null };
            }
        },

        shouldBeEqual: function (a, b, msg) {
            if (a != b) {
                throw { wassuccess: false, message: msg || null };
            }
        },

        shouldBeSame: function (a, b, msg) {
            if (a !== b) {
                throw { wassuccess: false, message: msg || null };
            }
        },

        shouldThrowError: function (expr, msg) {
            try {
                fn();
                throw { wassuccess: false, message: msg || null };
            } catch (e) {

            }
        }
    };

    var goodStyle = {
        position: "absolute",
        top: "0",
        left: "0",
        width: "10px",
        height: "10px",
        'border-right': "1px solid #A2CD5A",
        'border-bottom': "1px solid #6E8B3D",
        'line-height': "20px",
        background: "#BCEE68",
        padding: "0",
        margin: "1px"
    };

    var badStyle = {
        position: "absolute",
        top: "0",
        left: "0",
        width: "10px",
        height: "10px",
        'border-right': "1px solid #CD0000",
        'border-bottom': "1px solid #8B0000",
        'line-height': "20px",
        background: "#CD3333",
        padding: "0",
        margin: "1px"
    };

    var halp = {
        setStyle: function (e, styleObj) {
            var styleAttr = "";
            for (var key in styleObj) {
                if (styleObj.hasOwnProperty(key)) {
                    styleAttr += key + ":" + styleObj[key] + ";";
                }
            }
            halp.attr(e, 'style', styleAttr);
        },
        attr: function (e, name, val) {
            e.setAttribute(name, val);
        },
        ev: function (e, name, _fn) {
            var handler = window.addEventListener || window.attachEvent;
            var prefix = isIE ? "on" : "";
            handler(prefix + name, _fn, isIE);
        }
    };

    worker.tasks.registerTask("running unit tests", {
        tests:{
            "registering a task adds it to the task list": function () {
                var testWorker = new Worker();
                testWorker.tasks.registerTask("test task", {});

                this.assertEqual(1, testWorker.tasks.list.length);
                this.assertEqual("test task", testWorker.tasks.list[0].name);
            },
            "next returns the first task from the list": function () {
                var testWorker = new Worker();

                testWorker.tasks.registerTask("test task", {});
                testWorker.tasks.registerTask("another test task", {});

                this.assertEqual("test task", testWorker.tasks.next().name);
            }
        },
        work: function (workerHandler) {
            var testsToRun = [];

            for (var key in this.tests) {
                if (obj.hasOwnProperty(key)) {
                    testsToRun.push({ name: key, fn: obj[key] });
                }
            }

            workerHandler.progress.maxSteps = testsToRun.length;

            for (var i = -1, l = testsToRun.length; ++i < l;) {
                var testName = testsToRun[i].name;
                var result = { wassuccess: true, msg: '' };
                try {
                    testsToRun[i].fn.call(fx);
                } catch (e) {
                    result = e;
                }

                workerHandler.progress.stepCompleted(i+1);
            }
        }
    });
})(jQuery, worker);