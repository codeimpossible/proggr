var TestRunTask = (function () {
    var unitTestTask = Task.makeTask("Running unit tests", function (ux) {
        var runner = function () {
            var passed = 0, failed = 0;

            for (var key in tests) {
                if (tests.hasOwnProperty(key)) {
                    var testName = key,
                        test = tests[key],
                        result = { wassuccess: true, message: '' };
                    try {
                        test();
                    } catch (e) {
                        result = e;
                    }

                    if (result.wassuccess) passed++;
                    else failed++;
                }
            }

            return failed == 0;
        };

        return runner();
    });
    return unitTestTask;
})(tests);