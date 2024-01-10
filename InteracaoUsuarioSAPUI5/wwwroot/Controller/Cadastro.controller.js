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

            this.getView().setModel(new JSONModel(reserva));
        },

        _definirValueStateInput(input, valueStateText) {
            valueStateText
                ? input.setValueState(VALUE_STATE_ERRO).setValueStateText(valueStateText)
                : input.setValueState(VALUE_STATE_SUCESSO);
        },

        _definirValueStateInputsSemAlteracao(listaErrosValidacao) {
            const valorInicialValueState = "None";

            const idInputNome = "inputNome";
            const idInputCpf = "inputCpf";
            const idInputTelefone = "inputTelefone";
            const idInputIdade = "inputIdade";
            const idInputCheckIn = "inputCheckIn";
            const idInputCheckOut = "inputCheckOut";
            const idInputPrecoEstadia = "inputPrecoEstadia";

            const idInputs = [
                idInputNome,
                idInputCpf,
                idInputTelefone,
                idInputIdade,
                idInputCheckIn,
                idInputCheckOut,
                idInputPrecoEstadia
            ]

            for (let i = 0; i < idInputs.length; i++) {
                let input = this.byId(idInputs[i]);
                let valueStateInput = input.getValueState();

                if (valueStateInput == valorInicialValueState) {
                    this._definirValueStateInput(input, listaErrosValidacao[i]);
                }
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
                let mensagensErroValidacao = Validacao.obterMensagensErro();

                if (mensagensErroValidacao) {
                    let listaErrosValidacao = Validacao.obterListaErros();

                    this._definirValueStateInputsSemAlteracao(listaErrosValidacao);
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
                const idInputCheckIn = "inputCheckIn";
                const idInputCheckOut = "inputCheckOut";

                let inputCheckIn = this.byId(idInputCheckIn);
                let inputCheckOut = this.byId(idInputCheckOut);

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