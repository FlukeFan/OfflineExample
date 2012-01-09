
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\DateUtil.js"/>

describe("DateUtil tests", function () {

    it("should return the short month name for a date", function () {
        var dte = new Date("01-Feb-2003");
        var shortName = dte.getMonthShortName();

        expect(shortName).toBe("Feb");
    });

    it("should return the 1st of the month for a given date", function () {
        var dte = new Date("08-Feb-2003 04:05:67");

        var monthOnly = dte.getTrimToMonth();

        expect(monthOnly.valueOf()).toBe(new Date("01 Feb 2003").valueOf());
    });

});
