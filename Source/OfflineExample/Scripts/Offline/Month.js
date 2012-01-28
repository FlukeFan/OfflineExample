/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\Util\DateUtil.js"/>

$(document).delegate("#pageMonth", "pagebeforeshow", function () {
    if (!window.params)
        return; // no parameters defined

    var p = JSON.parse(params);
    p.month = Date.fromJson(p.month);
    //$("#monthTitle").text(p.month.getMonthShortName());

    new MonthController({
        params: p,
        title: $("#monthTitle"),
        list: $("#pageMonth #list")
    });
});

MonthController.ROW_TEMPLATE = "<li data-params='@params'><a href='#'>@day - @notes</a></li>";

function MonthController(view) {
    var that = this;
    this.getView = function () { return view; }

    var params = this.getView().params;

    view.title.text(params.month.getMonthShortName());

    this.populate();
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
