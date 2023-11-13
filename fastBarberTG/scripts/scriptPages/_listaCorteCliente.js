window.Corte = (function () {

    var config = {
        urls: {
            desmarcarCorte: '',
            BuscaOcupado: ''
        },
    };

    var init = function ($config) {
        config = $config;
    };

    var DesmarcarCorte = function () {
        debugger;
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

    var BuscaOcupado = function (data) {
        $.get(config.urls.BuscaOcupado, { data: data.toString().substring(0, 10)}).done(function (response) {
            $(".searchHorario").removeClass("bg-highlight");
            $("#grid-horas").show("slow");
            $(this).addClass("bg-highlight");
            $("#grid-horas").html(" ");
            console.log(response)
            var horasExistem = [];
            response.forEach(function (corte) {
                var hora = corte.DataCorte.split(" ")[1];
                var horaDecimal = hora.substr(0, 2);
                horasExistem.push(parseInt(horaDecimal));
            });

            console.log(horasExistem);
            var horasDesejadas = [8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 13, 13.5, 14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19];

            var horasNaoMarcadas = horasDesejadas.filter(function (hora) {
                var horaArredondada = Math.round(hora * 2) / 2;
                return horasExistem.includes(horaArredondada) === false;
            });

            horasNaoMarcadas.forEach(function (hora) {
                var div = `<div class="hora-marcada" onclick="Corte.AdicionarHora(${data}, ${formatarHora(hora)})"><p>${formatarHora(hora)}</p><div class="circle-green"></div></div>`;
                $("#grid-horas").append(div);
            });
        }).fail(function () {

        });
    }

    function formatarHora(hora) {
        var horasInteiras = Math.floor(hora);
        var minutos = hora % 1 === 0.5 ? "30" : "00";

        return horasInteiras + ":" + minutos;
    }

    var AdicionarHora = function (data, hora) {

    }

    return {
        init: init,
        DesmarcarCorte: DesmarcarCorte,
        Nofunction: Nofunction,
        BuscaOcupado: BuscaOcupado,
        AdicionarHora: AdicionarHora
    };
})();

