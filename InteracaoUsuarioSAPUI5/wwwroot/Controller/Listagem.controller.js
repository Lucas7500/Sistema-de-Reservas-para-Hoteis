sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "../model/formatter",

], (Controller, JSONModel, formatter) => {
    "use strict";

    return Controller.extend("reservas.hoteis.controller.Listagem", {
        formatter: formatter,
        onInit() {
            const oViewModel = new JSONModel({
                currency: "BRL"
            });
            this.getView().setModel(oViewModel, "view");

            this.ObterTodos();
        },
        ObterTodos() {
            fetch('/api/Reserva', { method: 'GET' })
                .then(response => response.json())
                .then(response => this.getView().setModel(new JSONModel(response), "TabelaReservas"))
                .catch(err => console.log(err.message))
        }
    });
});