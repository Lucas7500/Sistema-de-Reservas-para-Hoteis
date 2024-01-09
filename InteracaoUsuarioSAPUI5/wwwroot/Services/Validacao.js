sap.ui.define([], () => {
    "use strict";

    const REGEX_NUMEROS = "[0-9]";
    let LISTA_ERROS = []

    return {
        validarValorInicialCampos(reserva) {
        },

        validarNome(nome) {
            let nomeFormatado = nome.trim();
            if (nomeFormatado == "") {
                LISTA_ERROS.push("Nome não preenchido");
                return false;
            }

            let regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            const tamanhoNome = nomeFormatado.length;
            const tamanhoMinimoNome = 3;
            const tamanhoMaximoNome = 50;

            if (tamanhoNome < tamanhoMinimoNome) {
                LISTA_ERROS.push("Nome muito pequeno");
                return false;
            }
            else if (tamanhoNome > tamanhoMaximoNome) {
                LISTA_ERROS.push("Nome muito grande");
                return false;
            };

            for (let char of nomeFormatado) {
                if (!char.match(regexNome)) {
                    LISTA_ERROS.push("Formato incorreto para nome");
                    return false;
                }
            }

            return true;
        },

        validarCpf(cpf) {
            if (cpf == "") {
                LISTA_ERROS.push("CPF não preenchido");
                return false;
            };

            let numerosCpf = "";

            for (let char of cpf) {
                if (char.match(REGEX_NUMEROS)) {
                    numerosCpf += char;
                }
            }

            const tamanhoNumerosCpf = numerosCpf.length;
            const tamanhoCpfPreenchido = 11;
            if (tamanhoNumerosCpf < tamanhoCpfPreenchido) {
                LISTA_ERROS.push("CPF deve estar totalmente preenchido");
                return false;
            }

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
                LISTA_ERROS.push("Cpf é inválido");
                return false;
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
                LISTA_ERROS.push("CPF é inválido");
                return false;
            }

            return true;
        },

        validarTelefone(telefone) {
            if (telefone == "") {
                LISTA_ERROS.push("Telefone não preenchido");
                return false;
            }

            let numerosTelefone = "";

            for (let char of telefone) {
                if (char.match(REGEX_NUMEROS)) {
                    numerosTelefone += char;
                }
            }

            const tamanhoNumerosTelefone = numerosTelefone.length;
            const tamanhoTelefonePreenchido = 11;

            if (tamanhoNumerosTelefone < tamanhoTelefonePreenchido) {
                LISTA_ERROS.push("Telefone deve estar totalmente preenchido");
                return false;
            }
        },

        validarIdade(idade) {
            if (idade == "") {
                LISTA_ERROS.push("Idade não preenchida");
                return false;
            }

            let numeroIdade = Number(idade);

            if (numeroIdade < 18) {
                LISTA_ERROS.push("O cliente não pode ser menor de idade");
                return false;
            }
            else if (numeroIdade >= 200) {
                LISTA_ERROS.push("O cliente não pode ter mais de 200 anos");
                return false;
            }

            return true;
        },

        validarCheckIn(checkIn) {
            if (checkIn == "") {
                LISTA_ERROS.push("Check-in não preenchido");
                return false;
            }

            let dataHoje = new Date();
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split("-");

            if (anoCheckIn < dataHoje.getFullYear()) {
                LISTA_ERROS.push("Data de check-in inválida");
                return false;
            }
            else if ((anoCheckIn == dataHoje.getFullYear()) &&
                (mesCheckIn == (dataHoje.getMonth() + 1) && (diaCheckIn < dataHoje.getDate()))) {
                LISTA_ERROS.push("Data de check-in inválida");
                return false;
            }

            return true;
        },

        validarCheckOut(checkOut, checkIn) {
            if (checkIn == "") {
                LISTA_ERROS.push("Preencha primeiro o check-in");
                return false;
            }
            if (checkOut == "") {
                LISTA_ERROS.push("Check-out não preenchido");
                return false;
            }

            let dataHoje = new Date();
            let [anoCheckOut, mesCheckOut, diaCheckOut] = checkOut.split("-");
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split("-");

            if (anoCheckOut < dataHoje.getFullYear()) {
                LISTA_ERROS.push("Data de check-in inválida");
            }
            else if ((anoCheckOut == dataHoje.getFullYear()) &&
                (mesCheckOut == (dataHoje.getMonth() + 1) && (diaCheckOut < dataHoje.getDate()))) {
                LISTA_ERROS.push("Data de check-in inválida");
                return false;
            }
            else if ((anoCheckOut == anoCheckIn) &&
                (mesCheckOut == mesCheckIn && (diaCheckOut < diaCheckIn))) {
                LISTA_ERROS.push("Data de check-out inválida");
                return false;
            }

            return true;
        },

        validarPrecoEstadia(precoEstadia) {
            if (precoEstadia == "") {
                LISTA_ERROS.push("Preço da estadia não preenchido");
                return false;
            }

            let numeroPrecoEstadia = Number(precoEstadia);
            if (numeroPrecoEstadia > 9999999999.99) {
                LISTA_ERROS.push("Preço da estadia acima do permitido");
                return false;
            }
            else if (numeroPrecoEstadia <= 0) {
                LISTA_ERROS.push("Preço da estadia não pode ser negativo ou zero");
                return false;
            }

            return true;
        }
    }
})