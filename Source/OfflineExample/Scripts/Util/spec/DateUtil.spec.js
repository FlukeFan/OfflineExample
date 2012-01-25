
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

    it("should convert string dates to JS dates", function () {

        // It's 2012, and we still can't parse dates reliably on a client FFS!!
        // http://stackoverflow.com/questions/5392729/javascript-invalid-date-in-ios-android-2-2
        // http://stackoverflow.com/questions/5324178/javascript-date-parsing-on-iphone

        expect(Date.fromJson(null)).toBe(null);

        var realDate = new Date("01-Feb-2003");
        expect(Date.fromJson(realDate)).toBe(realDate);

        var dateValue = realDate.valueOf();
        var second = 1000;
        var minute = second * 60;
        var hour = minute * 60;

        var date1 = new Date(dateValue + 0);
        var stringDateFormat1 = "2003-02-01";
        expect(Date.fromJson(stringDateFormat1).valueOf()).toBe(date1.valueOf());

        var date2 = new Date(dateValue + hour + (minute * 2));
        var stringDateFormat2 = "2003-02-01T01:02";
        expect(Date.fromJson(stringDateFormat2).valueOf()).toBe(date2.valueOf());

        var date3 = new Date(dateValue + hour + (minute * 2) + (second * 3));
        var stringDateFormat3 = "2003-02-01T01:02:03";
        expect(Date.fromJson(stringDateFormat3).valueOf()).toBe(date3.valueOf());

        var date4 = new Date(dateValue + hour + (minute * 2) + (second * 3));
        var stringDateFormat4 = "2003-02-01T01:02:03.4+01:00";
        expect(Date.fromJson(stringDateFormat4).valueOf()).toBe(date4.valueOf());
    });

});
