sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
    "sap/ui/core/library",
    "sap/m/Dialog",
    "sap/m/Button",
    "sap/m/library",
    "sap/m/Text"
], (Controller, formatter, JSONModel, ReservaRepository, coreLibrary, Dialog, Button, mobileLibrary, Text) => {
    "use strict";

    const CAMINHO_ROTA_LISTAGEM = "reservas.hoteis.controller.Listagem";
    const MODELO_LISTA = "TabelaReservas";

    return Controller.extend(CAMINHO_ROTA_LISTAGEM, {
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
                    .then(response => {
                        const statusOk = 200

                        if (response.status == statusOk) {
                            return response.json();
                        }
                        else {
                            return Promise.reject(response);
                        }
                    })
                    .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA))
                    .catch(async erro => {
                        let mensagemErro = await erro.text();

                        console.error(erro);
                        this._mostrarMensagemErro(mensagemErro);
                    })
            }
            catch (erro) {
                console.error(erro);
                this._mostrarMensagemErro(erro.message);
            }
        },

        _mostrarMensagemErro(mensagemErro) {
            var ButtonType = mobileLibrary.ButtonType;
            var DialogType = mobileLibrary.DialogType;
            var ValueState = coreLibrary.ValueState;
            const tituloDialog = "Erro";

            if (!this.oErrorMessageDialog) {
                this.oErrorMessageDialog = new Dialog({
                    type: DialogType.Message,
                    title: tituloDialog,
                    state: ValueState.Warning,
                    content: new Text({ text: mensagemErro }),
                    beginButton: new Button({
                        type: ButtonType.Emphasized,
                        text: "OK",
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
                        const statusOk = 200

                        if (response.status == statusOk) {
                            return response.json();
                        }
                        else {
                            return Promise.reject(response);
                        }
                    })
                    .then(response => this.getView().setModel(new JSONModel(response), MODELO_LISTA))
                    .catch(async erro => {
                        let mensagemErro = await erro.text();

                        console.error(erro);
                        this._mostrarMensagemErro(mensagemErro);
                    })
            } catch (erro) {
                console.error(erro);
                this._mostrarMensagemErro(erro.message);
            }
        },

        aoClicarAbrirAdicionar() {
            try {
                let rota = this.getOwnerComponent().getRouter();
                const rotaAdicionar = "adicionar";
                rota.navTo(rotaAdicionar);
            } catch (erro) {
                console.error(erro);
                this._mostrarMensagemErro(erro.message);
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
            } catch (erro) {
                console.error(erro);
                this._mostrarMensagemErro(erro.message);
            }
        }
    });
});