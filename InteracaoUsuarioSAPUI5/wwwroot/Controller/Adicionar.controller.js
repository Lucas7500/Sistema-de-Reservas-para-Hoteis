sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History"
], (Controller, History) => {
    "use strict";

    const CAMINHO_ROTA_ADICIONAR = "reservas.hoteis.controller.Adicionar";

    return Controller.extend(CAMINHO_ROTA_ADICIONAR, {
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