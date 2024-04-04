window.User = (function () {

    var config = {
        urls: {
            showHorarios: '',
            buscarDiaSemana: ''
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
        $("#form-week").empty();
        $(".week-buttons .checked").removeClass("checked");
        $(el).addClass("checked");
        $.get(config.urls.buscarDiaSemana, { Id: $(el).val() }).done(function (html) {
            $("#form-week").show("slow").html(html);
            console.log(html);
        }).fail(function (msg) {
            $("#form-week").empty();
            iziToast.error({
                title: 'ERRO',
                message: 'Erro na requisição',
            });
        });
    }
    return {
        init: init,
        showHorarios: showHorarios,
        addDay: addDay
    };
})();

