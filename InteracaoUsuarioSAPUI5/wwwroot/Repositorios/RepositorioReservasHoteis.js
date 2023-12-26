sap.ui.define([
    "sap/ui/model/json/JSONModel"
],(JSONModel) => {
    "use strict";

    return {
        obterTodos() {
            const caminhoRotaObterTodos = '/api/Reserva';
            const metodoObterTodos = 'GET';

            return fetch(caminhoRotaObterTodos, { method: metodoObterTodos })
                .then(response => response.json())
                .catch(erro => console.log(erro.message))
        },
        obterPorId(id) {
            const caminhoRotaObterPorId = `/api/Reserva/${id}`;
            const metodoObterPorId = 'GET';

            fetch(caminhoRotaObterPorId, { method: metodoObterPorId })
                .then(response => response.json())
                .catch(erro => console.log(erro.message))
        }
    }
})