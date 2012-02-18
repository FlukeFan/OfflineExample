
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\Month.js"/>
/// <reference path="..\StorageService.js"/>

describe("Month tests", function () {

    var view;
    var params = { month: new Date("01 Jul 2001 02:03:04") };

    beforeEach(function () {
        InitStubs();

        view = {
            title: new elementStub(),
            list: new elementStub(),
            newButton: new elementStub()
        };

        var localStore = new localStorageStub();
        Storage = new StorageService(localStore);
    });

    it("should set the title on construction", function () {

        new MonthController(view).load(params);

        expect(view.title.currentText).toBe("Jul");
    });

    it("should populate items during construction", function () {

        spyOn(Storage, "getAllForMonth").andCallFake(function () {
            return [
                new Appointment().setVisitDate(new Date("02 Jul 2004")).setNotes("r1"),
                new Appointment().setVisitDate(new Date("03 Jul 2004")).setNotes("r2"),
            ];
        });

        spyOn(MonthController, "ROW_TEMPLATE");
        MonthController.ROW_TEMPLATE = "(@day)(@notes)";

        var month = new MonthController(view).load(params);
        expect(view.list.currentText).toBe(""); // verify list was 'cleared' first
        expect(view.list.appended.length).toBe(3);
        expect(view.list.appended[0]).toBe("(2)(r1)");
        expect(view.list.appended[1]).toBe("(3)(r2)");
        expect(view.list.appended[2]).toMatch("<li></li>");

        expect(view.list.listviewEvents.length).toBe(1);
        expect(view.list.listviewEvents[0]).toBe("refresh");
    });


    it("should open Edit when New clicked", function () {
        spyOn(OfflineGlobal, "getEditUrl").andCallFake(function () { return "EditUrl"; });

        new MonthController(view);
        view.newButton.click();

        expect($.mobile.changedPage).toBe("EditUrl");
    });
});
