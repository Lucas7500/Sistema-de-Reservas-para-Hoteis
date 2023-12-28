sap.ui.define([],() => {
    "use strict";

    const endpointReservaController = '/api/Reserva';

    return {
         async obterTodos(filtro="") {
            let filtroVazio = "";
            let metodoObterTodos = (filtro == filtroVazio);
            let caminhoRotaObterTodos = metodoObterTodos ? `${endpointReservaController}` : `${endpointReservaController}?filtro=${filtro}`;

            try {
                return await fetch(caminhoRotaObterTodos);
            } catch (erro) {
                return console.log(erro.message);
            }
        },
        async obterPorId(id) {
            const caminhoRotaObterPorId = `${endpointReservaController}/${id}`;

            try {
                return await fetch(caminhoRotaObterPorId);
            } catch (erro) {
                return console.log(erro.message);
            }
        }
    }
})