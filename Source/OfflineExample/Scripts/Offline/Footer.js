/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="OfflineGlobal.js"/>

function FooterController(view) {
    var that = this;
    this.getView = function () { return view; }

    OfflineGlobal.onIndicateOffline = function () { that.displayStatus(false); }
    OfflineGlobal.onIndicateOnline = function () { that.displayStatus(true); }
}

FooterController.Init = function (footerElement) {
    new FooterController({
        currentStatus: footerElement.find("#currentStatus")
    });
}

FooterController.prototype.displayStatus = function (isOnline) {
    var view = this.getView();
    view.currentStatus.text(isOnline ? "Online" : "Offline");
    view.currentStatus.css("color", isOnline ? "lightgreen" : "red");
}
