var _data = {

    //cart domain
    cart: {

        data: {
            CustomerId: "",
            Items:
                [
                    {
                        Code: "",
                        Quantity: 0,
                        DisplayName: "",
                        PlacedPrice: 0.0,
                        Total: 0.0
                    }
                ],
            TotalItems: 0,
            ShippingTotal: 0,
            TaxTotal: 0,
            SubTotal: 0,
            Total: 0,
            Currency: ""
        },

        state: {
            IsSlideOutCartOpen : false
        }
    },

    //customer domain
    customer: {
        CustomerId: "",
        FirstName: "",
        LastName: "",
        IsLoggedIn: false
    }
};

var _methods = {

    //========================================================
    //service methods
    //========================================================
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

    clearCartService: function () {
        var self = this;
        return $.ajax(
            {
                method: 'DELETE',
                url: '/api/cart'
            }).then(function (result) {
                Store.$emit('cart:clearCartService:then', result);
                return result;

            }).fail(function (error) {
                Store.$emit('cart:clearCartService:fail', error);
                return error;
            });
    },

    getCustomerService: function () {

        var self = this;
        return $.ajax(
            {
                method: 'GET',
                url: '/api/customer'

            }).then(function (result) {
                Store.$emit('customer:getCustomerService:then', result);
                return result;

            }).fail(function () {
                Store.$emit('customer:getCustomerService:fail');
                return;
            });
    },

    updateCartService: function (skuCode, quantityToUpdate) {

        var self = this;
        return $.ajax(
            {
                method: 'GET',
                url: '/api/cart/update/' + skuCode + '/' + quantityToUpdate

            }).then(function (result) {
                Store.$emit('cart:updateCartService:then', result);
                return result;

            }).fail(function () {
                Store.$emit('cart:updateCartService:fail');
                return;
            });
    },

    addCartService: function (skuCode, quantityToAdd) {

        var self = this;
        return $.ajax(
            {
                method: 'GET',
                url: '/api/cart/add/' + skuCode + '/' + quantityToAdd

            }).then(function (result) {
                Store.$emit('cart:addCartService:then', result);
                return result;

            }).fail(function () {
                Store.$emit('cart:addCartService:fail');
                return;
            });
    },

    //========================================================
    //data methods
    //========================================================
    setCartData: function (cartResult) {

        var self = this;

        //rs: manually mapping each of the fields
        self.cart.data.CustomerId = cartResult.CustomerId;
        self.cart.data.Items = cartResult.Items;
        self.cart.data.TotalItems = cartResult.TotalItems;
        self.cart.data.ShippingTotal = cartResult.ShippingTotal;
        self.cart.data.TaxTotal = cartResult.TaxTotal;
        self.cart.data.SubTotal = cartResult.SubTotal;
        self.cart.data.Total = cartResult.Total;
        self.cart.data.Currency = cartResult.Currency;
    },

    clearCartData: function (result) {

        var self = this;

        //rs: manually mapping each of the fields
        self.cart.data.Items = [];
        self.cart.data.TotalItems = 0;
        self.cart.data.ShippingTotal = 0;
        self.cart.data.TaxTotal = 0;
        self.cart.data.SubTotal = 0;
        self.cart.data.Total = 0;
    },

    setCustomerData: function (customerResult) {

        var self = this;

        //rs: manually mapping each of the fields
        self.customer.CustomerId = customerResult.CustomerId;
        self.customer.FirstName = customerResult.FirstName;
        self.customer.LastName = customerResult.LastName;
        self.customer.IsLoggedIn = true;
    },

    clearCustomerData: function () {

        var self = this;

        //rs: manually mapping each of the fields
        self.customer.CustomerId = '';
        self.customer.FirstName = '';
        self.customer.LastName = '';
        self.customer.IsLoggedIn = false;
    },

    setSlideOutState: function () {

        var self = this;

        if (!self.cart.state.IsSlideOutCartOpen)
            self.cart.state.IsSlideOutCartOpen = true;

        else
            self.cart.state.IsSlideOutCartOpen = false;
    },

    //========================================================
    //UI methods
    //========================================================
    openSlideCart: function () {

        var self = this;

        if (!self.cart.state.IsSlideOutCartOpen) {

            $('.vue-CartHeader').trigger('click');
            self.cart.state.IsSlideOutCartOpen = true;
        }
    }
};

var Store = new Vue({
    data: _data,
    methods: _methods
});
window.Store = Store;

//rs: service method events
Store.$on('cart:getCartService', Store.getCartService);
Store.$on('cart:clearCartService', Store.clearCartService);
Store.$on('cart:updateCartService', Store.updateCartService);
Store.$on('cart:addCartService', Store.addCartService);
Store.$on('customer:getCustomerService', Store.getCustomerService);

//rs: data method success events
Store.$on('cart:getCartService:then', Store.setCartData);
Store.$on('cart:clearCartService:then', Store.clearCartData);
Store.$on('cart:updateCartService:then', Store.setCartData);
Store.$on('cart:addCartService:then', Store.setCartData);
Store.$on('cart:addCartService:then', Store.openSlideCartIfClosed);
Store.$on('customer:getCustomerService:then', Store.setCustomerData);

//rs: data method fail event
Store.$on('customer:getCustomerService:fail', Store.clearCustomerData);

//rs: handle the failure
//Store.$on('cart:getCartService:fail', xxx);

//rs: handle UI events
Store.$on('cart:clickCartHeader', Store.setSlideOutState);
