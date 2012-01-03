/// <reference path="..\jquery-1.7.1.min.js"/>

$(document).delegate("#pageMonth", "pageinit", function () {
    if (!window.params)
        return; // no parameters defined

    $("#monthTitle").text(params);
});

