/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\json2.js"/>
/// <reference path="OfflineGlobal.js"/>
/// <reference path="Dto\Appointment.js"/>
/// <reference path="Dto\AppointmentMonth.js"/>
/// <reference path="Dto\CmdFetchFuture.js"/>

var Storage = new StorageService(window.localStorage);

StorageService.KEY_APPPOINTMENTS = "appointments";

function StorageService(localStore) {
    this.getLocalStore = function () { return localStore; }

    var that = this;
    OfflineGlobal.onExecuteCommands = function () { that.onExecuteCommands(); }
}

StorageService.prototype.onExecuteCommands = function () {
    var localStore = this.getLocalStore();
    var cmd = new CmdFetchFuture();

    this.execute(cmd, function (list) {
        localStore.setItem(StorageService.KEY_APPPOINTMENTS, JSON.stringify(list));
    });
}

StorageService.prototype.execute = function (cmd, callback) {
    $.ajax({
        url: "/OfflineExample/Online/Execute",
        type: "POST",
        dataType: "json",
        data: cmd.toJson(),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus) {
            callback(data);
        }
    });
}

StorageService.prototype.getAllByMonth = function () {
    var rawList = this.getRawList();
    var result = [];

    rawList.forEach(function (rawObj) {
        var app = new Appointment(rawObj);

        if ((result.length == 0) || (result[result.length - 1].getMonth().valueOf() != app.getVisitDate().getTrimToMonth().valueOf()))
            result.push(new AppointmentMonth().setMonth(app.getVisitDate().getTrimToMonth()));

        var last = result[result.length - 1];
        last.setCount(last.getCount() + 1);
    });

    return result;
}

StorageService.prototype.getRawList = function () {
    var localStore = this.getLocalStore();
    var json = localStore.getItem(StorageService.KEY_APPPOINTMENTS);
    var rawList = JSON.parse(json);
    return rawList;
}
