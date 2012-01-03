{
    var sys = require("util");

    global.window = {
        setTimeout: setTimeout,
        clearTimeout: clearTimeout,
        setInterval: setInterval,
        clearInterval: clearInterval
    };

    // load the loader
    global.globalRequire = require;
    var f = "OfflineExample/Scripts/spec/consoleLoader.js";
    var fs = require("fs");
    var src = fs.readFileSync(f);
    require('vm').runInThisContext(src, f);

    load("../SDKs/jasmine/jasmine.js");
    load("../SDKs/jasmine/console/ConsoleReporter.js");

    load("OfflineExample/Scripts/spec/LoadTests.spec.js");

    var jasmineEnv = jasmine.getEnv();

    var showColors = false;
    var consoleReporter = new jasmine.ConsoleReporter(sys.print, testsDone, showColors);

    jasmineEnv.addReporter(consoleReporter);
    jasmineEnv.execute();
}

function testsDone(runner) {
    if (runner.results().failedCount > 0) {
        console.error("Exit code (1)");
        process.stdout.on('drain', function () { process.exit(1); })
    }
}

