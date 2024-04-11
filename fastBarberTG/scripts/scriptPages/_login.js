window.login = (function () {

    var config = {
        urls: {
            logarUsuario: ''
        },
    };

    var init = function ($config) {
        config = $config;
    };

    var logarUsuario = function () {
        event.preventDefault();

       $.post(config.urls.logarUsuario,
       {
           email: $("#username").val(),
           senha: $("#userpassword").val()
       }).done(function (msg) {
           window.location.href = "/BarberControl";
       }).fail(function(msg) {
           iziToast.error({
               title: 'Error',
               message: "Usuário não encontrado"
           });
       })
   }

    return {
        init: init,
        logarUsuario: logarUsuario
        
    };
})();

