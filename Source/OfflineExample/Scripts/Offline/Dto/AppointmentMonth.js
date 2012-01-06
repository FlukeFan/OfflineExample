
function AppointmentMonth(obj) {
    if (!obj) obj = {
        Month: null,
        Count: 0
    };

    obj.class = "AppointmentMonth";

    this.toJson = function () { return JSON.stringify(obj); };

    this.getMonth = function () { return obj.Month; };
    this.setMonth = function (month) { obj.Month = month; return this; };

    this.getCount = function () { return obj.Count; };
    this.setCount = function (count) { obj.Count = count; return this; };
}

