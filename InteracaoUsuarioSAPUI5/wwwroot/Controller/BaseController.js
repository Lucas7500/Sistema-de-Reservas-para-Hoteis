sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], (Controller, JSONModel) => {
    "use strict";

    const CAMINHO_ROTA_BASE_CONTROLLER = "reservas.hoteis.controller.BaseController";

    return Controller.extend(CAMINHO_ROTA_BASE_CONTROLLER, {
        modelo(nomeModelo, dados) {
            if (dados) {
                this.getView().setModel(new JSONModel(dados), nomeModelo);
            }

            let modeloEncontrado = this.getView().getModel(nomeModelo);
            if (modeloEncontrado) {
                return modeloEncontrado.getData();
            }
        },

        obterRecursosI18n() {
            const modeloi18n = "i18n";
            return this.getOwnerComponent().getModel(modeloi18n).getResourceBundle();
        },

        navegar(rota, parametroId) {
            try {
                let roteador = this.getOwnerComponent().getRouter();

                parametroId 
                ? roteador.navTo(rota, {
                    id: parametroId
                })
                : roteador.navTo(rota);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    });
});