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
            this._aoCoincidirRota();
        },

        _aoCoincidirRota() {
            RepositorioReservasHoteis.obterTodos(this);
        },

        aoPesquisarFiltrarReservas(oEvent) {
            const idTabela = "TabelaReservas";
            const propriedadeFiltrada = "nome";
            const parametroBinding = "items";
            const parametroQuery = "query"

            const arrayFiltrado = [];
            const stringQuery = oEvent.getParameter(parametroQuery);
            if (stringQuery) {
                arrayFiltrado.push(new Filter(propriedadeFiltrada, FilterOperator.Contains, stringQuery));
            }

            const listaReservas = this.byId(idTabela);
            const objetoBinding = listaReservas.getBinding(parametroBinding);
            objetoBinding.filter(arrayFiltrado);
        },

        aoClicarAdicionarReserva() {
            const mensagemTeste = "Botão tá funfando ainda";
            MessageToast.show(mensagemTeste);
        }
    });
});