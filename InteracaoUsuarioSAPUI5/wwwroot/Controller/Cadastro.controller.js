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
    const MODEL_I18N = "i18n";
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

        _obterValorCheckIn() {
            return this.byId("inputCheckIn").getValue();
        },

        _criarReserva(reserva) {
            const resourceBundle = this.getOwnerComponent().getModel(MODEL_I18N).getResourceBundle();
            const textoMensagemSucessoSalvar = "mensagemSucessoSalvar";
            const mensagemSucessoSalvar = resourceBundle.getText(textoMensagemSucessoSalvar);

            let controller = this;

            ReservaRepository.criarReserva(reserva)
                .then(async response => {
                    if (response.status == STATUS_CREATED) {
                        let reserva = await response.json();
                        MessageBox.success(mensagemSucessoSalvar, {
                            onClose: () => {
                                controller._abrirDetalhesReservaCriada(reserva);
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
            this._navegarParaTelaListagem();
        },

        aoClicarSalvarReserva() {
            try {
                let reserva = this._obterReservaPreenchida();
                this._criarReserva(reserva)
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarCancelarCadastro() {
            try {
                const resourceBundle = this.getOwnerComponent().getModel(MODEL_I18N).getResourceBundle();
                const textoBotaoSim = "botaoSim";
                const botaoSim = resourceBundle.getText(textoBotaoSim);
                const textoBotaoNao = "botaoNao";
                const botaoNao = resourceBundle.getText(textoBotaoNao);
                const textoMensagemConfirmacaoCancelar = "mensagemConfirmacaoCancelar";
                const mensagemConfirmacao = resourceBundle.getText(textoMensagemConfirmacaoCancelar);

                let controller = this;

                MessageBox.confirm(mensagemConfirmacao, {
                    actions: [botaoSim, botaoNao],
                    emphasizedAction: botaoSim,
                    onClose: function (acao) {
                        if (acao == botaoSim) {
                            controller._navegarParaTelaListagem();
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
                let nome = evento.getParameter(PARAMETRO_VALUE);

                Validacao.validarNome(nome);
                evento.getSource().setValueState(VALUE_STATE_SUCESSO);
            }
            catch (mensagemErroValidacao) {
                evento.getSource().setValueState(VALUE_STATE_ERRO).setValueStateText(mensagemErroValidacao);
            }
        },

        aoMudarValidarCpf(evento) {
            try {
                let cpf = evento.getParameter(PARAMETRO_VALUE);

                Validacao.validarCpf(cpf);
                evento.getSource().setValueState(VALUE_STATE_SUCESSO);
            } catch (mensagemErroValidacao) {
                evento.getSource().setValueState(VALUE_STATE_ERRO).setValueStateText(mensagemErroValidacao);
            }
        },

        aoMudarValidarTelefone(evento) {
            try {
                let telefone = evento.getParameter(PARAMETRO_VALUE);

                Validacao.validarTelefone(telefone);
                evento.getSource().setValueState(VALUE_STATE_SUCESSO);
            } catch (mensagemErroValidacao) {
                evento.getSource().setValueState(VALUE_STATE_ERRO).setValueStateText(mensagemErroValidacao);
            }
        },

        aoMudarValidarIdade(evento) {
            try {
                let idade = evento.getParameter(PARAMETRO_VALUE);

                Validacao.validarIdade(idade);
                evento.getSource().setValueState(VALUE_STATE_SUCESSO);
            } catch (mensagemErroValidacao) {
                evento.getSource().setValueState(VALUE_STATE_ERRO).setValueStateText(mensagemErroValidacao);
            }
        },

        aoMudarValidarCheckIn(evento) {
            try {
                let checkIn = evento.getParameter(PARAMETRO_VALUE);

                Validacao.validarCheckIn(checkIn);
                evento.getSource().setValueState(VALUE_STATE_SUCESSO);
            } catch (mensagemErroValidacao) {
                evento.getSource().setValueState(VALUE_STATE_ERRO).setValueStateText(mensagemErroValidacao);
            }
        },

        aoMudarValidarCheckOut(evento) {
            try {
                let checkOut = evento.getParameter(PARAMETRO_VALUE);
                let checkIn = this._obterValorCheckIn();

                Validacao.validarCheckOut(checkOut, checkIn);
                evento.getSource().setValueState(VALUE_STATE_SUCESSO);
            } catch (mensagemErroValidacao) {
                evento.getSource().setValueState(VALUE_STATE_ERRO).setValueStateText(mensagemErroValidacao);
            }
        },

        aoMudarValidarPrecoEstadia(evento) {
            try {
                let precoEstadia = evento.getParameter(PARAMETRO_VALUE);

                Validacao.validarPrecoEstadia(precoEstadia);
                evento.getSource().setValueState(VALUE_STATE_SUCESSO);
            } catch (mensagemErroValidacao) {
                evento.getSource().setValueState(VALUE_STATE_ERRO).setValueStateText(mensagemErroValidacao);
            }
        }
    })
})