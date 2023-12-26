sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/RepositorioReservasHoteis",
    "sap/m/MessageToast"
], (Controller, formatter, JSONModel, RepositorioReservasHoteis, MessageToast) => {
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
            this._definirModelo();
        },

        _definirModelo() {
            RepositorioReservasHoteis.obterTodos()
                .then(response => this.getView().setModel(new JSONModel(response), "TabelaReservas"));
        },

        aoPesquisarFiltrarReservas(objetoPesquisa) {
        },

        aoClicarAdicionarReserva() {
            const mensagemTeste = "Botão tá funfando ainda";
            MessageToast.show(mensagemTeste);
        }
    });
});