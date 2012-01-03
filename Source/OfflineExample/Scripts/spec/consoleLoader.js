var document;

function load(f) {
    var fs = global.globalRequire('fs');
    var src = fs.readFileSync(f);
    global.globalRequire('vm').runInThisContext(src, f);
}
