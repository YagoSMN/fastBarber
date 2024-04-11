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

            var horasExistem = [];
            console.log(response);
            response.forEach(function (corte) {
                var hora = corte.DataCorte.split(" ")[1];
                var partesHora = hora.split(":");
                var horaDecimal = parseInt(partesHora[0]) + (parseInt(partesHora[1]) / 60);
                horasExistem.push(horaDecimal);
            });

            console.log(horasExistem);

            var horasDesejadas = [8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 13, 13.5, 14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 20, 20.5, 21];

            var horasNaoMarcadas = horasDesejadas.filter(function (hora) {
                var horaArredondada = Math.round(hora * 2) / 2;
                return !horasExistem.includes(horaArredondada);
            });

            horasNaoMarcadas.forEach(function (hora) {
                var div = `<div class="hora-marcada" onclick="Corte.AdicionarHora(this, '${$(a).attr('data-diaSemana').substr(0, 10)}', '${formatarHora(hora)}')"><p>${formatarHora(hora)}</p><div class="circle-green"></div></div>`;
                $("#grid-horas").append(div);
            });
            $("#grid-horas").show("slow");
        }).fail(function () {

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
                message: 'Ao buscar horários disponiveis!',
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

