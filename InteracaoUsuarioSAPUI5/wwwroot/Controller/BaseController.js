sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox"
], (Controller, JSONModel, MessageBox) => {
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

        exibirEspera(acao) {
            try {
                const tipoDaPromise = "catch", tipoBuscado = "function";
                let promise = acao();

                if (promise && typeof (promise[tipoDaPromise]) == tipoBuscado) {
                    promise.catch(erro => MessageBox.warning(erro.message));
                }
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    });
});