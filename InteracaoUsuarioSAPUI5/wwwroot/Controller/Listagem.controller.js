sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], (Controller, JSONModel) => {
    "use strict";

    return Controller.extend("reservas.hoteis.controller.Listagem", {
        onInit() {
            fetch('/api/Reserva', { method: 'GET' })
                .then(response => response.json())
                .then(response => this.getView().setModel(new JSONModel(response), "TabelaReservas"))
                .catch(err => console.log(err.message))
        }
    });
});