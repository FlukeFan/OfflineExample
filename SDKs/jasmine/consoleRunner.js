try {

    var sys = require("util");

    global.window = {
        setTimeout: setTimeout,
        clearTimeout: clearTimeout,
        setInterval: setInterval,
        clearInterval: clearInterval
    };

    load("jasmine.js");
    load("console/ConsoleReporter.js");

    var jasmineEnv = jasmine.getEnv();

    var showColors = true;

    var consoleReporter = new jasmine.ConsoleReporter(sys.print, testsDone, showColors);

    jasmineEnv.addReporter(consoleReporter);
    jasmineEnv.execute();
}
catch (e) {
    console.error("Error: " + e);

    try {
        for (prop in e)
            console.error(prop + ": " + e[prop]);
    }
    catch (ie) { }
    process.exit(1);
    throw e;
}

function testsDone(runner) {
    if (runner.results().failedCount > 0) {
        console.error("Exit code (1)");
        process.exit(1);
    }
}

function load(f) {
    var fs = require('fs');
    var src = fs.readFileSync(f);
    require('vm').runInThisContext(src, f);
}
