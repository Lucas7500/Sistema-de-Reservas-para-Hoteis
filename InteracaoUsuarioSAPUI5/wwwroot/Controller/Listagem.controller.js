sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox",
    "../Services/ProcessadorDeEventos"
], (BaseController, Formatter, ReservaRepository, MessageBox, ProcessadorDeEventos) => {
    "use strict";

    const CAMINHO_ROTA_LISTAGEM = "reservas.hoteis.controller.Listagem";
    const MODELO_LISTA = "TabelaReservas";

    return BaseController.extend(CAMINHO_ROTA_LISTAGEM, {
        formatter: Formatter,

        onInit() {
            const rotaListagem = "listagem";
            this.vincularRota(rotaListagem, this._aoCoincidirRota);
        },

        _aoCoincidirRota() {
            this._modeloListaReservas();
        },

        _modeloListaReservas(filtro) {
            try {
                ReservaRepository.obterTodos(filtro)
                    .then(response => {
                        return response.ok
                            ? response.json()
                            : Promise.reject(response);
                    })
                    .then(reservas => this.modelo(MODELO_LISTA, reservas))
                    .catch(async erro => MessageBox.warning(await erro.text()))
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoPesquisarFiltrarReservas(filtro) {
            ProcessadorDeEventos.processarEvento(() => {
                const parametroQuery = "query";
                const stringFiltro = filtro.getParameter(parametroQuery);

                this._modeloListaReservas(stringFiltro);
            });
        },

        aoClicarAbrirCadastro() {
            ProcessadorDeEventos.processarEvento(() => {
                const rotaCadastro = "cadastro";
                this.navegarPara(rotaCadastro);
            });
        },

        aoClicarAbrirDetalhes(evento) {
            ProcessadorDeEventos.processarEvento(() => {
                const propriedadeId = "id";
                const idReserva = evento
                    .getSource()
                    .getBindingContext(MODELO_LISTA)
                    .getProperty(propriedadeId);

                const rotaDetalhes = "detalhes";
                this.navegarPara(rotaDetalhes, idReserva);
            });
        }
    });
});