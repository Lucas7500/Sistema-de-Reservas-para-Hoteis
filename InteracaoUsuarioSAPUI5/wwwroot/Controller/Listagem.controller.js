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
            try {
                ReservaRepository.obterTodos()
                    .then(response => response.json())
                    .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA));
            } catch (error) {
                console.error(error);    
            }
        },
        
        aoPesquisarFiltrarReservas(filtro) {
            try {
                const parametroQuery = "query";
                let stringFiltro = filtro.getParameter(parametroQuery);
                
                ReservaRepository.obterTodos(stringFiltro)
                .then(response => response.json())
                .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA));
            } catch (error) {
                console.error(error);    
            }
        },
        
        aoClicarAbrirAdicionar() {
            try {
                let rota = this.getOwnerComponent().getRouter();
                const rotaAdicionar = "adicionar";
                rota.navTo(rotaAdicionar);
            } catch (error) {
                console.error(error);    
            }
        },
        
        aoClicarAbrirDetalhes(linhaReserva) {
            try {
                const reserva = linhaReserva.getSource();
                let rota = this.getOwnerComponent().getRouter();
                const rotaDetalhes = "detalhes";
                rota.navTo(rotaDetalhes, {
                    id: window.encodeURIComponent(reserva.getBindingContext("TabelaReservas").getPath().substr(1))
                });
            } catch (error) {
                console.error(error);    
            }
        }
    });
});