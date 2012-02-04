
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\Footer.js"/>
/// <reference path="..\StorageService.js"/>

describe("Footer tests", function () {

    var view;

    beforeEach(function () {
        view = {
            currentStatus: new elementStub()
        };
    });

    it("should show offline when offline raised", function () {
        var index = new FooterController(view);
        OfflineGlobal.onIndicateOffline();

        expect(view.currentStatus.currentText).toBe("Offline");
        expect(view.currentStatus.currentCss["color"]).toBe("red");
    });

    it("should show online when online raised", function () {
        var index = new FooterController(view);
        OfflineGlobal.onIndicateOnline();

        expect(view.currentStatus.currentText).toBe("Online");
        expect(view.currentStatus.currentCss["color"]).toBe("lightgreen");
    });

});
