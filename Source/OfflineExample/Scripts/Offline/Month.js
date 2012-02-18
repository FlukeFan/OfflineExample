/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\Util\DateUtil.js"/>

var monthParams = null;

$(document).delegate("#pageMonth", "pagebeforeshow", function (eventObject) {

    if (window.sessionStorage.getItem("params")) {
        monthParams = JSON.parse(window.sessionStorage.getItem("params"));
        monthParams.month = Date.fromJson(monthParams.month);
        window.sessionStorage.removeItem("params");
    }

    if (monthParams == null)
        return;

    if (!eventObject.target.controller) {

        eventObject.target.footerController = FooterController.Init($("#pageMonth"));

        eventObject.target.controller =
            new MonthController({
                title: $("#monthTitle"),
                list: $("#pageMonth #list"),
                newButton: $("#pageMonth #new")
            });
    }

    eventObject.target.footerController.load();
    eventObject.target.controller.load(monthParams);
});

MonthController.ROW_TEMPLATE = "<li data-params='@params'><a href='/OfflineExample/Offline/Edit'>@day - @notes</a></li>";

function MonthController(view) {
    var that = this;
    this.getView = function () { return view; }

    view.newButton.click(function () { that.newButtonClick(); });
}

MonthController.prototype.load = function (params) {
    var view = this.getView();
    view.params = params;

    view.title.text(params.month.getMonthShortName());
    this.populate();

    return this;
}

MonthController.prototype.populate = function () {
    var that = this;
    var view = this.getView();
    var params = view.params;

    var appointments = Storage.getAllForMonth(params.month);

    view.list.text("");

    appointments.forEach(function (appointment) {
        view.list.append(that.formatRow(appointment.getVisitDate().getDate(), appointment.getNotes()));
    });

    view.list.append("<li></li>");
    view.list.listview("refresh");
}

MonthController.prototype.formatRow = function (day, notes) {
    var row =
        MonthController.ROW_TEMPLATE
            .replace("@day", day)
            .replace("@notes", notes);

    return row;
}

MonthController.prototype.newButtonClick = function () {
    $.mobile.changePage(OfflineGlobal.getEditUrl());
}
