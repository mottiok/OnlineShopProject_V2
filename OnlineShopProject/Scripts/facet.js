
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

$(document).ready(function() {
    signChange();
});