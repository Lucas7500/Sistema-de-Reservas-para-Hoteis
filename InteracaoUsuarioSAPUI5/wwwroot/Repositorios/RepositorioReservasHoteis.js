sap.ui.define([
    "sap/ui/model/json/JSONModel"
],(JSONModel) => {
    "use strict";

    const ID_TABELA = "TabelaReservas";

    return {
        obterTodos(view) {
            const caminhoRotaObterTodos = '/api/Reserva';
            const metodoObterTodos = 'GET';

            fetch(caminhoRotaObterTodos, { method: metodoObterTodos })
                .then(response => response.json())
                .then(response => view.setModel(new JSONModel(response), ID_TABELA))
                .catch(err => console.log(err.message))
        },
        obterPorId(view, id) {
            const caminhoRotaObterPorId = `/api/Reserva/${id}`;
            const metodoObterPorId = 'GET';

            fetch(caminhoRotaObterPorId, { method: metodoObterPorId })
                .then(response => response.json())
                .then(response => view.setModel(new JSONModel([response]), ID_TABELA))
                .catch(err => console.log(err.message))
        }
    }
})