window.fastBarber = (function () {

    var config = {
        urls: {
            newCostumer: ''
        },
    };

    var init = function ($config) {
        config = $config;
    };

    var cadastrarCostumer = function () {
        event.preventDefault();
        var model = {
            Id: 0,
            Nome: $("#input-Nome").val(),
            Sobrenome: $("#input-subnome").val(),
            Cpf: $("#input-cpf").val(),
            DataNasc: $("#input-datanasc").val(),
            Tel: $("#input-tel").val(),
            Email: $("#input-email").val()
        }

        $.post(config.urls.newCostumer, model).done(function (data) {
            alert("" + data);
        }).fail(function (msg) {
            alert("Erro na requisição!" + msg)
        })
    }

    return {
        init: init,
        cadastrarCostumer: cadastrarCostumer
    };
})();

