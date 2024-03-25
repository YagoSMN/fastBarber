window.fastBarber = (function () {

    var config = {
        urls: {
            newCostumer: '',
            searchHorario: '',

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
            Cpf: formatCpf($("#input-cpf").val()),
            DataNasc: $("#input-datanasc").val(),
            Tel: $("#input-tel").val(),
            Email: $("#input-email").val()
        }

        // validação de formulário
        if (!validateCPF(model.Cpf)) {
            iziToast.error({
                title: 'CPF inválido',
                message: 'Insira um CPF válido',
            });
            return
        }

        if (!model.Nome.length > 0) {
            iziToast.error({
                title: 'NOME inválido',
                message: 'Insira um NOME válido',
            });
            return 
        }

        if (!model.Sobrenome.length > 0) {
            iziToast.error({
                title: 'SOBRENOME inválido',
                message: 'Insira um SOBRENOME válido',
            });
            return
        }

        if (!model.Sobrenome.length > 0) {
            iziToast.error({
                title: 'SOBRENOME inválido',
                message: 'Insira um SOBRENOME válido',
            });
            return
        }

        if (!validateDate(model.DataNasc)) {
            iziToast.error({
                title: 'DATA DE NASCIMENTO inválida',
                message: 'Insira uma DATA DE NASCIMENTO válida',
            });
            return
        }

        $.post(config.urls.newCostumer, model).done(function (data) {

            if (data === 'Sucesso, Cliente cadastrado!') {
                iziToast.success({
                    color: 'blue',
                    title: 'Success',
                    message: data,
                });

                //limpando campos do formulário de cadastro cliente
                $("#cpf-cliente").val(model.Cpf);
                $("#input-Nome").val("");
                $("#input-subnome").val("");
                $("#input-cpf").val("");
                $("#input-datanasc").val("");
                $("#input-tel").val("");
                $("#input-email").val("");

                $("#DontHaveAcc").hide();
                $("#HaveAcc").show("slow");

            }

            if (data === 'Cadastro inválido, Cliente já existe!') {
                iziToast.error({
                    title: 'Error',
                    message: "CPF já cadastrado!",
                });
            }

        }).fail(function (msg) {
            alert("Erro na requisição!" + msg)
        })
    }

    var searchHorario = function () {
        event.preventDefault();
        var cpf = formatCpf($("#cpf-input").val());

        if (!validateCPF(cpf)) {
            iziToast.error({
                title: 'CPF inválido',
                message: 'Insira um CPF válido',
            });
            return
        }

        var model = {
            Id: 0,
            Cpf: cpf,
            Nome: '',
            Sobrenome: '',
            DataNasc: '',
            Tel: '',
            Email: ''
        }

        $.get(config.urls.searchHorario, model).done(function (html) {
            $("#HaveAcc").hide();
            $("#response").html(html);
            $("#search-cpfcliente").val(cpf);
            $("#response").show("slow");

        }).fail(function (msg) {
            alert("Erro na requisição!");
        })
    }

    function validateCPF(cpf) {
        // Remove any non-digit character
        cpf = cpf.toString();
        // Check if the CPF has 11 digits
        if (cpf.length !== 11) {
            return false;
        }

        // Check for repeated digits (e.g., 111.111.111-11)
        if (/^(\d)\1+$/.test(cpf)) {
            return false;
        }

        // Validate the CPF using the algorithm
        let sum = 0;
        let remainder;

        for (let i = 1; i <= 9; i++) {
            sum += parseInt(cpf.charAt(i - 1)) * (11 - i);
        }

        remainder = (sum * 10) % 11;

        if ((remainder === 10) || (remainder === 11)) {
            remainder = 0;
        }

        if (remainder !== parseInt(cpf.charAt(9))) {
            return false;
        }

        sum = 0;
        for (let i = 1; i <= 10; i++) {
            sum += parseInt(cpf.charAt(i - 1)) * (12 - i);
        }

        remainder = (sum * 10) % 11;

        if ((remainder === 10) || (remainder === 11)) {
            remainder = 0;
        }

        if (remainder !== parseInt(cpf.charAt(10))) {
            return false;
        }

        return true;
    }

    function applyCpfMask(input) {
        // Remove non-digit characters
        var cleanedValue = input.value.replace(/\D/g, '');

        // Apply the CPF mask
        if (cleanedValue.length > 3) {
            cleanedValue = cleanedValue.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
        }

        // Update the input value
        input.value = cleanedValue;
    }

    function formatCpf(cpfSemFormato) {
        var cpfFormatado = cpfSemFormato.replace(/\D/g, '')
        return Number(cpfFormatado);
    }

    function applyPhoneMask(input) {
        var cleanedValue = input.value.replace(/\D/g, '');

        if (cleanedValue.length >= 10) {
            cleanedValue = `(${cleanedValue.substring(0, 2)}) ${cleanedValue.substring(2, 7)}-${cleanedValue.substring(7)}`;
        }
        input.value = cleanedValue;
    }

    function validateDate(data) {
        // Convert the input date string to a Date object
        const selectedDate = new Date(data);

        // Get the current date
        const currentDate = new Date();

        // Calculate the current year
        const currentYear = currentDate.getFullYear();

        // Calculate the birth year
        const birthYear = selectedDate.getFullYear();

        // Calculate the age
        const age = currentYear - birthYear;

        // Check if the age is 14 or more
        if (age >= 14) {
            return true;
        } else {
            return false;
        }
    }

    var Retornar = function () {
        $("#final-screen").hide();
        $("#Question").show();
    }

    return {
        init: init,
        cadastrarCostumer: cadastrarCostumer, 
        applyCpfMask: applyCpfMask,
        applyPhoneMask: applyPhoneMask,
        searchHorario: searchHorario,
        Retornar: Retornar
    };
})();

