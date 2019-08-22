//rs: Vue.js instance for the cart header
var slideCart = new Vue({

    //rs: note this is scoped to a DOM element
    el: '.vue-SlideCart',

    //rs: scoped data to the 'cart' domain
    data: Store.cart,

    mounted: function () {

        $('.vue-CartHeader')
            .bigSlide(
                {

                    menuWidth: '50%',
                    side: 'right',
                    easyClose: true
                });
    },

    methods: {

        updateLineItemQty: function (skuCode, quantityToUpdate) {
            Store.$emit('cart:updateCartService', skuCode, quantityToUpdate);
        }
    }
});
