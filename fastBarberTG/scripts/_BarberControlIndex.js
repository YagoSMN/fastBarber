window.BarberControl = (function () {

    var config = {
        urls: {
            userSettings: '',
            buscarColaborador: '',
            desmarcarCorte: '',
            finalizarCorte: '',
            buscarDiaSemana: '',
            iniciarCorte: ''
        },
    };

    var init = function ($config) {
        config = $config;
    };

    function obterDiaDaSemana(dataStr) {

        var data = parseDate(dataStr);
        var diasDaSemana = ['Domingo', 'Segunda-feira', 'Terça-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sábado'];
        var numeroDoDia = data.getDay();
        var nomeDoDia = diasDaSemana[numeroDoDia];
        $("#NomeDiaSemana").text(nomeDoDia);
        var dia = data.getDate();
        var mes = data.getMonth() + 1;
        var year = data.getFullYear();
        $("#DataDaSemana").text((dia < 10 ? '0' : '') + dia + '/' + (mes < 10 ? '0' : '') + mes + '/' + year);
    }

    function parseDate(str) {
        var parts = str.split('/');
        return new Date(parts[2], parts[1] - 1, parts[0]);
    }

    $(document).ready(function () {
        obterDiaDaSemana(new Date().toLocaleDateString('pt-BR'));
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
            $(`[data-horarioId="${id}"] .circle-status`)
                .removeClass("yellow")
                .addClass("red");
        }).fail(function () {
            iziToast.error({
                title: 'Error',
                message: "Erro na requisição",
            });
        });
    }

    function verificaContador(id) {
        var model = JSON.parse(localStorage.getItem("contador"));

        if (model.Id != id)
            return;

        console.log(model.Id + '/' + id);

        if (model !== null && model.Id === id) {
            $("#buttons-control").hide();
            $("#timer-control").show();
            var HoraInicio = new Date(model.HoraInicio);
            var HoraAtual = new Date();
            var diferenca = Math.floor((HoraAtual.getTime() - HoraInicio.getTime()) / 1000);
            contadorSetInterval = setInterval(function () {
                diferenca++;
                $(`[data-horario="${id}"]`).text(formatarTempo(diferenca));
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

        $.get(config.urls.iniciarCorte, { Id: id }).done(function() {

        }).fail(function() {
            iziToast.error({
                title: 'Erro',
                message: "Falha ao iniciar corte!",
            });
            return;
        });

        localStorage.setItem('contador', JSON.stringify({ Id: id, HoraInicio: new Date() , Nome: nome, Horario: hora}));
        $("#buttons-control").hide("slow");
        $("#timer-control").show("slow");
        contador = 0;
        contadorSetInterval = setInterval(function() {
            contador++;
            $(`[data-horario="${id}"]`).text(formatarTempo(contador));
        },
        1000);

        $(`[data-horarioId="${id}"] .circle-status`)
            .removeClass("yellow")
            .addClass("cyan");
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
        $(`[data-horarioId="${id}"] .circle-status`)
            .removeClass("cyan")
            .addClass("green");

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

    var buscarDiaSemana = function() {
        $.get(config.urls.buscarDiaSemana, { data: $("#date").val() }).done(function(res) {
            $("#cortes-dia").empty().html(res);
            obterDiaDaSemana($("#date").val().split("-").reverse().join("/"));
            iziToast.success({
                color: 'blue',
                title: 'Sucesso!',
                message: `Dia ${$("#date").val().split("-").reverse().join("/")} selecionado!`,
            });
        }).fail(function() {
            iziToast.error({
                title: 'Error',
                message: "Erro ao buscar dia",
            });
        });
    }

    var returnHome = function() {
        $("#request-div").empty();
        $("#main-page").show("slow");
    }

    return {
        init: init,
        acessarColaborador: acessarColaborador,
        desmarcarCorte: desmarcarCorte,
        iniciarContador: iniciarContador,
        finalizarCorte: finalizarCorte,
        buscarDiaSemana: buscarDiaSemana,
        returnHome: returnHome
    };
})();
