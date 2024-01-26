sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox",
    "sap/ui/core/BusyIndicator"
], (Controller, JSONModel, MessageBox, BusyIndicator) => {
    "use strict";

    const CAMINHO_ROTA_BASE_CONTROLLER = "reservas.hoteis.controller.BaseController";

    return Controller.extend(CAMINHO_ROTA_BASE_CONTROLLER, {
        modelo(nome, modelo) {
            return modelo
                ? this.getView().setModel(new JSONModel(modelo), nome)
                : this.getView().getModel(nome).getData();
        },

        obterRecursosI18n() {
            const modeloi18n = "i18n";

            return this
                .getOwnerComponent()
                .getModel(modeloi18n)
                .getResourceBundle();
        },

        vincularRota(nomeDaRota, aoCoincidirRota) {
            return this
                .getOwnerComponent()
                .getRouter()
                .getRoute(nomeDaRota)
                .attachPatternMatched(aoCoincidirRota, this);
        },

        messageBoxConfirmacao(mensagem, metodo) {
            MessageBox.confirm(mensagem, {
                actions: [MessageBox.Action.YES, MessageBox.Action.NO],
                emphasizedAction: MessageBox.Action.YES,
                onClose: (acao) => {
                    if (acao == MessageBox.Action.YES) metodo();
                }
            });
        },

        messageBoxSucesso(mensagem, metodo) {
            MessageBox.success(mensagem, {
                onClose: () => {
                    metodo();
                }
            });
        },

        navegarPara(nomeDaRota, parametroId) {
            return this
                .getOwnerComponent()
                .getRouter()
                .navTo(nomeDaRota, { id: parametroId });
        },

        async exibirEspera(acao) {
            const duracaoBusyIndicator = 450;
            const delayBusyIndicator = 0;

            await this._showBusyIndicator(duracaoBusyIndicator, delayBusyIndicator);
            await this.processarEvento(acao);
        },

        async processarEvento(acao) {
            try {
                const tipoDaPromise = "catch", tipoBuscado = "function";
                let promise = await acao();

                if (promise && typeof (promise[tipoDaPromise]) == tipoBuscado) {
                    promise.catch(erro => MessageBox.warning(erro.message));
                }
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        async _showBusyIndicator(duracao, delay) {
            BusyIndicator.show(delay);

            if (duracao && duracao > 0) {
                if (this._sTimeoutId) {
                    clearTimeout(this._sTimeoutId);
                    this._sTimeoutId = null;
                }

                this._sTimeoutId = setTimeout(function () {
                    this._hideBusyIndicator();
                }.bind(this), duracao);
            }
        },

        async _hideBusyIndicator() {
            BusyIndicator.hide();
        },
    });
});