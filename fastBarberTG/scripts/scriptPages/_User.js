window.User = (function () {

    var config = {
        urls: {
            showHorarios: '',
            buscarDiaSemana: '',
            salvarDiaSemana: ''
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
        }).fail(function (msg) {
            $("#form-week").empty();
            iziToast.error({
                title: 'ERRO',
                message: 'Erro na requisição',
            });
        });
    }

    var salvarDiaSemana = function () {
        event.preventDefault();
        var model = {
            Id: $("#form-salvardia").data("id"),
            Horario_Inicio: $("#form-salvardia  input[name='HorarioInicio']").val(),
            Horario_AlmocoInicio: $("#form-salvardia  input[name='HorarioInicioAlmoco']").val(),
            Horario_AlmocoFim:  $("#form-salvardia  input[name='HorarioFimAlmoco']").val(),
            Horario_Fim: $("#form-salvardia  input[name='HorarioFim']").val(),
            Ind_Ativo: ($("#form-salvardia  input[name='Ind_Ativo']").prop("checked") ? 'S' : 'N'),
        }

        $.post(config.urls.salvarDiaSemana, model).done(function() {
            iziToast.success({
                title: "Sucesso!",
                message: "ao salvar dia da semana"
            });
            $("#form-salvardia  input[name='HorarioInicio']").attr("disabled", true);
            $("#form-salvardia  input[name='HorarioInicioAlmoco']").attr("disabled", true);
            $("#form-salvardia  input[name='HorarioFimAlmoco']").attr("disabled", true);
            $("#form-salvardia  input[name='HorarioFim']").attr("disabled", true);
            $("#form-salvardia  input[name='Ind_Ativo']").attr("disabled", true);
        }).fail(function(msg) {
            iziToast.error({
                title: 'ERRO',
                message: 'Erro na requisição',
            });
        });
    }

    return {
        init: init,
        showHorarios: showHorarios,
        addDay: addDay,
        salvarDiaSemana: salvarDiaSemana
    };
})();

