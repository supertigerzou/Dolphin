define('utils', [], function () {
    var mapMemoToArray = function(items) {
        var underlyingArray = [];
        for (var prop in items) {
            if (items.hasOwnProperty(prop)) {
                underlyingArray.push(items[prop]);
            }
        }
        return underlyingArray;
    };

    return {
        mapMemoToArray: mapMemoToArray
    };
});