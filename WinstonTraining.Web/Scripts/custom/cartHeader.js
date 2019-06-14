//rs: Vue.js instance for the cart header
var cartHeader = new Vue({

    //rs: note this is scoped to a DOM element
    el: '.vue-CartHeader',

    //rs: scoped data to the 'cart' domain
    data: Store.cart
});
