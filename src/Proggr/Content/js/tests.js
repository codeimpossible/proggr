var tests = (function () {
    var should = { // all our assertions, YO!
        beTrue: function (expr, msg) { if (!expr) { throw { wassuccess: expr, message: msg || null }; } },
        beFalse: function (expr, msg) { if (expr) { throw { wassuccess: !expr, message: msg || null }; } },
        beEqual: function (a, b, msg) { if (a != b) { throw { wassuccess: false, message: msg || null }; } },
        beSame: function (a, b, msg) { if (a !== b) { throw { wassuccess: false, message: msg || null }; } },
        throwError: function (expr, msg) { try { fn(); throw { wassuccess: false, message: msg || null }; } catch (e) { } }
    };

    // fake ux lib, so real user interface doesn't update when we run the test suite.
    // TODO: make this nicer.
    var fakeUx = { setCurrentTask: function () { } };
    return {
        "registering a task adds it to the task list": function () {
            var testWorker = new Worker("", fakeUx);
            var testTask = Task.makeTask("test task", function () { });

            testWorker.enqueue(testTask);

            should.beEqual(1, testWorker.queueSize(), "queue size was not 1");
        },
        "next returns the first task from the list": function () {
            var testWorker = new Worker("", fakeUx);

            testWorker.enqueue(Task.makeTask("test task", function () { }));
            testWorker.enqueue(Task.makeTask("test task 2", function () { }));
            testWorker.enqueue(Task.makeTask("test task 3", function () { }));


            var runTask = testWorker.nextTask();

            should.beEqual("test task", runTask.title, "first item in queue was not titled correctly");
        }
    }
})();