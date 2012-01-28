
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\Month.js"/>
/// <reference path="..\StorageService.js"/>

describe("Month tests", function () {

    var view;

    beforeEach(function () {
        view = {
            params: { month: new Date("01 Jul 2001 02:03:04") },
            title: new elementStub(),
            list: new elementStub(),
        };

        var localStore = new localStorageStub();
        Storage = new StorageService(localStore);
    });

    it("should set the title on construction", function () {

        var month = new MonthController(view);

        expect(view.title.currentText).toBe("Jul");
    });

});
