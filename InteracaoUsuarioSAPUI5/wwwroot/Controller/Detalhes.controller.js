sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../Repositorios/ReservaRepository",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History"
], (Controller, ReservaRepository, JSONModel, History) => {
    "use strict";

    const caminhoRotaDetalhes = "reservas.hoteis.controller.Detalhes";

    return Controller.extend(caminhoRotaDetalhes, {
        onInit() {
            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute('detalhes').attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota(oEvent) {
            let idReserva = oEvent.getParameter("arguments").id;
            
            ReservaRepository.obterPorId(idReserva)
            .then(response => response.json())
            .then(response => this.getView().setModel(new JSONModel(response)));
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