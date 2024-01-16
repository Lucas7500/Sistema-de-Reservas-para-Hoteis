sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox",
    "../Services/Validacao"
], (BaseController, Formatter, ReservaRepository, MessageBox, Validacao) => {
    "use strict";

    const CAMINHO_ROTA_CADASTRO = "reservas.hoteis.controller.Cadastro";
    const ROTA_LISTAGEM = "listagem";
    const STATUS_CREATED = 201;
    const STATUS_NO_CONTENT = 204;
    const VALUE_STATE_SUCESSO = "Success";
    const VALUE_STATE_ERRO = "Error";
    const PARAMETRO_VALUE = "value";
    const VALOR_INICIAL_VALUE_STATE_INPUT = "None";
    const ID_INPUT_NOME = "inputNome";
    const ID_INPUT_CPF = "inputCpf";
    const ID_INPUT_TELEFONE = "inputTelefone";
    const ID_INPUT_IDADE = "inputIdade";
    const ID_INPUT_CHECK_IN = "inputCheckIn";
    const ID_INPUT_CHECK_OUT = "inputCheckOut";
    const ID_INPUT_PRECO_ESTADIA = "inputPrecoEstadia";
    const MODELO_RESERVA = "reserva";
    const STRING_VAZIA = "";
    const CHAR_VIRGULA = ",";
    const CHAR_PONTO = ".";
    const REGEX_PONTOS = /\./g;
    const REGEX_VIRGULAS = /,/g;

    const ARRAY_ID_INPUTS = [
        ID_INPUT_NOME,
        ID_INPUT_CPF,
        ID_INPUT_TELEFONE,
        ID_INPUT_IDADE,
        ID_INPUT_CHECK_IN,
        ID_INPUT_CHECK_OUT,
        ID_INPUT_PRECO_ESTADIA
    ]

    return BaseController.extend(CAMINHO_ROTA_CADASTRO, {
        onInit() {
            const recursosi18n = this.obterRecursosI18n();
            const rotaCadastro = "cadastro";
            const rotaEdicao = "edicao";
            Validacao.definirRecursosi18n(recursosi18n);

            this.vincularRota(rotaCadastro, this._aoCoincidirRotaCadastro);
            this.vincularRota(rotaEdicao, this._aoCoincidirRotaEdicao);
        },

        _aoCoincidirRotaCadastro() {
            this._modeloReserva();
        },

        _aoCoincidirRotaEdicao(evento) {
            const parametroArguments = "arguments";
            const id = evento.getParameter(parametroArguments).id;

            this._obterReservaPorId(id);
        },

        _modeloReserva(reserva) {
            const idRadioButtonPagamentoNaoEfetuado = "radioButtonPagamentoNaoEfetuado";
            this.byId(idRadioButtonPagamentoNaoEfetuado).setSelected(true);

            this._limparValueStateInputs();

            if (reserva) {
                reserva.checkIn = Formatter.formataData(new Date(reserva.checkIn));
                reserva.checkOut = Formatter.formataData(new Date(reserva.checkOut));
                reserva.precoEstadia = Formatter.formataPrecoEstadia(reserva.precoEstadia);
            }
            else {
                let dataHoje = new Date();
                let valorPadraoData = Formatter.formataData(dataHoje);

                reserva = {
                    nome: STRING_VAZIA,
                    cpf: STRING_VAZIA,
                    telefone: STRING_VAZIA,
                    idade: STRING_VAZIA,
                    sexo: STRING_VAZIA,
                    checkIn: valorPadraoData,
                    checkOut: valorPadraoData,
                    precoEstadia: STRING_VAZIA,
                    pagamentoEfetuado: false
                };
            }

            this.modelo(MODELO_RESERVA, reserva);
        },

        _definirValueStateInputValidado(input, valueStateText) {
            valueStateText
                ? input.setValueState(VALUE_STATE_ERRO).setValueStateText(valueStateText)
                : input.setValueState(VALUE_STATE_SUCESSO);
        },

        _definirValueStateInputsSemAlteracao(listaErrosValidacao) {
            for (let i = 0; i < ARRAY_ID_INPUTS.length; i++) {
                let input = this.byId(ARRAY_ID_INPUTS[i]);
                let valueStateInput = input.getValueState();

                if (valueStateInput == VALOR_INICIAL_VALUE_STATE_INPUT) {
                    this._definirValueStateInputValidado(input, listaErrosValidacao[i]);
                }
            }
        },

        _limparValueStateInputs() {
            for (let idInput of ARRAY_ID_INPUTS) {
                let input = this.byId(idInput);
                input.setValueState(VALOR_INICIAL_VALUE_STATE_INPUT);
            }
        },

        _obterInputsSemAlteracao() {
            let inputsSemAlteracao = []

            for (let idInput of ARRAY_ID_INPUTS) {
                let valueStateInput = this.byId(idInput).getValueState();

                if (valueStateInput == VALOR_INICIAL_VALUE_STATE_INPUT) {
                    inputsSemAlteracao.push(idInput);
                }
            }

            return inputsSemAlteracao;
        },

        _obterReservaPreenchida() {
            let reserva = this.modelo(MODELO_RESERVA);

            let reservaPreenchida = {};
            if (reserva.id) {
                reservaPreenchida.id = reserva.id;
            }

            reservaPreenchida.nome = reserva.nome;
            reservaPreenchida.cpf = reserva.cpf;
            reservaPreenchida.telefone = reserva.telefone;
            reservaPreenchida.idade = Number(reserva.idade);
            reservaPreenchida.sexo = Number(reserva.sexo);
            reservaPreenchida.checkIn = reserva.checkIn;
            reservaPreenchida.checkOut = reserva.checkOut;
            reservaPreenchida.precoEstadia = Number(reserva.precoEstadia
                .replace(REGEX_PONTOS, STRING_VAZIA)
                .replace(CHAR_VIRGULA, CHAR_PONTO));
            reservaPreenchida.pagamentoEfetuado = reserva.pagamentoEfetuado;

            return reservaPreenchida;
        },

        _obterReservaPorId(id) {
            ReservaRepository.obterPorId(id)
                .then(response => {
                    return response.ok
                        ? response.json()
                        : Promise.reject(response);
                })
                .then(reserva => this._modeloReserva(reserva))
                .catch(async erro => {
                    let mensagemErro = await erro.text();
                    MessageBox.warning(mensagemErro);
                });
        },

        _criarReserva(reservaParaCriar) {
            const recursosi18n = this.obterRecursosI18n();
            const variavelSucessoSalvar = "sucessoSalvar";
            const mensagemSucessoSalvar = recursosi18n.getText(variavelSucessoSalvar);
            const controllerCadastro = this;

            ReservaRepository.criarReserva(reservaParaCriar)
                .then(async response => {
                    if (response.status == STATUS_CREATED) {
                        let reservaCriada = await response.json();
                        MessageBox.success(mensagemSucessoSalvar, {
                            onClose: () => {
                                controllerCadastro._abrirDetalhesReserva(reservaCriada.id);
                            }
                        });
                    }
                    else {
                        return Promise.reject(response);
                    }
                })
                .catch(async erro => {
                    let mensagemErro = await erro.text();

                    MessageBox.warning(mensagemErro);
                });
        },

        _atualizarReserva(reservaParaAtualizar) {
            const recursosi18n = this.obterRecursosI18n();
            const variavelSucessoEditar = "sucessoEditar";
            const mensagemSucessoEditar = recursosi18n.getText(variavelSucessoEditar);
            const controllerCadastro = this;

            ReservaRepository.atualizarReserva(reservaParaAtualizar)
                .then(response => {
                    if (response.status == STATUS_NO_CONTENT) {
                        MessageBox.success(mensagemSucessoEditar, {
                            onClose: () => {
                                controllerCadastro._abrirDetalhesReserva(reservaParaAtualizar.id);
                            }
                        })
                    }
                    else {
                        return Promise.reject(response);
                    }
                })
                .catch(async erro => {
                    let mensagemErro = await erro.text();

                    MessageBox.warning(mensagemErro);
                });
        },

        _abrirDetalhesReserva(id) {
            try {
                const rotaDetalhes = "detalhes";
                this.navegarPara(rotaDetalhes, id);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarNavegarParaTelaListagem() {
            try {
                this.navegarPara(ROTA_LISTAGEM);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarSalvarReserva() {
            try {
                let reservaPreenchida = this._obterReservaPreenchida();
                let inputsSemAlteracao = this._obterInputsSemAlteracao();
                let listaErrosValidacao = Validacao.obterListaErros();
                
                if (inputsSemAlteracao.length) {
                    Validacao.validarPropriedadesVazias(reservaPreenchida);
                    listaErrosValidacao = Validacao.obterListaErros();
                    this._definirValueStateInputsSemAlteracao(listaErrosValidacao);
                }
                
                let mensagensErroValidacao = Formatter.formataListaErros(listaErrosValidacao);

                mensagensErroValidacao
                    ? MessageBox.warning(mensagensErroValidacao)
                    : reservaPreenchida.id
                        ? this._atualizarReserva(reservaPreenchida)
                        : this._criarReserva(reservaPreenchida);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarCancelarCadastro() {
            try {
                const recursosi18n = this.obterRecursosI18n();

                const variavelBotaoSim = "botaoSim";
                const variavelBotaoNao = "botaoNao";
                const variavelConfirmacaoCancelar = "confirmacaoCancelar";

                const botaoSim = recursosi18n.getText(variavelBotaoSim);
                const botaoNao = recursosi18n.getText(variavelBotaoNao);
                const mensagemConfirmacao = recursosi18n.getText(variavelConfirmacaoCancelar);

                let controllerCadastro = this;

                MessageBox.confirm(mensagemConfirmacao, {
                    actions: [botaoSim, botaoNao],
                    emphasizedAction: botaoSim,
                    onClose: function (acao) {
                        if (acao == botaoSim) {
                            controllerCadastro.navegarPara(ROTA_LISTAGEM);
                        }
                    }
                });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoDigitarValidarIdade(evento) {
            const regexNumeros = "[0-9]";
            let valorInput = evento.getSource().getValue();

            for (let char of valorInput) {
                if (!char.match(regexNumeros)) {
                    let inputValido = valorInput.replace(char, STRING_VAZIA);
                    evento.getSource().setValue(inputValido);
                }
            }
        },

        aoMudarValidarNome(evento) {
            try {
                let inputNome = evento.getSource();
                let valorNome = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarNome(valorNome);

                this._definirValueStateInputValidado(inputNome, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarCpf(evento) {
            try {
                let inputCpf = evento.getSource();
                let valorCpf = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarCpf(valorCpf);

                this._definirValueStateInputValidado(inputCpf, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarTelefone(evento) {
            try {
                let inputTelefone = evento.getSource();
                let valorTelefone = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarTelefone(valorTelefone);

                this._definirValueStateInputValidado(inputTelefone, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarIdade(evento) {
            try {
                let inputIdade = evento.getSource();
                let valorIdade = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarIdade(valorIdade);

                this._definirValueStateInputValidado(inputIdade, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarCheckInECheckOut() {
            try {
                let inputCheckIn = this.byId(ID_INPUT_CHECK_IN);
                let inputCheckOut = this.byId(ID_INPUT_CHECK_OUT);

                let valorCheckIn = inputCheckIn.getValue();
                let valorCheckOut = inputCheckOut.getValue();

                let mensagemErroValidacaoCheckIn = Validacao.validarCheckIn(valorCheckIn);
                let mensagemErroValidacaoCheckOut = Validacao.validarCheckOut(valorCheckOut, valorCheckIn);

                this._definirValueStateInputValidado(inputCheckIn, mensagemErroValidacaoCheckIn);
                this._definirValueStateInputValidado(inputCheckOut, mensagemErroValidacaoCheckOut);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarPrecoEstadia(evento) {
            try {
                let inputPrecoEstadia = evento.getSource();
                let valorPrecoEstadia = inputPrecoEstadia.getValue();

                const regexNumerosPontoVirgula = "[0-9\.,]";
                for (let char of valorPrecoEstadia) {
                    if (!char.match(regexNumerosPontoVirgula)) {
                        valorPrecoEstadia = valorPrecoEstadia.replace(char, STRING_VAZIA);
                    }
                }

                valorPrecoEstadia = valorPrecoEstadia
                    .replace(REGEX_PONTOS, STRING_VAZIA)
                    .replace(REGEX_VIRGULAS, CHAR_PONTO);

                let mensagemErroValidacao = Validacao.validarPrecoEstadia(valorPrecoEstadia);

                this._definirValueStateInputValidado(inputPrecoEstadia, mensagemErroValidacao);
                inputPrecoEstadia.setValue(Formatter.formataPrecoEstadia(valorPrecoEstadia));
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})