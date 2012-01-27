/// <reference path="..\jquery-1.7.1.min.js"/>

// Global functions for all offline pages that monitor the state of the cache and the online status

function OfflineGlobal(settings) {

    this.settings = function () { return settings; };

    var isOnline = false;
    OfflineGlobal.isOnline = function () { return isOnline; }
    OfflineGlobal.setOnline = function (newIsOnline) { isOnline = newIsOnline; }

    OfflineGlobal.current = this;

}

OfflineGlobal.prototype.init = function () {

    // Currently the framework needs this 'adjustment' for offline apps:
    //  https://github.com/jquery/jquery-mobile/issues/1579

    $.ajaxPrefilter(function (options, originalOptions, jqXHR) {
        if (originalOptions.forceNetwork == undefined)
            options.isLocal = true;
    });

    $(window).bind("offline", this.raiseOffline);
    $(window).bind("online", this.raiseOnline, false);
    $(window.applicationCache).bind("error", this.raiseOffline);
    $(window.applicationCache).bind("updateready", this.updatedCache);

    // browsers don't seem to reliably indicate their online/offline status
    // additionally, a we are more interested in the server being online/offline than the network connection
    // so diable this next line, and wait on an actual response (or error)

    //window.navigator.onLine ? this.raiseOnline() : this.raiseOffline();

    setInterval(this.queryOnline, 4000);
    this.queryOnline();
}

OfflineGlobal.prototype.updatedCache = function () {
    if (window.applicationCache.status == window.applicationCache.UPDATEREADY) {
        // manifest was updated, so refresh the page with the new cache content
        window.applicationCache.swapCache();
        document.location.href = window.location.href;
    }
}

OfflineGlobal.prototype.queryOnline = function () {
    $.ajax({
        url: OfflineGlobal.current.settings().onlinePath,
        cache: false,
        timeout: 2000,
        success: OfflineGlobal.current.raiseOnline,
        error: OfflineGlobal.current.raiseOffline,
        forceNetwork: true
    });
}

OfflineGlobal.prototype.raiseOffline = function () {
    OfflineGlobal.setOnline(false);
    OfflineGlobal.onIndicateOffline();
}

OfflineGlobal.prototype.raiseOnline = function () {
    OfflineGlobal.setOnline(true);
    OfflineGlobal.onIndicateOnline();
    OfflineGlobal.onExecuteCommands();
}

OfflineGlobal.onIndicateOffline = function () { }   // default to do nothing
OfflineGlobal.onIndicateOnline = function () { } // default to do nothing
OfflineGlobal.onExecuteCommands = function () { } // default to do nothing
