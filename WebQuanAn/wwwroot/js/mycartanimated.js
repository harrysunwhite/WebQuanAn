/*

*/

(function ($) {

    "use strict";

    var OptionManager = (function () {
        var objToReturn = {};

        var defaultOptions = {

            clickOnAddToCart: function ($addTocart) { }

        };


        var getOptions = function (customOptions) {
            var options = $.extend({}, defaultOptions);
            if (typeof customOptions === 'object') {
                $.extend(options, customOptions);
            }
            return options;
        }

        objToReturn.getOptions = getOptions;
        return objToReturn;
    }());





    var loadMyCartEvent = function (userOptions) {

        var options = OptionManager.getOptions(userOptions);

    }


    var MyCart = function (target, userOptions) {
        /*
        PRIVATE
        */
        var $target = $(target);
        var options = OptionManager.getOptions(userOptions);
        var $cartIcon = $("." + options.classCartIcon);
        var $cartBadge = $("." + options.classCartBadge);

        /*
        EVENT
        */
        $target.click(function () {
            options.clickOnAddToCart($target);

            var id = $target.data('id');
            var name = $target.data('name');
            var summary = $target.data('summary');
            var price = $target.data('price');
            var quantity = $target.data('quantity');
            var image = $target.data('image');


        });

    }


    $.fn.myCart = function (userOptions) {
        loadMyCartEvent(userOptions);
        return $.each(this, function () {
            new MyCart(this, userOptions);
        });
    }


})(jQuery);
