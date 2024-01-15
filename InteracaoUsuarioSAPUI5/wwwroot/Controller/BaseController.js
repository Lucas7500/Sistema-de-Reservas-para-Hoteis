sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], (Controller, JSONModel) => {
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
            return this.getOwnerComponent().getModel(modeloi18n).getResourceBundle();
        },

        vincularRota(nomeDaRota, aoCoincidirRota) {
            let roteador = this.getOwnerComponent().getRouter();
            roteador.getRoute(nomeDaRota).attachPatternMatched(aoCoincidirRota, this);
        },

        navegarPara(nomeDaRota, parametroId) {
            try {
                let roteador = this.getOwnerComponent().getRouter();

                parametroId
                    ? roteador.navTo(nomeDaRota, {
                        id: parametroId
                    })
                    : roteador.navTo(nomeDaRota);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    });
});