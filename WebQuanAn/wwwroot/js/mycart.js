

$(document).ready(
    function () {
        _getCount();
      
      

    });

function _getCount() {
    
    $.ajax({
        type: 'GET',
        url: 'cart/countQuantity',
        success: function (result) {
         
            $('.my-cart-badge').html(result)
        }
    });
}

function addcart(id) {

    $.ajax({
        url: 'cart/AddToCart/' + id,
        type: "Get",
        contentType: "application/json; charset=utf-8",
       
        async: true,
        success: function (result) {


            _getCount();

            $('#myModal').html(result);
            


        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
        




    });
}

function CartTemp() {

    $.ajax({
        url: 'cart/Cart/',
        type: "Get",
        contentType: "application/json; charset=utf-8",

        async: true,
        success: function (result) {


            _getCount();

            $('#myModal').html(result);



        }


    });
}

function Empty() {

    $.ajax({
        url: 'cart/EmptyCart/',
        type: "Get",
        contentType: "application/json; charset=utf-8",

        async: true,
        success: function (result) {


            _getCount();

            $('#myModal').html(result);



        }


    });
}
function remove(id) {

    $.ajax({
        url: 'cart/removecart/' + id,
        type: "Get",
        contentType: "application/json; charset=utf-8",
       
        async: true,
        success: function (result) {


            _getCount();

            $('#myModal').html(result);



        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }








    });
}
function Chekout() {

    $.ajax({
        url: 'cart/CartFinal/',
        type: "Get",
        contentType: "application/json; charset=utf-8",

        async: true,
        success: function (result) {


           

            $('#myModal').html(result);



        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }





    });
}


