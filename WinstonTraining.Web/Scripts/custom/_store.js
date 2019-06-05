var _data = {
    cart: {
        CustomerId: "",
        Items: {
            Code: "",
            Quantity: 0,
            DisplayName: "",
            PlacedPrice: 0.0
        },
        TotalItems: 0,
        ShippingTotal: 0,
        TaxTotal: 0,
        SubTotal: 0,
        Total: 0,
        Currency: ""
    }
};

var _methods = {

    //service methods
    getCartService: function () {

        var self = this;
        return $.ajax(
            {
                method: 'GET',
                url: '/api/cart',
                dataType: 'json'

            }).then(function (result) {
                Store.$emit('cart:getCartService:then', result);
                return result;

            }).fail(function (error) {
                Store.$emit('cart:getCartService:fail', error);
                return error;
            });
    },

    //data methods
    setCartData: function (cartResult) {

        var self = this;

        //rs: manually mapping each of the fields
        self.cart.CustomerId = cartResult.CustomerId;
        self.cart.Items = cartResult.Items;
        self.cart.TotalItems = cartResult.TotalItems;
        self.cart.ShippingTotal = cartResult.ShippingTotal;
        self.cart.TaxTotal = cartResult.TaxTotal;
        self.cart.SubTotal = cartResult.SubTotal;
        self.cart.Total = cartResult.Total;
        self.cart.Currency = cartResult.Currency;
    }
};

var Store = new Vue({
    data: _data,
    methods: _methods
});
window.Store = Store;

//rs: when we hear this event, call the service method
Store.$on('cart:getCartService', Store.getCartService);

//rs: when we hear this event, call the data method
Store.$on('cart:getCartService:then', Store.setCartData);

//rs: handle the failure
//Store.$on('cart:getCartService:fail', xxx);
