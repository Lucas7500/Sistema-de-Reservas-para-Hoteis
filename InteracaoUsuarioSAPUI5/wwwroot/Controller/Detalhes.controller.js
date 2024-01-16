sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (BaseController, Formatter, ReservaRepository, MessageBox) => {
    "use strict";

    const CAMINHO_ROTA_DETALHES = "reservas.hoteis.controller.Detalhes";
    const MODELO_RESERVA = "reserva";

    return BaseController.extend(CAMINHO_ROTA_DETALHES, {
        formatter: Formatter,
        onInit() {
            const rotaDetalhes = "detalhes";
            this.vincularRota(rotaDetalhes, this._aoCoincidirRota);
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
                    return response.ok
                        ? response.json()
                        : Promise.reject(response);
                })
                .then(reserva => this.modelo(MODELO_RESERVA, reserva))
                .catch(async erro => {
                    let mensagemErro = await erro.text();
                    MessageBox.warning(mensagemErro);
                });
        },

        aoClicarNavegarParaTelaListagem() {
            try {
                const rotaListagem = "listagem";
                this.navegarPara(rotaListagem);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})