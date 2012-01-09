/// <reference path="..\jquery-1.7.1.min.js"/>
/// <reference path="..\Util\DateUtil.js"/>

$(document).delegate("#pageMonth", "pagebeforeshow", function () {
    if (!window.params)
        return; // no parameters defined

    var p = JSON.parse(params);
    p.month = Date.fromJson(p.month);
    $("#monthTitle").text(p.month.getMonthShortName());
});
