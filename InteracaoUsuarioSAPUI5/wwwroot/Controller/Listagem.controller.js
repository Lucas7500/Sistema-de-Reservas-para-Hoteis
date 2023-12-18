sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], (Controller, JSONModel) => {
    return Controller.extend("reservas.hoteis.controller.Listagem", {
        onInit() {
            fetch("https://localhost:7192/api/Reserva")
                .then(T => T.json())
                .then(console.log)
        }
    });
});