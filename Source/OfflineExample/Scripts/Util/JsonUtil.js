
(function () {

    var originalStringify = JSON.stringify;

    JSON.stringify = function (obj) {
        return originalStringify(obj, function (key, value) {

            for (var prop in value)
                if (typeof (value[prop].getMonth) == 'function')
                    value[prop] = value[prop].toJsonString();

            return value;
        });
    }

})();
