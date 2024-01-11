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
            this._modeloReserva();
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

        _definirValueStateInput(input, valueStateText) {
            valueStateText
                ? input.setValueState(VALUE_STATE_ERRO).setValueStateText(valueStateText)
                : input.setValueState(VALUE_STATE_SUCESSO);
        },

        _definirValueStateInputsSemAlteracao(listaErrosValidacao) {
            for (let i = 0; i < ARRAY_ID_INPUTS.length; i++) {
                let input = this.byId(ARRAY_ID_INPUTS[i]);
                let valueStateInput = input.getValueState();

                if (valueStateInput == VALOR_INICIAL_VALUE_STATE_INPUT) {
                    this._definirValueStateInput(input, listaErrosValidacao[i]);
                }
            }
        },

        _limparValueStateInputs() {
            for (let i = 0; i < ARRAY_ID_INPUTS.length; i++) {
                let input = this.byId(ARRAY_ID_INPUTS[i]);
               input.setValueState(VALOR_INICIAL_VALUE_STATE_INPUT);
            }
        },

        _obterVariavelI18n(nomeVariavel) {
            const modeloi18n = "i18n";
            const recursosi18n = this.getOwnerComponent().getModel(modeloi18n).getResourceBundle();

            return recursosi18n.getText(nomeVariavel);
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
            const textoMensagemSucessoSalvar = "mensagemSucessoSalvar";
            const mensagemSucessoSalvar = this._obterVariavelI18n(textoMensagemSucessoSalvar);
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
                let reserva = this._obterReservaPreenchida();
                Validacao.validarPropriedadesSemAlteracao(reserva);
                let listaErrosValidacao = Validacao.obterListaErros();

                if (listaErrosValidacao.length) {
                    this._definirValueStateInputsSemAlteracao(listaErrosValidacao);
                    let mensagensErroValidacao = Formatter.formataListaErros(listaErrosValidacao);
                    MessageBox.warning(mensagensErroValidacao);
                }
                else {
                    this._criarReserva(reserva);
                }
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarCancelarCadastro() {
            try {
                const textoBotaoSim = "botaoSim";
                const textoBotaoNao = "botaoNao";
                const textoMensagemConfirmacaoCancelar = "mensagemConfirmacaoCancelar";

                const botaoSim = this._obterVariavelI18n(textoBotaoSim);
                const botaoNao = this._obterVariavelI18n(textoBotaoNao);
                const mensagemConfirmacao = this._obterVariavelI18n(textoMensagemConfirmacaoCancelar);

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

        aoMudarValidarNome(evento) {
            try {
                let inputNome = evento.getSource();
                let valorNome = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarNome(valorNome);

                this._definirValueStateInput(inputNome, mensagemErroValidacao);
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

                this._definirValueStateInput(inputCpf, mensagemErroValidacao);
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

                this._definirValueStateInput(inputTelefone, mensagemErroValidacao);
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

                this._definirValueStateInput(inputIdade, mensagemErroValidacao);
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

                this._definirValueStateInput(inputCheckIn, mensagemErroValidacaoCheckIn);
                this._definirValueStateInput(inputCheckOut, mensagemErroValidacaoCheckOut);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarPrecoEstadia(evento) {
            try {
                let inputPrecoEstadia = evento.getSource();
                let valorPrecoEstadia = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarPrecoEstadia(valorPrecoEstadia);

                this._definirValueStateInput(inputPrecoEstadia, mensagemErroValidacao);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})