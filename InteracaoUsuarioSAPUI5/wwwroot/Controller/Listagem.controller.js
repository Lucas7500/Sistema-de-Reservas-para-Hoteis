sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
], (Controller, formatter, JSONModel, ReservaRepository) => {
    "use strict";

    const caminhoRotaListagem = "reservas.hoteis.controller.Listagem";
    const MODELO_LISTAGEM = "TabelaReservas";

    return Controller.extend(caminhoRotaListagem, {
        formatter: formatter,
        onInit() {
            const minhaRota = 'listagem';
            
            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute(minhaRota).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota() {
            this._carregarLista();
        },

        _carregarLista() {
            ReservaRepository.obterTodos()
                .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTAGEM));
        },

        aoPesquisarFiltrarReservas(filtro) {
            const parametroQuery = "query";
            let stringFiltro = filtro.getParameter(parametroQuery);

            ReservaRepository.obterTodos(stringFiltro)
                .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTAGEM));
        },

        aoClicarAbrirAdicionar() {
            let rota = this.getOwnerComponent().getRouter();
            const paginaAdicionar = "adicionar";
            rota.navTo(paginaAdicionar);
        },

        aoClicarAbrirDetalhes() {
            let rota = this.getOwnerComponent().getRouter();
            const paginaDetalhes = "detalhes";
            rota.navTo(paginaDetalhes);
        }
    });
});