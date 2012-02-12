/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="OfflineGlobal.js"/>

function FooterController(view) {
    var that = this;
    this.getView = function () { return view; }
}

FooterController.Init = function (footerElement) {
    return new FooterController({
        currentStatus: footerElement.find("#currentStatus")
    });
}

FooterController.prototype.load = function () {
    var that = this;
    OfflineGlobal.onIndicateOffline = function () { that.displayStatus(false); }
    OfflineGlobal.onIndicateOnline = function () { that.displayStatus(true); }
    return this;
}

FooterController.prototype.displayStatus = function (isOnline) {
    var view = this.getView();
    view.currentStatus.text(isOnline ? "Online" : "Offline");
    view.currentStatus.css("color", isOnline ? "lightgreen" : "red");
}
