
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\DateUtil.js"/>

describe("DateUtil tests", function () {

    it("should return the short month name for a date", function () {
        var dte = new Date("01-Feb-2003");
        var shortName = dte.getMonthShortName();

        expect(shortName).toBe("Feb");
    });

});
