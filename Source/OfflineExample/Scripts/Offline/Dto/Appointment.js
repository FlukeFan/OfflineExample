/// <reference path="..\..\Util\DateUtil.js"/>

function Appointment(obj) {
    if (!obj) obj = {
        Id: 0,
        ClientId: 0,
        VisitDate: null,
        Notes: ""
    };

    obj.class = "Appointment";
    obj.VisitDate = Date.fromJson(obj.VisitDate);

    this.toJson = function () { return JSON.stringify(obj); };
    this.getRaw = function () { return obj; }; // TODO: see if we can get rid of this (it's only for the tests)

    this.getId = function () { return obj.Id; };
    this.setId = function (id) { obj.Id = id; return this; };

    this.getClientId = function () { return obj.ClientId; };
    this.setClientId = function (clientId) { obj.ClientId = clientId; return this; };

    this.getVisitDate = function () { return obj.VisitDate; };
    this.setVisitDate = function (visitDate) { obj.VisitDate = visitDate; return this; };

    this.getNotes = function () { return obj.Notes; };
    this.setNotes = function (notes) { obj.Notes = notes; return this; };
}

