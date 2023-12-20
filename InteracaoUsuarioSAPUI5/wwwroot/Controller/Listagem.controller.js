sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "../model/formatter",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator",
    "../Repositorios/RepositorioReservasHoteis"

], (Controller, JSONModel, formatter, Filter, FilterOperator, RepositorioReservasHoteis) => {
    "use strict";

    return Controller.extend("reservas.hoteis.controller.Listagem", {
        formatter: formatter,
        onInit() {
            const oViewModel = new JSONModel({
                currency: "BRL"
            });
            this.getView().setModel(oViewModel, "view");

            RepositorioReservasHoteis.obterTodos(this);
        },
        aoPesquisarFiltrarReservas(oEvent) {
           const aFilter = [];
           const sQuery = oEvent.getParameter("query");
           if (sQuery) {
              aFilter.push(new Filter("nome", FilterOperator.Contains, sQuery));
           }
  
           const oList = this.byId("TabelaReservas");
           const oBinding = oList.getBinding("items");
           oBinding.filter(aFilter);
        }
    });
});