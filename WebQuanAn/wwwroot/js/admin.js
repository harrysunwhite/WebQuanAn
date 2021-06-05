
var $Content = $("#frmChange");

function ShowChangepass() {

    $Content.html("");
   
    $.ajax({
        url: '/AdminUser/ChangePass',

        type: "Get",
        timeout: 20000,

        async: true,


        success: function (result) {
            $Content.html(result);
           


            $('#preloader-wrapper').toggleClass('hide');
        },
        failure: function (message) {
            $('#preloader-wrapper').toggleClass('hide');
        }
    });

}
   
$(document).ready(

    function () {

        $("#btChangePass").click(function () {
           
            if ($("#frmChange").valid()) {
               
                $.ajax({
                    type: "POST",
                    url: "/AdminUser/ChangePass",
                    data: $("#frmChange").serialize(),
                    success: function (result) {
                        $('#preloader-wrapper').toggleClass('hide');
                        if (result.status >= 1) {

                            Swal.fire(

                                'Đổi mật khẩu thành công',
                                'Đăng nhập lại',
                                'success'
                            )
                            sessionStorage.clear();
                            location.reload();
                        }
                        else if ((result.status == -3)) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Đã có lỗi xảy ra',
                                text: 'Mật khẩu cũ không đúng',
                                
                            })
                        }
                        else {
                            toastr.warning(result.text);
                        }
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    },
                    failure: function (message) {
                        $('#preloader-wrapper').toggleClass('hide');
                    }
                });
            }


        });
      


    });