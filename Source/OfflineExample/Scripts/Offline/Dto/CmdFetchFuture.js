
function CmdFetchFuture(obj) {
    if (!obj) obj = { };

    obj.class = "FetchFuture";

    this.toJson = function () { return JSON.stringify(obj); };
}

