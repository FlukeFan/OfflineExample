
// blank out any test responses
function InitStubs() {
    $.mobile.changedPage = null;
}

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

// jquerymobile stub
$.mobile = {};
$.mobile.changePage = function (url) {
    $.mobile.changedPage = url;
}

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
    this.getItem = function (k) { return this[k]; };
}

// utility function to get the underlying raw object from a dto
Object.prototype.getRaw = function () {
    return JSON.parse(this.toJson());
}
