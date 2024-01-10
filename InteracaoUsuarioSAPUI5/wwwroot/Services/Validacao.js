sap.ui.define([], () => {
    "use strict";

    const REGEX_NUMEROS = "[0-9]";
    let LISTA_ERROS = []

    return {
        _obterListaErros() {
            let separador = "\n";
            let erros = LISTA_ERROS.join(separador);
            LISTA_ERROS = [];

            return erros;
        },

        validarValorInicialCampos(reserva) {
            let nomeFormatado = reserva.nome.trim();

            if (nomeFormatado == "") LISTA_ERROS.push("Nome não preenchido");
            if (reserva.cpf == "") LISTA_ERROS.push("CPF não preenchido");
            if (reserva.telefone == "") LISTA_ERROS.push("Telefone não preenchido");
            if (reserva.idade == "") LISTA_ERROS.push("Idade não preenchida");
            if (reserva.checkIn == "") LISTA_ERROS.push("Check-in não preenchido");
            if (reserva.checkOut == "") LISTA_ERROS.push("Check-out não preenchido");
            if (reserva.precoEstadia == "") LISTA_ERROS.push("Preço da estadia não preenchido");

            return this._obterListaErros();
        },

        validarNome(nome) {
            let nomeFormatado = nome.trim();
            
            if (nomeFormatado == "") {
                LISTA_ERROS.push("Nome não preenchido");
                return "Nome não preenchido";
            }

            let regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            const tamanhoNome = nomeFormatado.length;
            const tamanhoMinimoNome = 3;
            const tamanhoMaximoNome = 50;

            if (tamanhoNome < tamanhoMinimoNome) {
                LISTA_ERROS.push("Nome muito pequeno");
                return "Nome muito pequeno";
            }
            else if (tamanhoNome > tamanhoMaximoNome) {
                LISTA_ERROS.push("Nome muito grande");
                return "Nome muito grande";
            };

            for (let char of nomeFormatado) {
                if (!char.match(regexNome)) {
                    LISTA_ERROS.push("Formato incorreto para nome");
                    return "Formato incorreto para nome";
                }
            }
        },

        validarCpf(cpf) {
            if (cpf == "") {
                LISTA_ERROS.push("CPF não preenchido");
                return "CPF não preenchido";
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
                return "CPF deve estar totalmente preenchido";
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
                return "Cpf é inválido";
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
                return "CPF é inválido";
            }
        },

        validarTelefone(telefone) {
            if (telefone == "") {
                LISTA_ERROS.push("Telefone não preenchido");
                return "Telefone não preenchido";
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
                return "Telefone deve estar totalmente preenchido";
            }
        },

        validarIdade(idade) {
            if (idade == "") {
                LISTA_ERROS.push("Idade não preenchida");
                return "Idade não preenchida";
            }

            let numeroIdade = Number(idade);

            if (numeroIdade < 18) {
                LISTA_ERROS.push("O cliente não pode ser menor de idade");
                return "O cliente não pode ser menor de idade";
            }
            else if (numeroIdade >= 200) {
                LISTA_ERROS.push("O cliente não pode ter mais de 200 anos");
                return "O cliente não pode ter mais de 200 anos";
            }
        },

        validarCheckIn(checkIn) {
            if (checkIn == "") {
                LISTA_ERROS.push("Check-in não preenchido");
                return "Check-in não preenchido";
            }

            let dataHoje = new Date();
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split("-");

            if (anoCheckIn < dataHoje.getFullYear()) {
                LISTA_ERROS.push("Data de check-in inválida");
                return "Data de check-in inválida";
            }
            else if ((anoCheckIn == dataHoje.getFullYear()) &&
                (mesCheckIn == (dataHoje.getMonth() + 1) && (diaCheckIn < dataHoje.getDate()))) {
                LISTA_ERROS.push("Data de check-in inválida");
                return "Data de check-in inválida";
            }
        },

        validarCheckOut(checkOut, checkIn) {
            if (checkIn == "") {
                LISTA_ERROS.push("Preencha primeiro o check-in");
                return "Preencha primeiro o check-in";
            }
            if (checkOut == "") {
                LISTA_ERROS.push("Check-out não preenchido");
                return "Check-out não preenchido";
            }

            let dataHoje = new Date();
            let [anoCheckOut, mesCheckOut, diaCheckOut] = checkOut.split("-");
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split("-");

            if (anoCheckOut < dataHoje.getFullYear()) {
                LISTA_ERROS.push("Data de check-out inválida");
                return "Data de check-out inválida";
            }
            else if ((anoCheckOut == dataHoje.getFullYear()) &&
                (mesCheckOut == (dataHoje.getMonth() + 1) && (diaCheckOut < dataHoje.getDate()))) {
                LISTA_ERROS.push("Data de check-out inválida");
                return "Data de check-out inválida";
            }
            else if ((anoCheckOut == anoCheckIn) &&
                (mesCheckOut == mesCheckIn && (diaCheckOut < diaCheckIn))) {
                LISTA_ERROS.push("Data de check-out inválida");
                return "Data de check-out inválida";
            }
        },

        validarPrecoEstadia(precoEstadia) {
            if (precoEstadia == "") {
                LISTA_ERROS.push("Preço da estadia não preenchido");
                return "Preço da estadia não preenchido";
            }

            let numeroPrecoEstadia = Number(precoEstadia);
            if (numeroPrecoEstadia > 9999999999.99) {
                LISTA_ERROS.push("Preço da estadia acima do permitido");
                return "Preço da estadia acima do permitido";
            }
            else if (numeroPrecoEstadia <= 0) {
                LISTA_ERROS.push("Preço da estadia não pode ser negativo ou zero");
                return "Preço da estadia não pode ser negativo ou zero";
            }
        }
    }
})