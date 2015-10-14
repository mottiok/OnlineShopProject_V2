$(function () {
    $(".cart_quantity_input").change(function () {
        var cartItemId = $(this).parents(".cart_quantity").data("cart-item-id"),
            newQuantity = $(this).val();

        $.post("/Carts/UpdateQuantityValue", { cartItemId: cartItemId, newQuantity: newQuantity }, function () {
            // Do Nothing
        });
    });

    $(".cart_quantity_input").keypress(function (e) {
        var input = String.fromCharCode(e.keyCode);
        if (isNaN(input)) {
            return false;
        }

        var number = parseInt(input);

        if (number <= 0) {
            return false;
        }

        return true;
    });
});
