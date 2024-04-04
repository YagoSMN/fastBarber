window.User = (function () {

    var config = {
        urls: {
            showHorarios: ''
        },
    };

    var init = function ($config) {
        config = $config;
    };

    var showHorarios = function() {
        $.get(config.urls.showHorarios).done(function(html) {
            $("#user-requestdiv").show("slow").html(html);
            $("#user-menu").hide("slow");
        }).fail(function(msg) {
            iziToast.error({
                title: 'ERRO',
                message: 'Erro na requisição',
            });
        });
    };

    var addDay = function (el) {
        event.preventDefault();
        $(el).addClass("checked");
    }

    return {
        init: init,
        showHorarios: showHorarios,
        addDay: addDay
    };
})();

