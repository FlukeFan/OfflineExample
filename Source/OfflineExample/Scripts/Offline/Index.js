/// <reference path="..\json2.js"/>
/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\jquery.mobile-1.0.1.min.js"/>
/// <reference path="..\Util\DateUtil.js"/>
/// <reference path="Footer.js"/>
/// <reference path="OfflineGlobal.js"/>
/// <reference path="StorageService.js"/>

$(document).delegate("#pageAll", "pagebeforeshow", function (eventObject) {

    if (!eventObject.target.controller) {

        eventObject.target.footerController = FooterController.Init($("#pageAll"));

        eventObject.target.controller =
            new indexController({
                list: $("#pageAll #list"),
                newButton: $("#pageAll #new")
            });
    }

    eventObject.target.footerController.load();
    eventObject.target.controller.load();
});

indexController.ROW_TEMPLATE = "<li><a href='/OfflineExample/Offline/Month?params=@params'>@monthDescription<span class='ui-li-count'>@count</span></a></li>";

function indexController(view) {

    var that = this;
    this.getView = function () { return view; }

    view.newButton.click(function () { that.newButtonClick(); });
}

indexController.prototype.load = function () {
    this.populate();
    return this;
};

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

indexController.prototype.newButtonClick = function () {
    $.mobile.changePage(OfflineGlobal.getEditUrl());
}
