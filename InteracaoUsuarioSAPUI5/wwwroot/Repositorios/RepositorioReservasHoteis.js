sap.ui.define([
    "sap/ui/model/json/JSONModel"
],(JSONModel) => {
    "use strict";

    return {
        obterTodos(controller) {
            fetch('/api/Reserva', { method: 'GET' })
                .then(response => response.json())
                .then(response => controller.getView().setModel(new JSONModel(response), "TabelaReservas"))
                .catch(err => console.log(err.message))
        },
        obterPorId(controller, id) {
            fetch(`/api/Reserva/${id}`, { method: 'GET' })
                .then(response => response.json())
                .then(response => controller.getView().setModel(new JSONModel([response]), "TabelaReservas"))
                .catch(err => console.log(err.message))
        }
    }
})