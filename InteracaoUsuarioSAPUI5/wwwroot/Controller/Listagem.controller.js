sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/Formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (Controller, Formatter, JSONModel, ReservaRepository, MessageBox) => {
    "use strict";

    const STATUS_OK = 200;
    const MODELO_LISTA = "TabelaReservas";
    const CAMINHO_ROTA_LISTAGEM = "reservas.hoteis.controller.Listagem";

    return Controller.extend(CAMINHO_ROTA_LISTAGEM, {
        formatter: Formatter,
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
                .then(response => {
                    return response.status == STATUS_OK
                        ? response.json()
                        : Promise.reject(response);
                })
                .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA))
                .catch(async erro => {
                    let mensagemErro = await erro.text();
                    MessageBox.warning(mensagemErro);
                })
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoPesquisarFiltrarReservas(filtro) {
            try {
                const parametroQuery = "query";
                let stringFiltro = filtro.getParameter(parametroQuery);

                ReservaRepository.obterTodos(stringFiltro)
                    .then(response => {
                        return response.status == STATUS_OK
                            ? response.json()
                            : Promise.reject(response);
                    })
                    .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA))
                    .catch(async erro => {
                        let mensagemErro = await erro.text();
                        MessageBox.warning(mensagemErro);
                    })
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
                let idReserva = evento
                    .getSource()
                    .getBindingContext(MODELO_LISTA)
                    .getProperty(propriedadeId);
                
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