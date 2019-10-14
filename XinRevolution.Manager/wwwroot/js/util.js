// 使用方式 : var fullName = String.format('Hello. My name is {0} {1}.', 'FirstName', 'LastName');
String.format = function () {
    var s = arguments[0];

    if (s === null)
        return "";

    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = getStringFormatPlaceHolderRegEx(i);

        s = s.replace(reg, (arguments[i + 1] === null ? "" : arguments[i + 1]));
    }

    return cleanStringFormatResult(s);
};

// 使用方式 : var fullName = 'Hello. My name is {0} {1}.'.format('FirstName', 'LastName');
String.prototype.format = function () {
    var txt = this.toString();

    for (var i = 0; i < arguments.length; i++) {
        var exp = getStringFormatPlaceHolderRegEx(i);
        txt = txt.replace(exp, (arguments[i] === null ? "" : arguments[i]));
    }

    return cleanStringFormatResult(txt);
};

// 讓輸入的字串可以包含{}
function getStringFormatPlaceHolderRegEx(placeHolderIndex) {
    return new RegExp('({)?\\{' + placeHolderIndex + '\\}(?!})', 'gm');
}

// 當format格式有多餘的position時，就不會將多餘的position輸出
function cleanStringFormatResult(txt) {
    if (txt === null)
        return "";

    return txt.replace(getStringFormatPlaceHolderRegEx("\\d+"), "");
}