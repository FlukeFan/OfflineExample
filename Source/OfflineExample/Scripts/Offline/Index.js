/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\json2.js"/>
/// <reference path="..\Util\DateUtil.js"/>
/// <reference path="Footer.js"/>
/// <reference path="StorageService.js"/>

$(document).delegate("#pageAll", "pagebeforeshow", function () {
    FooterController.Init($("#pageAll"));

    new indexController({
        list: $("#pageAll #list"),
        testButton: $("#pageAll #tst")
    });
});

indexController.ROW_TEMPLATE = "<li><a href='/OfflineExample/Offline/Month?params=@params'>@monthDescription<span class='ui-li-count'>@count</span></a></li>";

function indexController(view) {

    var that = this;
    this.getView = function () { return view; }

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
