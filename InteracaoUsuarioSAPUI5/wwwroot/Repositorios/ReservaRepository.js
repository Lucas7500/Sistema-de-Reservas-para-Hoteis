sap.ui.define([], () => {
    "use strict";

    const ENDPOINT_RESERVA = '/api/Reserva';

    return {
        obterTodos(filtro) {
            let query = ENDPOINT_RESERVA;
            if (filtro != undefined || filtro != null) {
                query += `?filtro=${filtro}`
            }

            return fetch(query);
        },

        obterPorId(id) {
            return fetch(`${ENDPOINT_RESERVA}/${id}`);
        },

        criarReserva(reservaParaCriar) {
            let opcoes = {
                method: "POST",
                body: JSON.stringify(reservaParaCriar),
                headers: { "Content-type": "application/json; charset=UTF-8" }
            };

            return fetch(ENDPOINT_RESERVA, opcoes);
        },

        atualizarReserva(reservaParaAtualizar) {
            let opcoes = {
                method: "PUT",
                body: JSON.stringify(reservaParaAtualizar),
                headers: { "Content-type": "application/json; charset=UTF-8" }
            };

            return fetch(`${ENDPOINT_RESERVA}/${reservaParaAtualizar.id}`, opcoes);
        }
    }
})