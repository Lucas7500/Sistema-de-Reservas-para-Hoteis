sap.ui.define([], () => {
    "use strict";

    const REGEX_NUMEROS = "[0-9]";

    let LISTA_ERROS = [];
    const INDICE_MENSAGEM_ERRO_NOME = 0;
    const INDICE_MENSAGEM_ERRO_CPF = 1;
    const INDICE_MENSAGEM_ERRO_TELEFONE = 2;
    const INDICE_MENSAGEM_ERRO_IDADE = 3;
    const INDICE_MENSAGEM_ERRO_CHECK_IN = 4;
    const INDICE_MENSAGEM_ERRO_CHECK_OUT = 5;
    const INDICE_MENSAGEM_ERRO_PRECO_ESTADIA = 6;

    return {
        obterListaErros() {
            let erros = LISTA_ERROS;
            LISTA_ERROS = [];

            return erros;
        },

        contemValor(propriedade) {
            return Boolean(propriedade);
        },

        validarPropriedadesSemAlteracao(reserva) {
            let nomeFormatado = reserva.nome.trim();

            if (!this.contemValor(nomeFormatado))
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = "Nome não preenchido";

            if (!this.contemValor(reserva.cpf))
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] = "CPF não preenchido";

            if (!this.contemValor(reserva.telefone))
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE] = "Telefone não preenchido";

            if (!this.contemValor(reserva.idade))
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = "Idade não preenchida";

            if (!this.contemValor(reserva.checkIn))
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = "Check-in não preenchido";

            if (!this.contemValor(reserva.checkOut))
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = "Check-out não preenchido";

            if (!this.contemValor(reserva.precoEstadia))
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = "Preço da estadia não preenchido";
        },

        validarNome(nome) {
            let nomeFormatado = nome.trim();

            if (!this.contemValor(nomeFormatado)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = "Nome não preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
            }

            let regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            const tamanhoNome = nomeFormatado.length;
            const tamanhoMinimoNome = 3;
            const tamanhoMaximoNome = 50;

            if (tamanhoNome < tamanhoMinimoNome) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = "Nome muito pequeno";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
            }
            else if (tamanhoNome > tamanhoMaximoNome) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = "Nome muito grande";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
            };

            for (let char of nomeFormatado) {
                if (!char.match(regexNome)) {
                    LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = "Formato incorreto para nome";
                    return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
                }
            }
        },

        validarCpf(cpf) {
            if (!this.contemValor(cpf)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] = "CPF não preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF];
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
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] = "CPF deve estar totalmente preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF];
            }

            const multiplicadoresPrimeiroDigito = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            const multiplicadoresSegundoDigito = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

            let arrayDigitosCpf = Array.from(String(numerosCpf), Number);
            let primeiroDigitoVerificador = arrayDigitosCpf[9];
            let segundoDigitoVerificador = arrayDigitosCpf[10]
            let somaPrimeiroDigito = 0;
            let somaSegundoDigito = 0;

            for (let i = 0; i < arrayDigitosCpf.length; i++) {

                if (i < multiplicadoresPrimeiroDigito.length) {
                    somaPrimeiroDigito += arrayDigitosCpf[i] * multiplicadoresPrimeiroDigito[i];
                }

                if (i < multiplicadoresSegundoDigito.length) {
                    somaSegundoDigito += arrayDigitosCpf[i] * multiplicadoresSegundoDigito[i];
                }
            }

            let restoPrimeiroDigito = somaPrimeiroDigito % 11;
            let restoSegundoDigito = somaSegundoDigito % 11;

            let primeiroCasoInvalido = restoPrimeiroDigito < 2 && primeiroDigitoVerificador != 0;
            let segundoCasoInvalido = restoPrimeiroDigito >= 2 && primeiroDigitoVerificador != (11 - restoPrimeiroDigito);
            let terceiroCasoInvalido = restoSegundoDigito < 2 && segundoDigitoVerificador != 0;
            let quartoCasoInvalido = restoSegundoDigito >= 2 && segundoDigitoVerificador != (11 - restoSegundoDigito);

            if (primeiroCasoInvalido || segundoCasoInvalido || terceiroCasoInvalido || quartoCasoInvalido) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] = "Cpf é inválido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF];
            }
        },

        validarTelefone(telefone) {
            if (!this.contemValor(telefone)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE] = "Telefone não preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE];
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
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE] = "Telefone deve estar totalmente preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE];
            }
        },

        validarIdade(idade) {
            if (!this.contemValor(idade)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = "Idade não preenchida";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE];
            }

            let numeroIdade = Number(idade);
            const valorMinimoIdade = 18;
            const valorMaximoIdade = 200;

            if (numeroIdade < valorMinimoIdade) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = "O cliente não pode ser menor de idade";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE];
            }
            else if (numeroIdade >= valorMaximoIdade) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = "O cliente não pode ter mais de 200 anos";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE];
            }
        },

        validarCheckIn(checkIn) {
            if (!this.contemValor(checkIn)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = "Check-in não preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN];
            }

            let dataAtual = new Date();
            let anoAtual = dataAtual.getFullYear();
            let mesAtual = dataAtual.getMonth() + 1;
            let diaAtual = dataAtual.getDate();

            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split("-");

            if (anoCheckIn < anoAtual) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = "Data de check-in inválida";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN];
            }
            else if ((anoCheckIn == anoAtual) && (mesCheckIn == mesAtual) && (diaCheckIn < diaAtual)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = "Data de check-in inválida";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN];
            }
        },

        validarCheckOut(checkOut, checkIn) {
            if (!this.contemValor(checkOut)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = "Check-out não preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT];
            }

            let dataAtual = new Date();
            let anoAtual = dataAtual.getFullYear();
            let mesAtual = dataAtual.getMonth() + 1;
            let diaAtual = dataAtual.getDate();

            const separador = "-";
            let [anoCheckOut, mesCheckOut, diaCheckOut] = checkOut.split(separador);
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split(separador);

            if (anoCheckOut < anoAtual) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = "Data de check-out inválida";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT];
            }
            else if ((anoCheckOut == anoAtual) && (mesCheckOut == mesAtual) && (diaCheckOut < diaAtual)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = "Data de check-out inválida";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT];
            }
            else if ((anoCheckOut == anoCheckIn) && (mesCheckOut == mesCheckIn) && (diaCheckOut < diaCheckIn)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = "Check-out não pode ser anterior ao Check-in";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT];
            }
        },

        validarPrecoEstadia(precoEstadia) {
            if (!this.contemValor(precoEstadia)) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = "Preço da estadia não preenchido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA];
            }

            let numeroPrecoEstadia = Number(precoEstadia);
            const valorMaximoPrecoEstadia = 9999999999.99;
            const valorZero = 0;

            if (numeroPrecoEstadia > valorMaximoPrecoEstadia) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = "Preço da estadia acima do permitido";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA];
            }
            else if (numeroPrecoEstadia <= valorZero) {
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = "Preço da estadia não pode ser negativo ou zero";
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA];
            }
        }
    }
})