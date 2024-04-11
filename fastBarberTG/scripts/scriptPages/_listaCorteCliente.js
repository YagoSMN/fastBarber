window.Corte = (function () {

    var config = {
        urls: {
            desmarcarCorte: '',
            BuscaOcupado: '',
            MarcarHorario: '',
            buscarCalendario: ''
        },
    };

    var init = function ($config) {
        config = $config;
    };

    var DesmarcarCorte = function () {
        var id = +$("#id-horariomarc").val();
        $.post(config.urls.desmarcarCorte, { id : id}).done(function () {
            iziToast.success({
                color: 'blue',
                title: 'Sucesso!',
                message: 'corte desmarcado',
            });
            $("#response").hide();
            $("#Question").show();
            $("#response").html(" ");
            
        }).fail(function () {
            iziToast.error({
                title: 'Error',
                message: "Erro na requisição",
            });
        });
    }

    var Nofunction = function () {
        $("#response").hide();
        $("#Question").show();
        $("#response").html(" ");
    }

    var BuscaOcupado = function (a) {
        $.get(config.urls.BuscaOcupado, { data: $(a).attr('data-diaSemana').substring(0, 10) }).done(function (response) {
            $("[data-diaSemana]").removeClass("bg-highlight");
            $("#grid-horas").html(" ");
            $(a).addClass("bg-highlight");
            $("#grid-horas").html(response).show("slow");
        }).fail(function () {
            iziToast.error({
                title: 'Erro',
                message: 'Ao buscar horários disponiveis!',
            });
        });
    }

    var buscarCalendario = function() {
        $.get(config.urls.buscarCalendario).done(function(html) {
            $(".fastbarber-calendar").show("slow");
            $("#grid-calendario").html(html);
            $("#marcar-horarios").removeClass("uk-none");
            $("#buscar-horarios").hide("");
        }).fail(function(msg) {
            iziToast.error({
                title: 'Erro',
                message: 'Ao buscar os dias disponiveis!',
            });
        });
    }

    function formatarHora(hora) {
        var horasInteiras = Math.floor(hora);
        var minutos = hora % 1 === 0.5 ? "30" : "00";

        return horasInteiras + ":" + minutos;
    }

    var AdicionarHora = function (x, data, hora) {
        $("#addhora-corte").val(data + " " + hora);
        $(".hora-marcada").removeClass("div-highlight");

        $(x).addClass("div-highlight");
    }

    var MarcarHorario = function () {
        var data = $("#addhora-corte").val();
        var cpf = $("#search-cpfcliente").val();

        if (!data) {
            iziToast.error({
                title: 'Erro',
                message: 'Você precisa selecionar um horário!',
            });

            return;
        }

        $.post(config.urls.MarcarHorario, { cpf: cpf, data: data }).done(function () {
            $("#final-screen").show("slow");
            $("#response").hide();
            iziToast.success({
                color: 'blue',
                title: 'Success',
                message: 'Horário marcado!',
            });
        }).fail(function () {
            iziToast.error({
                title: 'Erro',
                message: 'Erro ao marcar horário!',
            });
        })
    }

    return {
        init: init,
        DesmarcarCorte: DesmarcarCorte,
        Nofunction: Nofunction,
        BuscaOcupado: BuscaOcupado,
        AdicionarHora: AdicionarHora,
        MarcarHorario: MarcarHorario,
        buscarCalendario: buscarCalendario
    };
})();

