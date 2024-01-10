sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/Formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (Controller, Formatter, JSONModel, ReservaRepository, MessageBox) => {
    "use strict";

    const CAMINHO_ROTA_LISTAGEM = "reservas.hoteis.controller.Listagem";
    const MODELO_LISTA = "TabelaReservas";

    return Controller.extend(CAMINHO_ROTA_LISTAGEM, {
        formatter: Formatter,
        onInit() {
            let rota = this.getOwnerComponent().getRouter();
            const rotaLista = "listagem";
            rota.getRoute(rotaLista).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota() {
            this._carregarLista();
        },

        _carregarLista() {
            try {
                this._obterReservas();
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        _obterReservas(filtro) {
            ReservaRepository.obterTodos(filtro)
                .then(response => {
                    return response.ok
                        ? response.json()
                        : Promise.reject(response);
                })
                .then(reservas => this.getView().setModel(new JSONModel(reservas), MODELO_LISTA))
                .catch(async erro => {
                    let mensagemErro = await erro.text();
                    MessageBox.warning(mensagemErro);
                })
        },

        aoPesquisarFiltrarReservas(filtro) {
            try {
                const parametroQuery = "query";
                let stringFiltro = filtro.getParameter(parametroQuery);
                this._obterReservas(stringFiltro);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarAbrirCadastro() {
            try {
                let rota = this.getOwnerComponent().getRouter();
                const rotaCadastro = "cadastro";
                rota.navTo(rotaCadastro);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarAbrirDetalhes(evento) {
            try {
                const propriedadeId = "id";
                let idReserva = evento.getSource().getBindingContext(MODELO_LISTA).getProperty(propriedadeId);

                const rotaDetalhes = "detalhes";
                let rota = this.getOwnerComponent().getRouter();
                rota.navTo(rotaDetalhes, {
                    id: idReserva
                });
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    });
});