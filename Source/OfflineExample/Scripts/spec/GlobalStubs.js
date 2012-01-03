
// window.document
if (!document) {
    // we're running in Node.js, so we need a document object created
    document = new elementStub();
}
else {
    // we're running in a browser, so extend the document object
    elementStubMethods(document);
}

// jquery stub
function $(obj) { return obj; }

function elementStub() {
    this.appended = [];
    this.listviewEvents = [];
    this.currentText = null;
    this.currentCss = {};
}

elementStubMethods(elementStub.prototype);

function elementStubMethods(prototype) {

    prototype.delegate = function () {
    }

    prototype.append = function (text) {
        this.appended.push(text);
    }

    prototype.listview = function (e) {
        this.listviewEvents.push(e);
    }

    prototype.text = function (txt) {
        this.currentText = txt;
    }

    prototype.css = function (name, value) {
        this.currentCss[name] = value;
    }

    prototype.click = function (f) {
        if (f) {
            this.clickEvent = f;
            return;
        }

        this.clickEvent();
    }

}

function localStorageStub() {
    this.setItem = function (k, v) { this[k] = v; };
}