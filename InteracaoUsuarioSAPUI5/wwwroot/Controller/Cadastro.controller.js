sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository"
], (Controller, History, JSONModel, ReservaRepository) => {
    "use strict";

    const CAMINHO_ROTA_CADASTRO = "reservas.hoteis.controller.Cadastro";

    return Controller.extend(CAMINHO_ROTA_CADASTRO, {
        onInit() {
            this._defineModeloPadraoReserva();
        },

        _defineModeloPadraoReserva() {
            reserva.nome = null;
            reserva.cpf = null;
            reserva.telefone = null;
            reserva.idade = null;
            reserva.sexo = null;
            reserva.checkIn = null;
            reserva.checkOut = null;
            reserva.precoEstadia = null;
            reserva.pagamentoEfetuado = false;
            
            this.getView().setModel(new JSONModel(reserva));
        },

        voltarPagina() {
            const oHistory = History.getInstance();
            const sPreviousHash = oHistory.getPreviousHash();

            if (sPreviousHash !== undefined) {
                window.history.go(-1);
            } else {
                const rotaLista = "listagem";
                const oRouter = this.getOwnerComponent().getRouter();
                oRouter.navTo(rotaLista, {}, true);
            }
        },


        aoClicarSalvarReserva() {
            reserva.idade = parseInt(reserva.idade);
            reserva.sexo = parseInt(reserva.sexo);
            reserva.precoEstadia = parseFloat(reserva.precoEstadia);
            
            console.log(reserva);
            ReservaRepository.criarReserva(reserva);
            
            this.voltarPagina();
            this._defineModeloPadraoReserva();
        }
    })
})