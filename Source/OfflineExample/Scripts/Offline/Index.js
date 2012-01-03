/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\json2.js"/>
/// <reference path="OfflineGlobal.js"/>

$(document).delegate("#pageAll", "pageinit", function () {
    new indexController({
        list: $("#pageAll #list"),
        currentStatus: $("#pageAll #currentStatus"),
        testButton: $("#pageAll #tst")
    });
});

function indexController(view) {

    var that = this;
    this.getView = function () { return view; }

    OfflineGlobal.onIndicateOffline = function () { that.displayStatus(false); }
    OfflineGlobal.onIndicateOnline = function () { that.displayStatus(true); }

    view.testButton.click(function () { that.testButtonClick(); });

    this.populate();
}

indexController.prototype.populate = function () {
    var view = this.getView();

    view.list.append("<li data-params='Nov'><a href='/OfflineExample/Offline/Month'>Nov 2011 <span class='ui-li-count'>2</span></a></li>");
    view.list.append("<li data-params='Jan'><a href='/OfflineExample/Offline/Month'>Jan 2011 <span class='ui-li-count'>7</span></a></li>");
    view.list.append("<li data-params='Feb'><a href='/OfflineExample/Offline/Month'>Feb 2011 <span class='ui-li-count'>3</span></a></li>");
    view.list.append("<li></li>");
    view.list.listview("refresh");
}

indexController.prototype.displayStatus = function (isOnline) {
    var view = this.getView();
    view.currentStatus.text(isOnline ? "Online" : "Offline");
    view.currentStatus.css("color", isOnline ? "lightgreen" : "red");
}

indexController.prototype.testButtonClick = function () {

    $(document).ajaxError(function (evt, jqXHR, ajaxSettings, thrownError) {
        alert(thrownError);
    });

    var t = {
        Class: 'MyDto',
        TestValue: 123
    };

    var encoded = JSON.stringify(t);

    $.ajax({
        url: "/OfflineExample/Online/Execute",
        type: "POST",
        dataType: "json",
        data: encoded,
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus) {
            alert(data.Result);
        }
    });

}
