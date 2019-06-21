//rs: Vue.js instance for the add to cart button
var addToCart = new Vue({

    //rs: note this is scoped to a DOM element
    el: '.vue-addToCart',

    //rs: no data
    data: {},

    methods: {

        addToCart: function (skuCode, quantityToAdd) {
            Store.$emit('cart:addCartService', skuCode, quantityToAdd);
        }
    }
});
