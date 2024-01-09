sap.ui.define([], () => {
    "use strict";

    const REGEX_NUMEROS = "[0-9]";
    let LISTA_ERROS = []

    return {
        validarValorInicialCampos(reserva) {
        },

        validarNome(nome) {
            let nomeFormatado = nome.trim();
            if (nomeFormatado == "") throw "Nome não preenchido"

            let regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            const tamanhoNome = nomeFormatado.length;
            const tamanhoMinimoNome = 3;
            const tamanhoMaximoNome = 50;

            if (tamanhoNome < tamanhoMinimoNome) throw "Nome muito pequeno";
            if (tamanhoNome > tamanhoMaximoNome) throw "Nome muito grande";

            for (let char of nomeFormatado) {
                if (!char.match(regexNome)) {
                    throw "Formato incorreto para nome"
                }
            }
        },

        validarCpf(cpf) {
            if (cpf == "") throw "CPF não preenchido";

            let numerosCpf = "";

            for (let char of cpf) {
                if (char.match(REGEX_NUMEROS)) {
                    numerosCpf += char;
                }
            }

            const tamanhoNumerosCpf = numerosCpf.length;
            const tamanhoCpfPreenchido = 11;
            if (tamanhoNumerosCpf < tamanhoCpfPreenchido) throw "CPF deve estar totalmente preenchido";

            let stringNumerosCpf = String(numerosCpf);
            const multiplicacoesPrimeiroDigito = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            let somaPrimeiroDigito = 0;
            let resto;

            for (let i = 0; i < multiplicacoesPrimeiroDigito.length; i++) {
                somaPrimeiroDigito += Number(stringNumerosCpf[i]) * multiplicacoesPrimeiroDigito[i];
            }

            let primeiroDigitoVerificador = Number(stringNumerosCpf[9]);
            resto = somaPrimeiroDigito % 11;

            if ((resto < 2 && primeiroDigitoVerificador != 0) ||
                (resto >= 2 && primeiroDigitoVerificador != (11 - resto))) {
                throw "Cpf é inválido"
            }

            const multiplicacoesSegundoDigito = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
            let somaSegundoDigito = 0;

            for (let i = 0; i < multiplicacoesSegundoDigito.length; i++) {
                somaSegundoDigito += Number(stringNumerosCpf[i]) * multiplicacoesSegundoDigito[i];
            }

            let segundoDigitoVerificador = Number(stringNumerosCpf[10]);
            resto = somaSegundoDigito % 11;

            if ((resto < 2 && segundoDigitoVerificador != 0) ||
                (resto >= 2 && segundoDigitoVerificador != (11 - resto))) {
                throw "CPF é inválido"
            }
        },

        validarTelefone(telefone) {
            if (telefone == "") throw "Telefone não preenchido";

            let numerosTelefone = "";

            for (let char of telefone) {
                if (char.match(REGEX_NUMEROS)) {
                    numerosTelefone += char;
                }
            }

            const tamanhoNumerosTelefone = numerosTelefone.length;
            const tamanhoTelefonePreenchido = 11;

            if (tamanhoNumerosTelefone < tamanhoTelefonePreenchido) throw "Telefone deve estar totalmente preenchido";
        },

        validarIdade(idade) {
            if (idade == "") throw "Idade não preenchida"

            let numeroIdade = Number(idade);
            if (numeroIdade < 18) throw "O cliente não pode ser menor de idade";
            if (numeroIdade >= 200) throw "O cliente não pode ter mais de 200 anos"
        },

        validarCheckIn(checkIn) {
            if (checkIn == "") throw "Check-in não preenchido";

            let dataHoje = new Date();
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split("-");

            if (anoCheckIn < dataHoje.getFullYear()) {
                throw "Data de check-in inválida"
            }
            else if ((anoCheckIn == dataHoje.getFullYear()) &&
                (mesCheckIn == (dataHoje.getMonth() + 1) && (diaCheckIn < dataHoje.getDate()))) {
                throw "Data de check-in inválida";
            }
        },

        validarCheckOut(checkOut, checkIn) {
            if (checkIn == "") throw "Preencha primeiro o check-in"
            if (checkOut == "") throw "Check-out não preenchido";

            let dataHoje = new Date();
            let [anoCheckOut, mesCheckOut, diaCheckOut] = checkOut.split("-");
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split("-");

            if (anoCheckOut < dataHoje.getFullYear()) {
                throw "Data de check-in inválida"
            }
            else if ((anoCheckOut == dataHoje.getFullYear()) &&
                (mesCheckOut == (dataHoje.getMonth() + 1) && (diaCheckOut < dataHoje.getDate()))) {
                throw "Data de check-in inválida";
            }
            else if ((anoCheckOut == anoCheckIn) &&
                (mesCheckOut == mesCheckIn && (diaCheckOut < diaCheckIn))) {
                throw "Data de check-out inválida";
            }
        },

        validarPrecoEstadia(precoEstadia) {
            if (precoEstadia == "") throw "Preço da estadia não preenchido";

            let numeroPrecoEstadia = Number(precoEstadia);
            if (numeroPrecoEstadia > 9999999999.99) throw "Preço da estadia acima do permitido";
            if (numeroPrecoEstadia <= 0) throw "Preço da estadia não pode ser negativo ou zero";
        }
    }
})