jQuery.fn.isVisible = function () {
    var visibility = this.css('visibility');
    return visibility !== "hidden" && visibility !== "collapse";
};

jQuery.fn.visible = function () {
    return this.css('visibility', 'visible');
};

jQuery.fn.invisible = function () {
    return this.css('visibility', 'hidden');
};

jQuery.fn.visibilityToggle = function () {
    return this.css('visibility', function (i, visibility) {
        return (visibility === 'visible') ? 'hidden' : 'visible';
    });
};

jQuery.fn.findByContentText = function (text) {
    return this.contents().filter(function () {
        return jQuery(this).text().trim() === text.trim();
    });
};

String.prototype.toPhoneNumber = function () {

    var s = "" + this;
    if (s.length === 10) {
        var s2 = ("" + s).replace(/\D/g, '');
        var m = s2.match(/^(\d{3})(\d{3})(\d{4})$/);
        return (!m) ? null : "(" + m[1] + ") " + m[2] + "-" + m[3];
    }
    else {
        return s;
    }
};

function hasValue(value) {

    return value && value !== undefined && value !== null;

}

function roundNumber(num, places) {
    if (!("" + num).includes("e")) {
        return +(Math.round(num + "e+" + places) + "e-" + places);
    } else {
        var arr = ("" + num).split("e");
        var sig = ""
        if (+arr[1] + places > 0) {
            sig = "+";
        }
        return +(Math.round(+arr[0] + "e" + sig + (+arr[1] + places)) + "e-" + places);
    }
}

Number.prototype.format = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};

Number.prototype.toUSCurrency = function () {
    return this.toCurrency(2, ".", ",", "$");
};

/* 
    decimal_sep: character used as deciaml separtor, it defaults to '.' when omitted
    thousands_sep: char used as thousands separator, it defaults to ',' when omitted
*/
Number.prototype.toCurrency = function (decimals, decimal_sep, thousands_sep, symbol) {
    var n = this,
        c = isNaN(decimals) ? 2 : Math.abs(decimals), //if decimal is zero we must take it, it means user does not want to show any decimal
        d = decimal_sep || '.', //if no decimal separator is passed we use the dot as default decimal separator (we MUST use a decimal separator)

        /*
        according to [https://stackoverflow.com/questions/411352/how-best-to-determine-if-an-argument-is-not-sent-to-the-javascript-function]
        the fastest way to check for not defined parameter is to use typeof value === 'undefined' 
        rather than doing value === undefined.
        */
        t = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep, //if you don't want to use a thousands separator you can pass empty string as thousands_sep value

        sign = (n < 0) ? '-' : '',

        //extracting the absolute value of the integer part of the number and converting to string
        i = parseInt(n = Math.abs(n).toFixed(c)) + '',

        j = ((j = i.length) > 3) ? j % 3 : 0;

    if (symbol === 'undefined')
        symbol = '';

    return sign + symbol + (j ? i.substr(0, j) + t : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : '');
};

function endsWith(str, suffix) {
    return str.indexOf(suffix, str.length - suffix.length) !== -1;
}

function guidNew() {
    function _p8(s) {
        var p = (Math.random().toString(16) + "000000000").substr(2, 8);
        return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
    }
    return _p8() + _p8(true) + _p8(true) + _p8();
}

function guidEmpty() {
    return "00000000-0000-0000-0000-000000000000";
}

function getQueryStringParams() {
    var a = window.location.search.substr(1).split('&');
    var b = {};
    for (var i = 0; i < a.length; ++i) {
        var p = a[i].split('=');
        if (p.length !== 2) continue;
        b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
    }
    return b;
}

function getParentForm(source) {
    return $(source).closest("form")[0];
}

function navigateTo(url) {
    window.location.href = url;
}

function newWindowTo(url) {
    window.open(url, '_blank');
}

(function ($) {
    $.fn.serializefiles = function () {

        var obj = $(this);
        /* ADD FILE TO PARAM AJAX */
        var formData = new FormData();
        //$.each($(obj).find("input[type='file']"), function (i, tag) {
        //    $.each($(tag)[0].files, function (i, file) {
        //        formData.append(tag.name, file);
        //    });
        //});
        var params = $(obj).serializeArray();
        $.each(params, function (i, val) {
            formData.append(val.name, val.value);
        });

        return formData;
    };
})(jQuery);

function isIPad() {
    return navigator.userAgent.match(/iPad/i) !== null;
}