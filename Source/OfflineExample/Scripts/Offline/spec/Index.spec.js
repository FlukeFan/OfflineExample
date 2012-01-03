
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\Index.js"/>

describe("Index tests", function () {

    var view;

    beforeEach(function () {
        view = {
            list: new elementStub(),
            currentStatus: new elementStub(),
            testButton: new elementStub()
        };
    });

    it("should populate items during construction", function () {
        var index = new indexController(view);

        expect(view.list.appended.length).toBe(4);
        expect(view.list.appended[0]).toMatch("<li data-params='Nov'><a");
        expect(view.list.appended[1]).toMatch("<li data-params='Jan'><a");
        expect(view.list.appended[2]).toMatch("<li data-params='Feb'><a");
        expect(view.list.appended[3]).toMatch("<li></li>");

        expect(view.list.listviewEvents.length).toBe(1);
        expect(view.list.listviewEvents[0]).toBe("refresh");
    });

    it("should show offline when offline raised", function () {
        var index = new indexController(view);
        OfflineGlobal.onIndicateOffline();

        expect(view.currentStatus.currentText).toBe("Offline");
        expect(view.currentStatus.currentCss["color"]).toBe("red");
    });

    it("should show online when online raised", function () {
        var index = new indexController(view);
        OfflineGlobal.onIndicateOnline();

        expect(view.currentStatus.currentText).toBe("Online");
        expect(view.currentStatus.currentCss["color"]).toBe("lightgreen");
    });

});
