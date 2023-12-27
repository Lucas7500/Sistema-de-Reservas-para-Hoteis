sap.ui.define([
    "sap/ui/core/mvc/Controller",
	"sap/ui/core/routing/History"
], (Controller, History) => {
    "use strict";

    const caminhoRotaDetalhes = "reservas.hoteis.controller.Detalhes";

    return Controller.extend(caminhoRotaDetalhes, {
        voltarPagina() {
            const oHistory = History.getInstance();
			const sPreviousHash = oHistory.getPreviousHash();

			if (sPreviousHash !== undefined) {
				window.history.go(-1);
			} else {
				const oRouter = this.getOwnerComponent().getRouter();
				oRouter.navTo("overview", {}, true);
            }
        }
    })
})