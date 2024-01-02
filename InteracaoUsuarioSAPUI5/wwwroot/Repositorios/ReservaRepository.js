sap.ui.define([],() => {
    "use strict";

    const ENDPOINT_RESERVA = '/api/Reserva';

    return {
         obterTodos(filtro) {
            let query = ENDPOINT_RESERVA;
            if(filtro != (undefined || null)){
                query += `?filtro=${filtro}`
            }

            return fetch(query);
        },

        obterPorId(id) {
            return fetch(`${ENDPOINT_RESERVA}/${id}`);
        }
    }
})