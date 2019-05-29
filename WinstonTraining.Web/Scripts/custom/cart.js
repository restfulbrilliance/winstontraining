//rs: Vue.js instance for the cart page partial
var cart = new Vue({

    //rs: not this is tied to a DOM element
    el: '.vue-Cart',

    //rs: scoped data to the 'cart' domain
    data: Store.cart,

    //rs: when the Vue.js instance is created, simply emit an event
    created: function () {
        Store.$emit('cart:getCartService');
    }
});
