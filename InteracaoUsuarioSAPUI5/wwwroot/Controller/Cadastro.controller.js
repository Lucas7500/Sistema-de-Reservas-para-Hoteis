sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/library",
    "sap/m/Dialog",
    "sap/m/Button",
    "sap/m/library",
    "sap/m/Text",
    "sap/ui/core/routing/History",
    "../Repositorios/ReservaRepository"
], (Controller, CoreLibrary, Dialog, Button, MobileLibrary, Text, History, ReservaRepository) => {
    "use strict";

    const CAMINHO_ROTA_CADASTRO = "reservas.hoteis.controller.Cadastro";
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
        _mostrarMensagemErro(mensagemErro) {
            debugger
            var ButtonType = MobileLibrary.ButtonType;
            var DialogType = MobileLibrary.DialogType;
            var ValueState = CoreLibrary.ValueState;
            const tituloDialog = "Erro";

            if (!this.oErrorMessageDialog) {
                const textoBotao = "OK";
                this.oErrorMessageDialog = new Dialog({
                    type: DialogType.Message,
                    title: tituloDialog,
                    state: ValueState.Warning,
                    content: new Text({ text: mensagemErro }),
                    beginButton: new Button({
                        type: ButtonType.Emphasized,
                        text: textoBotao,
                        press: function () {
                            this.oErrorMessageDialog.close();
                        }.bind(this)
                    })
                });
            }

            this.oErrorMessageDialog.open();
        },
        _retornaReservaAserCriada() {
            const propriedadeSelectedKey = "selectedKey";
            const propriedadeSelected = "selected";

            return {
                nome: this.byId(ID_INPUT_NOME).getValue(),
                cpf: this.byId(ID_INPUT_CPF).getValue(),
                telefone: this.byId(ID_INPUT_TELEFONE).getValue(),
                idade: parseInt(this.byId(ID_INPUT_IDADE).getValue()),
                sexo: parseInt(this.byId(ID_COMBOBOX_SEXO).getProperty(propriedadeSelectedKey)),
                checkIn: this.byId(ID_INPUT_CHECKIN).getValue(),
                checkOut: this.byId(ID_INPUT_CHECKOUT).getValue(),
                precoEstadia: parseFloat(this.byId(ID_INPUT_PRECO_ESTADIA).getValue()),
                pagamentoEfetuado: this.byId(ID_RADIOBUTTON_PAGAMENTO_EFETUADO).getProperty(propriedadeSelected)
            }
        },

        _limparCampos() {
            const valorVazio = "";
            this.byId(ID_INPUT_NOME).setValue(valorVazio);
            this.byId(ID_INPUT_CPF).setValue(valorVazio);
            this.byId(ID_INPUT_TELEFONE).setValue(valorVazio);
            this.byId(ID_INPUT_IDADE).setValue(valorVazio);
            this.byId(ID_RADIOBUTTON_PAGAMENTO_NAO_EFETUADO).setSelected(true);
            this.byId(ID_COMBOBOX_SEXO).setValue(valorVazio);
            this.byId(ID_INPUT_CHECKIN).setValue(valorVazio);
            this.byId(ID_INPUT_CHECKOUT).setValue(valorVazio);
            this.byId(ID_INPUT_PRECO_ESTADIA).setValue(valorVazio);
        },

        voltarPagina() {
            const oHistory = History.getInstance();
            const sPreviousHash = oHistory.getPreviousHash();

            if (sPreviousHash !== undefined) {
                window.history.go(-1);
            } else {
                const rotaLista = "listagem";
                const oRouter = this.getOwnerComponent().getRouter();
                oRouter.navTo(rotaLista, {}, true);
            }
        },

        aoClicarCancelarCadastro() {
            this.voltarPagina();
            this._limparCampos();
        },

        aoClicarSalvarReserva() {
            try {
                debugger
                let reserva = this._retornaReservaAserCriada();

                ReservaRepository.criarReserva(reserva)
                    .then(response => {
                        debugger
                        if (response.status == STATUS_CREATED) {
                            this.voltarPagina();
                            this._limparCampos();

                            return response.json();
                        }
                        else {
                            return Promise.reject(response);
                        }
                    })
                    .catch(async erro => {
                        debugger
                        let mensagemErro = await erro.text();

                        this._mostrarMensagemErro(mensagemErro);
                    });
            }
            catch (erro) {
                this._mostrarMensagemErro(erro.message);
            }
        }
    })
})