﻿window.BarberControl = (function () {

    var config = {
        urls: {
            userSettings: ''
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
        var formatoMMDD = (mes < 10 ? '0' : '') + mes + '/' + (dia < 10 ? '0' : '') + dia;
        var diaMes = formatoMMDD;
        $("#DataDaSemana").text(diaMes);
    }


    $(document).ready(function () {
        obterDiaDaSemana();
    });

    return {
        init: init,
    };
})();
