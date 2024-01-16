sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (BaseController, Formatter, ReservaRepository, MessageBox) => {
    "use strict";

    const CAMINHO_ROTA_DETALHES = "reservas.hoteis.controller.Detalhes";
    const MODELO_RESERVA = "reserva";
    let ID_RESERVA;

    return BaseController.extend(CAMINHO_ROTA_DETALHES, {
        formatter: Formatter,
        onInit() {
            const rotaDetalhes = "detalhes";
            this.vincularRota(rotaDetalhes, this._aoCoincidirRota);
        },

        _aoCoincidirRota(evento) {
            try {
                const parametroArgumentos = "arguments";
                const idReserva = evento.getParameter(parametroArgumentos).id;
                
                this._definirIdReserva(idReserva);
                this._obterReserva();
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        _obterReserva() {
            ReservaRepository.obterPorId(ID_RESERVA)
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

        _definirIdReserva(idReserva) {
            ID_RESERVA = idReserva;
        },

        aoClicarNavegarParaTelaListagem() {
            try {
                const rotaListagem = "listagem";
                this.navegarPara(rotaListagem);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarNavegarParaTelaEdicao() {
            try {
                const rotaEdicao = "edicao";
                this.navegarPara(rotaEdicao, ID_RESERVA);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})