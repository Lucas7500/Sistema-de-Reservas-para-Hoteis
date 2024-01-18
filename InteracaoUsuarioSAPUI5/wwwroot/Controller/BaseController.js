sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox"
], (Controller, JSONModel, MessageBox) => {
    "use strict";

    const CAMINHO_ROTA_BASE_CONTROLLER = "reservas.hoteis.controller.BaseController";
    let roteador;

    return Controller.extend(CAMINHO_ROTA_BASE_CONTROLLER, {
        onInit() {
            roteador = this.getOwnerComponent().getRouter();
        },

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
            roteador.getRoute(nomeDaRota).attachPatternMatched(aoCoincidirRota, this);
        },

        navegarPara(nomeDaRota, parametroId) {
            try {
                roteador.navTo(nomeDaRota, {
                    id: parametroId
                });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    });
});