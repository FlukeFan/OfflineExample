
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\Index.js"/>
/// <reference path="..\StorageService.js"/>

describe("Index tests", function () {

    var view;

    beforeEach(function () {
        InitStubs();

        view = {
            list: new elementStub(),
            newButton: new elementStub()
        };

        var localStore = new localStorageStub();
        Storage = new StorageService(localStore);
    });

    it("should populate items during construction", function () {

        spyOn(Storage, "getAllByMonth").andCallFake(function () {
            return [
                new AppointmentMonth().setMonth(new Date("02 Mar 2004")).setCount(2),
                new AppointmentMonth().setMonth(new Date("03 Apr 2005")).setCount(3),
                new AppointmentMonth().setMonth(new Date("04 May 2006")).setCount(4)
            ];
        });

        spyOn(indexController, "ROW_TEMPLATE");
        indexController.ROW_TEMPLATE = "(@monthDescription)(@count)";

        var index = new indexController(view).load();
        expect(view.list.currentText).toBe(""); // verify list was 'cleared' first
        expect(view.list.appended.length).toBe(4);
        expect(view.list.appended[0]).toBe("(Mar 2004)(2)");
        expect(view.list.appended[1]).toBe("(Apr 2005)(3)");
        expect(view.list.appended[2]).toBe("(May 2006)(4)");
        expect(view.list.appended[3]).toMatch("<li></li>");

        expect(view.list.listviewEvents.length).toBe(1);
        expect(view.list.listviewEvents[0]).toBe("refresh");
    });

    it("should replace items in template", function () {
        spyOn(indexController, "ROW_TEMPLATE");

        indexController.ROW_TEMPLATE = "(@params)(@monthDescription)(@count)";

        var result = new indexController(view).formatRow(new Date("02 Mar 2004"), 5);

        var expectedParams = JSON.stringify({ month: new Date("01 Mar 2004") });
        expect(result).toBe("(" + expectedParams + ")(Mar 2004)(5)");
    });

    it("should open Edit when New clicked", function () {
        spyOn(OfflineGlobal, "getEditUrl").andCallFake(function () { return "EditUrl"; });

        new indexController(view);
        view.newButton.click();

        expect($.mobile.changedPage).toBe("EditUrl");
    });

});
