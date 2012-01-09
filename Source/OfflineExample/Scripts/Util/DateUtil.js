
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
        if (typeof (dte) == "string") {
            if (dte.substr(0, 6) == "\/Date(")
                return new Date(parseInt(dte.substr(6)));

            return new Date(dte);
        }

        return dte;
    }

})();
