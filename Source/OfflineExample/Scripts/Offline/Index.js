/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\json2.js"/>
/// <reference path="..\Util\DateUtil.js"/>
/// <reference path="OfflineGlobal.js"/>
/// <reference path="StorageService.js"/>

$(document).delegate("#pageAll", "pagebeforeshow", function () {
    new indexController({
        list: $("#pageAll #list"),
        currentStatus: $("#pageAll #currentStatus"),
        testButton: $("#pageAll #tst")
    });
});

indexController.ROW_TEMPLATE = "<li data-params='@params'><a href='/OfflineExample/Offline/Month'>@monthDescription<span class='ui-li-count'>@count</span></a></li>";

function indexController(view) {

    var that = this;
    this.getView = function () { return view; }

    OfflineGlobal.onIndicateOffline = function () { that.displayStatus(false); }
    OfflineGlobal.onIndicateOnline = function () { that.displayStatus(true); }

    view.testButton.click(function () { that.testButtonClick(); });

    this.populate();
}

indexController.prototype.formatRow = function (month, count) {
    var params = JSON.stringify({ month: month.getTrimToMonth() });
    var monthDescription = month.getMonthShortName() + " " + month.getFullYear();

    var row =
        indexController.ROW_TEMPLATE
            .replace("@params", params)
            .replace("@monthDescription", monthDescription)
            .replace("@count", count);

    return row;
}

indexController.prototype.populate = function () {
    var that = this;
    var view = this.getView();
    var appointmentMonths = Storage.getAllByMonth();

    view.list.text("");

    appointmentMonths.forEach(function (appointmentMonth) {
        view.list.append(that.formatRow(appointmentMonth.getMonth(), appointmentMonth.getCount()));
    });

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
        // do nothing
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
