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
    let RECURSOS_I18N;

    return {
        definirRecursosi18n(recursosi18n) {
            RECURSOS_I18N = recursosi18n;
        },

        obterListaErros() {
            return LISTA_ERROS;
        },

        contemValor(propriedade) {
            return Boolean(propriedade);
        },

        validarPropriedadesVazias(reservaPreenchida) {
            let nomeFormatado = reservaPreenchida.nome.trim();

            if (!this.contemValor(nomeFormatado)) {
                const variavelNomeNaoPreenchido = "nomeNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = RECURSOS_I18N.getText(variavelNomeNaoPreenchido);
            }

            if (!this.contemValor(reservaPreenchida.cpf)) {
                const variavelCpfNaoPreenchido = "cpfNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] =  RECURSOS_I18N.getText(variavelCpfNaoPreenchido);
            }

            if (!this.contemValor(reservaPreenchida.telefone)) {
                const variavelTelefoneNaoPreenchido = "telefoneNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE] = RECURSOS_I18N.getText(variavelTelefoneNaoPreenchido);
            }

            if (!this.contemValor(reservaPreenchida.idade)) {
                const variavelIdadeNaoPreenchida = "idadeNaoPreenchida";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = RECURSOS_I18N.getText(variavelIdadeNaoPreenchida);
            }

            if (!this.contemValor(reservaPreenchida.checkIn)) {
                const variavelCheckInNaoPreenchido = "checkInNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = RECURSOS_I18N.getText(variavelCheckInNaoPreenchido);
            }

            if (!this.contemValor(reservaPreenchida.checkOut)) {
                const variavelCheckOutNaoPreenchido = "checkOutNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = RECURSOS_I18N.getText(variavelCheckOutNaoPreenchido);
            }

            if (!this.contemValor(reservaPreenchida.precoEstadia)) {
                const variavelPrecoEstadiaNaoPreenchido = "precoEstadiaNaoPreenchido"
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = RECURSOS_I18N.getText(variavelPrecoEstadiaNaoPreenchido);
            }
        },

        validarNome(nome) {
            let nomeFormatado = nome.trim();

            if (!this.contemValor(nomeFormatado)) {
                const variavelNomeNaoPreenchido = "nomeNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = RECURSOS_I18N.getText(variavelNomeNaoPreenchido);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
            }

            let regexNome = "^[a-zA-ZA-ZáàâãéèêíìîóòõôúùûçÁÀÃÂÉÈÊÍÌÎÓÒÔÕÚÙÛÇ ]*$";

            const tamanhoNome = nomeFormatado.length;
            const tamanhoMinimoNome = 3;
            const tamanhoMaximoNome = 50;

            if (tamanhoNome < tamanhoMinimoNome) {
                const variavelNomeCurto = "nomeCurto";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = RECURSOS_I18N.getText(variavelNomeCurto);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
            }
            else if (tamanhoNome > tamanhoMaximoNome) {
                const variavelNomeLongo = "nomeLongo";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = RECURSOS_I18N.getText(variavelNomeLongo);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
            };

            for (let char of nomeFormatado) {
                if (!char.match(regexNome)) {
                    const variavelNomeFormatoIncorreto = "nomeFormatoIncorreto";
                    LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = RECURSOS_I18N.getText(variavelNomeFormatoIncorreto);

                    return LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME];
                }
            }

            LISTA_ERROS[INDICE_MENSAGEM_ERRO_NOME] = undefined;
        },

        validarCpf(cpf) {
            if (!this.contemValor(cpf)) {
                const variavelCpfNaoPreenchido = "cpfNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] =  RECURSOS_I18N.getText(variavelCpfNaoPreenchido);
                
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
                const variavelCpfParcialmentePreenchido = "cpfParcialmentePreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] = RECURSOS_I18N.getText(variavelCpfParcialmentePreenchido);

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
                const variavelCpfInvalido = "cpfInvalido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] = RECURSOS_I18N.getText(variavelCpfInvalido);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF];
            }

            LISTA_ERROS[INDICE_MENSAGEM_ERRO_CPF] = undefined;
        },

        validarTelefone(telefone) {
            if (!this.contemValor(telefone)) {
                const variavelTelefoneNaoPreenchido = "telefoneNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE] = RECURSOS_I18N.getText(variavelTelefoneNaoPreenchido);

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
                const variavelTelefoneParcialmentePreenchido = "telefoneParcialmentePreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE] = RECURSOS_I18N.getText(variavelTelefoneParcialmentePreenchido);
                
                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE];
            }

            LISTA_ERROS[INDICE_MENSAGEM_ERRO_TELEFONE] = undefined;
        },

        validarIdade(idade) {
            if (!this.contemValor(idade)) {
                const variavelIdadeNaoPreenchida = "idadeNaoPreenchida";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = RECURSOS_I18N.getText(variavelIdadeNaoPreenchida);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE];
            }

            let numeroIdade = Number(idade);
            const valorMinimoIdade = 18;
            const valorMaximoIdade = 200;

            if (numeroIdade < valorMinimoIdade) {
                const variavelMenorDeIdade = "menorDeIdade";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = RECURSOS_I18N.getText(variavelMenorDeIdade);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE];
            }
            else if (numeroIdade >= valorMaximoIdade) {
                const variavelIdadeAcimaValorMaximo = "IdadeAcimaValorMaximo";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = RECURSOS_I18N.getText(variavelIdadeAcimaValorMaximo);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE];
            }

            LISTA_ERROS[INDICE_MENSAGEM_ERRO_IDADE] = undefined;
        },

        validarCheckIn(checkIn) {
            if (!this.contemValor(checkIn)) {
                const variavelCheckInNaoPreenchido = "checkInNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = RECURSOS_I18N.getText(variavelCheckInNaoPreenchido);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN];
            }

            let dataAtual = new Date();
            let anoAtual = dataAtual.getFullYear();
            let mesAtual = dataAtual.getMonth() + 1;
            let diaAtual = dataAtual.getDate();

            const separador = "-";
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split(separador);

            let primeiroCasoInvalido = anoCheckIn < anoAtual;
            let segundoCasoInvalido = (anoCheckIn == anoAtual) && (mesCheckIn == mesAtual) && (diaCheckIn < diaAtual);

            if (primeiroCasoInvalido || segundoCasoInvalido) {
                const variavelCheckInDatasPassadas = "checkInDatasPassadas";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = RECURSOS_I18N.getText(variavelCheckInDatasPassadas);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN];
            }

            LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_IN] = undefined;
        },

        validarCheckOut(checkOut, checkIn) {
            if (!this.contemValor(checkOut)) {
                const variavelCheckOutNaoPreenchido = "checkOutNaoPreenchido";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = RECURSOS_I18N.getText(variavelCheckOutNaoPreenchido);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT];
            }

            let dataAtual = new Date();
            let anoAtual = dataAtual.getFullYear();
            let mesAtual = dataAtual.getMonth() + 1;
            let diaAtual = dataAtual.getDate();

            const separador = "-";
            let [anoCheckOut, mesCheckOut, diaCheckOut] = checkOut.split(separador);
            let [anoCheckIn, mesCheckIn, diaCheckIn] = checkIn.split(separador);

            let primeiroCasoInvalido = anoCheckOut < anoAtual;
            let segundoCasoInvalido = (anoCheckOut == anoAtual) && (mesCheckOut == mesAtual) && (diaCheckOut < diaAtual);
            let terceiroCasoInvalido = (anoCheckOut == anoCheckIn) && (mesCheckOut == mesCheckIn) && (diaCheckOut < diaCheckIn);

            if (primeiroCasoInvalido || segundoCasoInvalido) {
                const variavelCheckOutDatasPassadas = "checkOutDatasPassadas";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = RECURSOS_I18N.getText(variavelCheckOutDatasPassadas);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT];
            }
            else if (terceiroCasoInvalido) {
                const variavelCheckOutAnteriorCheckIn = "checkOutAnteriorCheckIn";
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = RECURSOS_I18N.getText(variavelCheckOutAnteriorCheckIn);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT];
            }

            LISTA_ERROS[INDICE_MENSAGEM_ERRO_CHECK_OUT] = undefined;
        },

        validarPrecoEstadia(precoEstadia) {
            if (!this.contemValor(precoEstadia)) {
                const variavelPrecoEstadiaNaoPreenchido = "precoEstadiaNaoPreenchido"
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = RECURSOS_I18N.getText(variavelPrecoEstadiaNaoPreenchido);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA];
            }

            let numeroPrecoEstadia = Number(precoEstadia);
            const valorMaximoPrecoEstadia = 9999999999.99;
            const valorZero = 0;

            if (numeroPrecoEstadia > valorMaximoPrecoEstadia) {
               const variavelPrecoEstadiaAcimaValorMaximo = "precoEstadiaAcimaValorMaximo"
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = RECURSOS_I18N.getText(variavelPrecoEstadiaAcimaValorMaximo);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA];
            }
            else if (numeroPrecoEstadia <= valorZero) {
                const variavelPrecoEstadiaNegativoOuZero = "precoEstadiaNegativoOuZero"
                LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = RECURSOS_I18N.getText(variavelPrecoEstadiaNegativoOuZero);

                return LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA];
            }

            LISTA_ERROS[INDICE_MENSAGEM_ERRO_PRECO_ESTADIA] = undefined;
        }
    }
})