sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
], (Controller, formatter, JSONModel, ReservaRepository) => {
    "use strict";

    const caminhoRotaListagem = "reservas.hoteis.controller.Listagem";
    const MODELO_LISTA = "TabelaReservas";

    return Controller.extend(caminhoRotaListagem, {
        formatter: formatter,
        onInit() {
            const rotaLista = 'listagem';

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute(rotaLista).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota() {
            this._carregarLista();
        },

        _carregarLista() {
            ReservaRepository.obterTodos()
                .then(async response => await response.json())
                .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA));
        },

        aoPesquisarFiltrarReservas(filtro) {
            const parametroQuery = "query";
            let stringFiltro = filtro.getParameter(parametroQuery);

            ReservaRepository.obterTodos(stringFiltro)
                .then(async response => await response.json())
                .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA));
        },

        aoClicarAbrirAdicionar() {
            let rotaAdicionar = this.getOwnerComponent().getRouter();
            const paginaAdicionar = "adicionar";
            rotaAdicionar.navTo(paginaAdicionar);
        },

        aoClicarAbrirDetalhes() {
            let rotaDetalhes = this.getOwnerComponent().getRouter();
            const paginaDetalhes = "detalhes";
            rotaDetalhes.navTo(paginaDetalhes);
        }
    });
});