
(function () {
    var shortMonth = new Array(12);
    shortMonth[0] = "Jan";
    shortMonth[1] = "Feb";
    shortMonth[2] = "Mar";
    shortMonth[3] = "Apr";
    shortMonth[4] = "May";
    shortMonth[5] = "Jun";
    shortMonth[6] = "Jul";
    shortMonth[7] = "Aug";
    shortMonth[8] = "Sep";
    shortMonth[9] = "Oct";
    shortMonth[10] = "Nov";
    shortMonth[11] = "Dec";

    Date.prototype.getMonthShortName = function () {
        return shortMonth[this.getMonth()];
    }

    Date.prototype.getTrimToMonth = function () {
        return new Date(this.getFullYear(), this.getMonth(), 1);
    }

    Date.fromJson = function (dte) {
        // date parsing fails on some devices, so manually parse them *sigh*
        // http://stackoverflow.com/questions/5392729/javascript-invalid-date-in-ios-android-2-2
        // http://stackoverflow.com/questions/5324178/javascript-date-parsing-on-iphone

        if (typeof (dte) == "string") {
            var arr = dte.split(/[- :T]/);
            var year = arr[0];
            var month = arr[1] - 1;
            var day = arr[2];
            var hours = (arr.length > 3) ? arr[3] : 0;
            var minutes = (arr.length > 4) ? arr[4] : 0;
            var seconds = (arr.length > 5) ? parseInt(arr[5]) : 0;
            return new Date(year, month, day, hours, minutes, seconds);
        }

        return dte;
    }

})();
