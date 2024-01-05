sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/Formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (Controller, Formatter, JSONModel, ReservaRepository, MessageBox) => {
    "use strict";

    const STATUS_OK = 200;
    const CAMINHO_ROTA_DETALHES = "reservas.hoteis.controller.Detalhes";

    return Controller.extend(CAMINHO_ROTA_DETALHES, {
        formatter: Formatter,
        onInit() {
            const rotaDetalhes = 'detalhes';

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute(rotaDetalhes).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota(evento) {
            try {
                const parametroArgumentos = "arguments";
                let idReserva = evento.getParameter(parametroArgumentos).id;

                ReservaRepository.obterPorId(idReserva)
                    .then(response => {
                        return response.status == STATUS_OK
                            ? response.json()
                            : Promise.reject(response);
                    })
                    .then(response => this.getView().setModel(new JSONModel(response)))
                    .catch(async erro => {
                        let mensagemErro = await erro.text();

                        MessageBox.warning(mensagemErro);
                    });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        navegarParaTelaListagem() {
            try {
                const rotaListagem = "listagem";
                let rota = this.getOwnerComponent().getRouter();
                rota.navTo(rotaListagem);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})