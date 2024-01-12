sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/Formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox",
    "../Services/Validacao"
], (Controller, Formatter, JSONModel, ReservaRepository, MessageBox, Validacao) => {
    "use strict";

    const CAMINHO_ROTA_CADASTRO = "reservas.hoteis.controller.Cadastro";
    const STATUS_CREATED = 201;
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

    const ARRAY_ID_INPUTS = [
        ID_INPUT_NOME,
        ID_INPUT_CPF,
        ID_INPUT_TELEFONE,
        ID_INPUT_IDADE,
        ID_INPUT_CHECK_IN,
        ID_INPUT_CHECK_OUT,
        ID_INPUT_PRECO_ESTADIA
    ]

    return Controller.extend(CAMINHO_ROTA_CADASTRO, {
        onInit() {
            let rota = this.getOwnerComponent().getRouter();
            const rotaCadastro = 'cadastro';
            rota.getRoute(rotaCadastro).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota() {
            const recursosi18n = this._obterRecursosI18n();

            this._modeloReserva();
            Validacao.definirRecursosi18n(recursosi18n);
        },

        _modeloReserva() {
            let dataHoje = new Date();
            let valorPadraoData = Formatter.formataData(dataHoje);

            const stringVazia = "";
            let reserva = {
                nome: stringVazia,
                cpf: stringVazia,
                telefone: stringVazia,
                idade: stringVazia,
                sexo: stringVazia,
                checkIn: valorPadraoData,
                checkOut: valorPadraoData,
                precoEstadia: stringVazia,
                pagamentoEfetuado: false
            };

            const idRadioButtonPagamentoNaoEfetuado = "radioButtonPagamentoNaoEfetuado";
            this.byId(idRadioButtonPagamentoNaoEfetuado).setSelected(true);
            this._limparValueStateInputs();

            this.getView().setModel(new JSONModel(reserva));
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

        _obterRecursosI18n() {
            const modeloi18n = "i18n";
            return this.getOwnerComponent().getModel(modeloi18n).getResourceBundle();
        },

        _obterReservaPreenchida() {
            let reserva = this.getView().getModel().getData();

            return {
                nome: reserva.nome,
                cpf: reserva.cpf,
                telefone: reserva.telefone,
                idade: Number(reserva.idade),
                sexo: Number(reserva.sexo),
                checkIn: reserva.checkIn,
                checkOut: reserva.checkOut,
                precoEstadia: Number(reserva.precoEstadia),
                pagamentoEfetuado: reserva.pagamentoEfetuado
            };
        },

        _criarReserva(reserva) {
            const recursosi18n = this._obterRecursosI18n();
            const variavelSucessoSalvar = "sucessoSalvar";
            const mensagemSucessoSalvar = recursosi18n.getText(variavelSucessoSalvar);
            let controllerCadastro = this;

            ReservaRepository.criarReserva(reserva)
                .then(async response => {
                    if (response.status == STATUS_CREATED) {
                        let reserva = await response.json();
                        MessageBox.success(mensagemSucessoSalvar, {
                            onClose: () => {
                                controllerCadastro._abrirDetalhesReservaCriada(reserva);
                            }
                        });

                        return response.json();
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

        _abrirDetalhesReservaCriada(reservaCriada) {
            try {
                let rota = this.getOwnerComponent().getRouter();
                const rotaDetalhes = "detalhes";
                rota.navTo(rotaDetalhes, {
                    id: reservaCriada.id
                });
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        _navegarParaTelaListagem() {
            try {
                let rota = this.getOwnerComponent().getRouter();
                const rotaListagem = "listagem";
                rota.navTo(rotaListagem);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarNavegarParaTelaListagem() {
            try {
                this._navegarParaTelaListagem();
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
                    : this._criarReserva(reservaPreenchida);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarCancelarCadastro() {
            try {
                const recursosi18n = this._obterRecursosI18n();

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
                            controllerCadastro._navegarParaTelaListagem();
                        }
                    }
                });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoDigitarValidarIdade(evento) {
            const regexIdade = "[0-9]";
            let valorInput = evento.getSource().getValue();

            for (let char of valorInput) {
                if (!char.match(regexIdade)) {
                    let inputValido = valorInput.replace(char, "");
                    evento.getSource().setValue(inputValido);
                }
            }
        },

        aoDigitarValidarPrecoEstadia(evento) {
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
                let valorPrecoEstadia = evento.getParameter(PARAMETRO_VALUE);
                let valorPrecoEstadiaFormatado = Formatter.formataPrecoEstadia(valorPrecoEstadia);
                inputPrecoEstadia.setValue(valorPrecoEstadiaFormatado);

                let mensagemErroValidacao = Validacao.validarPrecoEstadia(valorPrecoEstadia);

                this._definirValueStateInputValidado(inputPrecoEstadia, mensagemErroValidacao);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})