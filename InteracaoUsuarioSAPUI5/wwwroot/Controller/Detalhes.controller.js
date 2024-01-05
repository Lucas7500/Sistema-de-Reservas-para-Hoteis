sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "../model/Formatter",
    "sap/ui/model/json/JSONModel",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (Controller, Formatter, JSONModel, ReservaRepository, MessageBox) => {
    "use strict";

    const CAMINHO_ROTA_DETALHES = "reservas.hoteis.controller.Detalhes";
    const STATUS_OK = 200;

    return Controller.extend(CAMINHO_ROTA_DETALHES, {
        formatter: Formatter,
        onInit() {
            let rota = this.getOwnerComponent().getRouter();
            const rotaDetalhes = 'detalhes';
            rota.getRoute(rotaDetalhes).attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota(evento) {
            try {
                const parametroArgumentos = "arguments";
                let idReserva = evento.getParameter(parametroArgumentos).id;
                this._obterReserva(idReserva);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },
        _obterReserva(id) {
            ReservaRepository.obterPorId(id)
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
        },

        aoClicarNavegarParaTelaListagem() {
            try {
                let rota = this.getOwnerComponent().getRouter();
                const rotaListagem = "listagem";
                rota.navTo(rotaListagem);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})