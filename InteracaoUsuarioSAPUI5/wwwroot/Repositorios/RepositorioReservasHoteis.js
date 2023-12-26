sap.ui.define([
    "sap/ui/model/json/JSONModel"
],(JSONModel) => {
    "use strict";

    return {
        async obterTodos(filtro="") {
            const caminhoRotaObterTodos = `/api/Reserva?filtro=${filtro}`;
            const metodoObterTodos = 'GET';

            try {
                const response = await fetch(caminhoRotaObterTodos, { method: metodoObterTodos });
                return await response.json();
            } catch (erro) {
                return console.log(erro.message);
            }
        },
        async obterPorId(id) {
            const caminhoRotaObterPorId = `/api/Reserva/${id}`;
            const metodoObterPorId = 'GET';

            try {
                const response = await fetch(caminhoRotaObterPorId, { method: metodoObterPorId });
                return await response.json();
            } catch (erro) {
                return console.log(erro.message);
            }
        }
    }
})