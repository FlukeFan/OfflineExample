/// <reference path="jquery-1.7.1.min.js"/>
/// <reference path="jquery.mobile-1.0.1.min.js"/>

// note - this MUST be loaded BEFORE the JQM library

$(document).bind("mobileinit", function () {
    $.mobile.defaultPageTransition = 'none';
});
