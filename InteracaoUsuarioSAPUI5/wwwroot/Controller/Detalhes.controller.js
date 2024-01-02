sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../Repositorios/ReservaRepository",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History",
    "sap/ui/core/library",
    "sap/m/Dialog",
    "sap/m/Button",
    "sap/m/library",
    "sap/m/Text"
], (Controller, ReservaRepository, JSONModel, History, CoreLibrary, Dialog, Button, MobileLibrary, Text) => {
    "use strict";

    const STATUS_OK = 200;
    const CAMINHO_ROTA_DETALHES = "reservas.hoteis.controller.Detalhes";

    return Controller.extend(CAMINHO_ROTA_DETALHES, {
        onInit() {
            const rotaDetalhes = 'detalhes';

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute(rotaDetalhes).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota(evento) {
            try {
                const parametroArgumentos = "arguments";
                let idReserva = evento.getParameter(parametroArgumentos).id;

                ReservaRepository.obterPorId(idReserva)
                    .then(response => {
                        return response.status == STATUS_OK
                            ? response.json()
                            : Promise.reject(response);
                    })
                    .then(response => this.getView().setModel(new JSONModel(response)))
                    .catch(async erro => {
                        let mensagemErro = await erro.text();

                        this._mostrarMensagemErro(mensagemErro);
                    });
            }
            catch (erro) {
                this._mostrarMensagemErro(erro.message);
            }
        },

        _mostrarMensagemErro(mensagemErro) {
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
        }
    })
})