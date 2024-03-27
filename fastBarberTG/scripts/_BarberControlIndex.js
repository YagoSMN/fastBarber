window.BarberControl = (function () {

    var config = {
        urls: {
            userSettings: '',
            buscarColaborador: ''
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
        }).fail(function(msg) {
            iziToast.error(msg);
        });
    };

    var DesmarcarCorte = function (id) {
        $.post(config.urls.desmarcarCorte, { id: id }).done(function () {
            iziToast.success({
                color: 'blue',
                title: 'Sucesso!',
                message: 'corte desmarcado',
            });
        }).fail(function () {
            iziToast.error({
                title: 'Error',
                message: "Erro na requisição",
            });
        });
    }

    return {
        init: init,
        acessarColaborador: acessarColaborador
    };
})();
