sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/RepositorioReservasHoteis",
	"sap/ui/core/routing/History"
], (Controller, formatter, JSONModel, RepositorioReservasHoteis, History) => {
    "use strict";

    const caminhoRotaListagem = "reservas.hoteis.controller.Listagem";
    const MODELO_LISTAGEM = "TabelaReservas";

    return Controller.extend(caminhoRotaListagem, {
        formatter: formatter,
        onInit() {
            const minhaRota = 'overview';
            
            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute(minhaRota).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota() {
            this._carregarLista();
        },

        _carregarLista() {
            RepositorioReservasHoteis.obterTodos()
                .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTAGEM));
        },

        aoPesquisarFiltrarReservas(filtro) {
            const parametroQuery = "query";
            let stringFiltro = filtro.getParameter(parametroQuery);

            RepositorioReservasHoteis.obterTodos(stringFiltro)
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
        },
        
        voltarPagina() {
            const oHistory = History.getInstance();
			const sPreviousHash = oHistory.getPreviousHash();

			if (sPreviousHash !== undefined) {
				window.history.go(-1);
			} else {
				const oRouter = this.getOwnerComponent().getRouter();
				oRouter.navTo("overview", {}, true);
            }
        }
    });
});