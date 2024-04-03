window.user = (function () {

    var config = {
        urls: {
            showHorario: '',
        },
    };

    var init = function ($config) {
        config = $config;
    };

    var showHorario = function() {
        
        $.get(config.urls.showHorario).done(function(html) {
            $("#user-requestdiv").show("slow").html(html);
            $("#user-settings").hide("slow");
            console.log(html)
        }).fail(function(msg) {
            iziToast.error({
                title: 'Erro!',
                message: 'Erro na requisição',
            });
        });
    };

    return {
        init: init,
        showHorario: showHorario
    };
})();

