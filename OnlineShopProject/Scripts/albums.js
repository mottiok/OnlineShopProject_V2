$(function () {
    $(".cart_quantity_input").change(function () {
        var cartItemId = $(this).parents(".cart_quantity").data("cart-item-id"),
            newQuantity = $(this).val();

        $.post("/Carts/UpdateQuantityValue", { cartItemId: cartItemId, newQuantity: newQuantity }, function () {
            
        });
    });

    $(".cart_quantity_input").keypress(function (e) {
        return !isNaN(String.fromCharCode(e.keyCode));
    });
});
