
describe("JsonUtil tests", function () {

    it("should convert dates to application used format", function () {
        var obj = {
            dte: new Date("01-Jul-2003 23:00"),
            str: "test"
        };

        var json = JSON.stringify(obj);

        expect(json).toBe('{"dte":"2003-07-01T23:00:00","str":"test"}');
    });


});
