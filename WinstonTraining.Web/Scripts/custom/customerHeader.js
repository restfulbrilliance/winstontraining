//rs: Vue.js instance for the cart header
var customerHeader = new Vue({

    //rs: note this is scoped to a DOM element
    el: '.vue-CustomerHeader',

    //rs: scoped data to the 'customer' domain
    data: Store.customer
});
