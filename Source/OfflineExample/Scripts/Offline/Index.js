/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\json2.js"/>
/// <reference path="..\Util\DateUtil.js"/>
/// <reference path="OfflineGlobal.js"/>

$(document).delegate("#pageAll", "pagebeforeshow", function () {
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

indexController.prototype.formatRow = function (month, count) {
    var rowTemplate = "<li data-params='@params'><a href='/OfflineExample/Offline/Month'>@monthDescription<span class='ui-li-count'>@count</span></a></li>";

    var params = JSON.stringify({ month: month });
    var monthDescription = month.getMonthShortName() + " " + month.getFullYear();

    var row =
        rowTemplate
            .replace("@params", params)
            .replace("@monthDescription", monthDescription)
            .replace("@count", count);

    return row;
}

indexController.prototype.populate = function () {
    var view = this.getView();

    view.list.text("");
    view.list.append(this.formatRow(new Date("01 Nov 2011"), 2));
    view.list.append(this.formatRow(new Date("01 Jan 2012"), 7));
    view.list.append(this.formatRow(new Date("01 Feb 2012"), 3));
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
