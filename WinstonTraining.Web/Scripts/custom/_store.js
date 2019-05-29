var _methods = {

    //service methods
    validatePromoCodeService: function (promoCode) {

        var self = this;
        return $.ajax(
            {
                method: 'GET',
                url: '/api/promo/validate/' + promoCode,
                dataType: 'json'
            }).then(function (result) {
                Store.$emit('checkout:validatePromoCodeService:then', result);
                return result;
            }).fail(function (error) {
                Store.$emit('checkout:validatePromoCodeService:fail', error);
                return error;
            });
    },

    //data and state methods
    updatePromoCodeDataAndState: function (promoCode, isPromoCodeValid) {

        var self = this;
        self.checkout.state.promoCode = promoCode;
        self.checkout.state.isPromoCodeApplied = isPromoCodeValid;
        self.checkout.state.isPromoCodeAppliedFailure = !isPromoCodeValid;
    },

    //action methods
    attemptApplyPromoCode: function (promoCode) {
        var self = this;

        //short circult the validation call if we know the promo code is invalid because it's less that 10 chars
        if (promoCode.length < 10) {
            self.updatePromoCodeData(promoCode, false);
            self.checkout.state.isPromoCodeBeingValidated = false;
            return;
        }

        self.checkout.state.isPromoCodeBeingValidated = true;

        self.validatePromoCodeService(promoCode)
            .then(function (result) {
                self.updatePromoCodeData(promoCode, true);
                self.checkout.state.isPromoCodeBeingValidated = false;
                Store.$emit('checkout:attemptApplyPromoCode:then', result);
            })
            .fail(function (error) {
                self.updatePromoCodeData(promoCode, false);
                self.checkout.state.isPromoCodeBeingValidated = false;
                Store.$emit('checkout:attemptApplyPromoCode:fail', error);
            });
    }
}

var Store = new Vue({
    data: _data,
    methods: _methods
});
window.Store = Store;