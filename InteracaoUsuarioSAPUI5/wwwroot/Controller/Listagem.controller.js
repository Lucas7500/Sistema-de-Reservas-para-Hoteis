sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (BaseController, Formatter, ReservaRepository, MessageBox) => {
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
                .then(reservas => this.modelo(MODELO_LISTA, reservas))
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
                const rotaCadastro = "cadastro";
                this.navegarPara(rotaCadastro);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarAbrirDetalhes(evento) {
            try {
                const propriedadeId = "id";
                const idReserva = evento.getSource().getBindingContext(MODELO_LISTA).getProperty(propriedadeId);

                const rotaDetalhes = "detalhes";
                this.navegarPara(rotaDetalhes, idReserva);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    });
});