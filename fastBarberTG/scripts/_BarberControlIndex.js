window.BarberControl = (function () {

    var config = {
        urls: {
            userSettings: '',
            buscarCustumer: ''
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

    var acessarColaborador = function(id) {
        $.get(config.urls.buscarCustumer, { id: id}).done(function(html) {
            $("#main-page").hide("slow");
            $("#request-div").show("slow").html(html);
        }).fail(function(msg) {
            iziToast.error(msg);
        });
    };

    return {
        init: init,
        acessarColaborador: acessarColaborador
    };
})();
