sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (Controller, History, ReservaRepository, MessageBox) => {
    "use strict";

    const CAMINHO_ROTA_CADASTRO = "reservas.hoteis.controller.Cadastro";
    const MODEL_I18N = "i18n";
    const STATUS_CREATED = 201;
    const ID_INPUT_NOME = "inputNome";
    const ID_INPUT_CPF = "inputCpf";
    const ID_INPUT_TELEFONE = "inputTelefone";
    const ID_INPUT_IDADE = "inputIdade";
    const ID_COMBOBOX_SEXO = "comboBoxSexo";
    const ID_INPUT_CHECKIN = "inputCheckIn";
    const ID_INPUT_CHECKOUT = "inputCheckOut";
    const ID_INPUT_PRECO_ESTADIA = "inputPrecoEstadia";
    const ID_RADIOBUTTON_PAGAMENTO_EFETUADO = "radioButtonPagamentoEfetuado";
    const ID_RADIOBUTTON_PAGAMENTO_NAO_EFETUADO = "radioButtonPagamentoNaoEfetuado";

    return Controller.extend(CAMINHO_ROTA_CADASTRO, {
        onInit() {
            const rotaCadastro = 'cadastro';

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute(rotaCadastro).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota() {
            this._limparCampos();
        },

        _limparCampos() {
            const valorVazio = "";
            const valorPadraoComboBoxSexo = 0;

            this.byId(ID_INPUT_NOME).setValue(valorVazio);
            this.byId(ID_INPUT_CPF).setValue(valorVazio);
            this.byId(ID_INPUT_TELEFONE).setValue(valorVazio);
            this.byId(ID_INPUT_IDADE).setValue(valorVazio);
            this.byId(ID_COMBOBOX_SEXO).setSelectedKey(valorPadraoComboBoxSexo);
            this.byId(ID_INPUT_CHECKIN).setValue(valorVazio);
            this.byId(ID_INPUT_CHECKOUT).setValue(valorVazio);
            this.byId(ID_INPUT_PRECO_ESTADIA).setValue(valorVazio);
            this.byId(ID_RADIOBUTTON_PAGAMENTO_NAO_EFETUADO).setSelected(true);
        },

        _retornaReservaAserCriada() {
            const propriedadeSelectedKey = "selectedKey";
            const propriedadeSelected = "selected";

            return {
                nome: this.byId(ID_INPUT_NOME).getValue(),
                cpf: this.byId(ID_INPUT_CPF).getValue(),
                telefone: this.byId(ID_INPUT_TELEFONE).getValue(),
                idade: Number(this.byId(ID_INPUT_IDADE).getValue()),
                sexo: Number(this.byId(ID_COMBOBOX_SEXO).getProperty(propriedadeSelectedKey)),
                checkIn: this.byId(ID_INPUT_CHECKIN).getValue(),
                checkOut: this.byId(ID_INPUT_CHECKOUT).getValue(),
                precoEstadia: Number(this.byId(ID_INPUT_PRECO_ESTADIA).getValue()),
                pagamentoEfetuado: this.byId(ID_RADIOBUTTON_PAGAMENTO_EFETUADO).getProperty(propriedadeSelected)
            }
        },

        _abrirDetalhesObjetoCriado(reservaCriada) {
            try {
                const rotaDetalhes = "detalhes";
                let rota = this.getOwnerComponent().getRouter();
                rota.navTo(rotaDetalhes, {
                    id: reservaCriada.id
                });
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        voltarPagina() {
            try {
                const oHistory = History.getInstance();
                const sPreviousHash = oHistory.getPreviousHash();

                if (sPreviousHash !== undefined) {
                    window.history.go(-1);
                } else {
                    const rotaLista = "listagem";
                    const oRouter = this.getOwnerComponent().getRouter();
                    oRouter.navTo(rotaLista, {}, true);
                }
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarSalvarReserva() {
            try {
                let reserva = this._retornaReservaAserCriada();
                let controller = this;

                const resourceBundle = this.getOwnerComponent().getModel(MODEL_I18N).getResourceBundle();
                const textoMensagemSucessoSalvar = "mensagemSucessoSalvar";
                const mensagemSucessoSalvar = resourceBundle.getText(textoMensagemSucessoSalvar);

                ReservaRepository.criarReserva(reserva)
                    .then(async response => {
                        if (response.status == STATUS_CREATED) {
                            let reserva = await response.json();
                            MessageBox.success(mensagemSucessoSalvar, {
                                onClose: () => {
                                    controller._abrirDetalhesObjetoCriado(reserva);
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
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarCancelarCadastro() {
            try {
                const resourceBundle = this.getOwnerComponent().getModel(MODEL_I18N).getResourceBundle();

                const textoBotaoSim = "botaoSim";
                const textoBotaoNao = "botaoNao";
                const textoMensagemConfirmacaoCancelar = "mensagemConfirmacaoCancelar";
                const botaoSim = resourceBundle.getText(textoBotaoSim);
                const botaoNao = resourceBundle.getText(textoBotaoNao);
                const mensagemConfirmacao = resourceBundle.getText(textoMensagemConfirmacaoCancelar);

                let controller = this;

                MessageBox.confirm(mensagemConfirmacao, {
                    actions: [botaoSim, botaoNao],
                    emphasizedAction: botaoSim,
                    onClose: function (acao) {
                        if (acao == botaoSim) {
                            controller.voltarPagina();
                        }
                    }
                });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})