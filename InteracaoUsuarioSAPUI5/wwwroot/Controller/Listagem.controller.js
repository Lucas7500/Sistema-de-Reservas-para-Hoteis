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

    const caminhoRotaListagem = "reservas.hoteis.controller.Listagem";
    const MODELO_LISTA = "TabelaReservas";

    return Controller.extend(caminhoRotaListagem, {
        formatter: formatter,
        onInit() {
            const rotaLista = 'listagem';
            alert("aaaaaaaaa")
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
                    .catch(erro => {
                        this._mostrarMensagemErro();
                    })
            }
            catch (erro) {
                console.error(erro);
            }
        },

        _mostrarMensagemErro() {
            var ButtonType = mobileLibrary.ButtonType;
            var DialogType = mobileLibrary.DialogType;
            var ValueState = coreLibrary.ValueState;

            if (!this.oErrorMessageDialog) {
                this.oErrorMessageDialog = new Dialog({
                    type: DialogType.Message,
                    title: "Error",
                    state: ValueState.Warning,
                    content: new Text({ text: "The only error you can make is to not even try." }),
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