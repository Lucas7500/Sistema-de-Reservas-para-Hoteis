sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/formatter",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator",
    "../Repositorios/RepositorioReservasHoteis",
    "sap/m/MessageToast"
], (Controller, formatter, Filter, FilterOperator, RepositorioReservasHoteis, MessageToast) => {
    "use strict";

    const caminhoRotaListagem = "reservas.hoteis.controller.Listagem";

    return Controller.extend(caminhoRotaListagem, {
        formatter: formatter,
        onInit() {
            const minhaRota = 'overview';
            
            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute(minhaRota).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota() {
            RepositorioReservasHoteis.obterTodos(this.getView());
        },

        aoPesquisarFiltrarReservas(objetoPesquisa) {
        },

        aoClicarAdicionarReserva() {
            const mensagemTeste = "Botão tá funfando ainda";
            MessageToast.show(mensagemTeste);
        }
    });
});