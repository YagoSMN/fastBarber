window.BarberControl = (function () {

    var config = {
        urls: {
            userSettings: '',
            buscarColaborador: '',
            desmarcarCorte: '',
            finalizarCorte: ''
        },
    };

    var init = function ($config) {
        config = $config;
    };

    function obterDiaDaSemana() {
        var dataAtual = new Date();
        var diasDaSemana = ['Domingo', 'Segunda-feira', 'Terça-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sábado'];
        var numeroDoDia = dataAtual.getDay();
        var nomeDoDia = diasDaSemana[numeroDoDia];
        $("#NomeDiaSemana").text(nomeDoDia);


        var dia = dataAtual.getDate();
        var mes = dataAtual.getMonth() + 1;
        $("#DataDaSemana").text((dia < 10 ? '0' : '') + dia + '/' + (mes < 10 ? '0' : '') + mes);
    }

    $(document).ready(function () {
        obterDiaDaSemana();
    });

    var acessarColaborador = function (id) {
        $.get(config.urls.buscarColaborador,
        {
            id: id 
        }).done(function (html) {
            $("#main-page").hide("slow");
            $("#request-div").show("slow").html(html);
            verificaContador(id);
        }).fail(function(msg) {
            iziToast.error(msg);
        });
    };

    var desmarcarCorte = function (id) {
        $.post(config.urls.desmarcarCorte, { id: id }).done(function () {
            iziToast.success({
                color: 'blue',
                title: 'Sucesso!',
                message: 'corte desmarcado',
            });
            $("#status-corte").removeClass("yellow").addClass("red");
            $("#buttons-control").hide();
            $("#corte-cancelado").removeClass("uk-none");
        }).fail(function () {
            iziToast.error({
                title: 'Error',
                message: "Erro na requisição",
            });
        });
    }

    function verificaContador(id) {
        var model = JSON.parse(localStorage.getItem("contador"));

        if (model !== null && model.Id === id) {
            $("#buttons-control").hide();
            $("#timer-control").show();
            var HoraInicio = new Date(model.HoraInicio);
            var HoraAtual = new Date();
            var diferenca = Math.floor((HoraAtual.getTime() - HoraInicio.getTime()) / 1000);
            contadorSetInterval = setInterval(function () {
                diferenca++;
                $("#tempo-decorrido").text(formatarTempo(diferenca));
            },
            1000);
        }
    }

    var iniciarContador = function (id, nome, hora) {
        var model = JSON.parse(localStorage.getItem("contador"));
        if (model != null && model.Id != id) {
            iziToast.error({
                title: 'Error',
                message: `Você já possui o corte de ${nome} no horário ${hora} Rodando!`
            });
            return;
        }

        localStorage.setItem('contador', JSON.stringify({ Id: id, HoraInicio: new Date() , Nome: nome, Horario: hora}));
        $("#buttons-control").hide("slow");
        $("#timer-control").show("slow");
        contador = 0;
        contadorSetInterval = setInterval(function() {
            contador++;
            $("#tempo-decorrido").text(formatarTempo(contador));
        },
        1000);
    };

    var finalizarCorte = function(id) {
        clearInterval(contadorSetInterval);
        localStorage.removeItem('contador');
        $.post(config.urls.finalizarCorte, { Id: id, TempoCorte: $("#tempo-decorrido").text() }).done(function() {
            iziToast.success({
                color: 'blue',
                title: 'Sucesso!',
                message: 'Corte Finalizado!',
            });
            $("#timer-control").hide("slow");
            $("#corte-concluido").removeClass("uk-none");

            $("#status-corte").removeClass("yellow").addClass("green");
        }).fail(function() {
            iziToast.error({
                title: 'Error',
                message: "Erro na requisição",
            });
        });

    };

    function formatarTempo(contador) {
        // Calcula o número de horas
        var horas = Math.floor(contador / 3600);
        // Calcula o número de minutos restantes
        var minutosRestantes = Math.floor((contador % 3600) / 60);
        // Calcula o número de segundos restantes
        var segundos = contador % 60;

        // Adiciona um zero à esquerda se o número for menor que 10
        var horasFormatadas = horas < 10 ? "0" + horas : horas;
        var minutosFormatados = minutosRestantes < 10 ? "0" + minutosRestantes : minutosRestantes;
        var segundosFormatados = segundos < 10 ? "0" + segundos : segundos;

        return horasFormatadas + ":" + minutosFormatados + ":" + segundosFormatados;
    }

    return {
        init: init,
        acessarColaborador: acessarColaborador,
        desmarcarCorte: desmarcarCorte,
        iniciarContador: iniciarContador,
        finalizarCorte: finalizarCorte
    };
})();
