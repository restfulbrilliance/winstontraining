//rs: Vue.js instance for the cart
var cart = new Vue({

    //rs: note this is scoped to a DOM element
    el: '.vue-Cart',

    //rs: scoped data to the 'cart' domain
    data: Store.cart,
   
    methods: {

        clickClearAllBtn: function () {
            Store.$emit('cart:clearCartService');
        },

        updateLineItemQty: function (skuCode, quantityToUpdate) {
            Store.$emit('cart:updateCartService', skuCode, quantityToUpdate);
        }
    }
});
