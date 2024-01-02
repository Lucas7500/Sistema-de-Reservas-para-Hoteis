sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/Formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
    "sap/ui/core/library",
    "sap/m/Dialog",
    "sap/m/Button",
    "sap/m/library",
    "sap/m/Text"
], (Controller, Formatter, JSONModel, ReservaRepository, CoreLibrary, Dialog, Button, MobileLibrary, Text) => {
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
                    this._mostrarMensagemErro(mensagemErro);
                })
            }
            catch (erro) {
                this._mostrarMensagemErro(erro.message);
            }
        },

        _mostrarMensagemErro(mensagemErro) {
            var ButtonType = MobileLibrary.ButtonType;
            var DialogType = MobileLibrary.DialogType;
            var ValueState = CoreLibrary.ValueState;
            const tituloDialog = "Erro";

            if (!this.oErrorMessageDialog) {
                const textoBotao = "OK";
                this.oErrorMessageDialog = new Dialog({
                    type: DialogType.Message,
                    title: tituloDialog,
                    state: ValueState.Warning,
                    content: new Text({ text: mensagemErro }),
                    beginButton: new Button({
                        type: ButtonType.Emphasized,
                        text: textoBotao,
                        press: function () {
                            this.oErrorMessageDialog.close();
                        }.bind(this)
                    })
                });
            }

            this.oErrorMessageDialog.open();
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
                        this._mostrarMensagemErro(mensagemErro);
                    })
            } catch (erro) {
                this._mostrarMensagemErro(erro.message);
            }
        },

        aoClicarAbrirAdicionar() {
            try {
                let rota = this.getOwnerComponent().getRouter();
                const rotaAdicionar = "adicionar";
                rota.navTo(rotaAdicionar);
            } catch (erro) {
                this._mostrarMensagemErro(erro.message);
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
                this._mostrarMensagemErro(erro.message);
            }
        }
    });
});