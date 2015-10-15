
var signChange = function() {
    var accordianItem = $("#accordian .panel .panel-collapse");

    accordianItem.on("shown.bs.collapse", function() {
        var minusPlus = $(this).parent().find(".panel-heading .panel-title i");
        minusPlus.attr('class', "fa fa-minus");
    });

    accordianItem.on("hidden.bs.collapse", function () {
        var minusPlus = $(this).parent().find(".panel-heading .panel-title i");
        minusPlus.attr('class', "fa fa-plus");
    });
};

var selectedFacet = function () {
    var params = URLToArray();

    $(params).each(function (i, obj) {
        if (obj.value != "undefined") {
            $("#" + obj.key).attr('class', 'panel-collapse in'); // keep section open
            var a_tag = $("#" + obj.key + '-' + obj.value);
            a_tag.find('img').attr("src", "../Content/images/shop/checkbox_selected.jpg");
            a_tag.attr('class', 'selected_facet');

            var newParams = $(params).filter(function (i, p) {
                return p.key != obj.key;
            });

            var new_params = [];

            $(newParams).each(function(i, o) {
                new_params[i] = o.key + "=" + o.value;
            });

            a_tag.attr('href', '/Albums/Filter?' + new_params.join('&'));
        }
    });
};

var URLToArray = function() {
    var url = window.location.href;
    var request = [];
    var pairs = url.substring(url.indexOf('?') + 1).split('&');
    for (var i = 0; i < pairs.length; i++) {
        if (!pairs[i])
            continue;
        var pair = pairs[i].split('=');
        request[i] = { key: decodeURIComponent(pair[0]), value: decodeURIComponent(pair[1]) };
    }
    return request;
};

$(document).ready(function() {
    signChange();
    selectedFacet();
});