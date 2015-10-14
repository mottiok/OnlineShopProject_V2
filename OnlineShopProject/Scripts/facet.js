
var signChange = function() {
    var accordianItem = $("#accordian .panel .panel-heading .panel-title");
    
    accordianItem.click(function () {
        var minusPlus = $(this).find("i");
        if (minusPlus.attr('class').indexOf("minus") > 0) {

            minusPlus.attr('class', "fa fa-plus");
        } else {
            minusPlus.attr('class', "fa fa-minus");
        }
    });
};

$(document).ready(function() {
    signChange();
});