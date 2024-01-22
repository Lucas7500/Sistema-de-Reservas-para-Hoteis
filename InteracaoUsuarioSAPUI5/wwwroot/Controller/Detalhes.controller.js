sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox"
], (BaseController, Formatter, ReservaRepository, MessageBox) => {
    "use strict";

    const CAMINHO_ROTA_DETALHES = "reservas.hoteis.controller.Detalhes";
    const MODELO_RESERVA = "reserva";
    const ROTA_LISTAGEM = "listagem";

    return BaseController.extend(CAMINHO_ROTA_DETALHES, {
        formatter: Formatter,

        onInit() {
            const rotaDetalhes = "detalhes";
            this.vincularRota(rotaDetalhes, this._aoCoincidirRota);
        },

        _aoCoincidirRota(evento) {
            this._definirReservaPeloId(this._obterIdPeloParametro(evento));
        },

        _obterIdPeloParametro(evento) {
            const parametroArguments = "arguments";
            return evento.getParameter(parametroArguments).id;
        },

        _definirReservaPeloId(id) {
            try {
                ReservaRepository.obterPorId(id)
                    .then(response => {
                        return response.ok
                            ? response.json()
                            : Promise.reject(response);
                    })
                    .then(reserva => this.modelo(MODELO_RESERVA, reserva))
                    .catch(async erro => MessageBox.warning(await erro.text()));
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        _removerReserva() {
            try {
                const idReserva = this.modelo(MODELO_RESERVA).id;
                const variavelSucessoRemover = "sucessoRemover";
                const mensagemSucesso = this.obterRecursosI18n().getText(variavelSucessoRemover);
                
                return ReservaRepository.removerReserva(idReserva)
                    .then(() => MessageBox.success(mensagemSucesso, {
                        onClose: () => {
                            this.navegarPara(ROTA_LISTAGEM);
                        }
                    }))
                    .catch(async erro => MessageBox.warning(await erro.text()));
    } catch (erro) {
        MessageBox.warning(erro.message);
    }
},

    aoClicarNavegarParaTelaListagem() {
    try {
        this.navegarPara(ROTA_LISTAGEM);
    } catch(erro) {
        MessageBox.warning(erro.message);
    }
},

    aoClicarNavegarParaTelaEdicao() {
    try {
        const rotaEdicao = "edicao";
        const idReserva = this.modelo(MODELO_RESERVA).id;

        this.navegarPara(rotaEdicao, idReserva);
    } catch(erro) {
        MessageBox.warning(erro.message);
    }
},

    aoClicarRemoverReserva() {
    const variavelConfirmacaoRemocao = "confirmacaoRemocao";
    const mensagemConfirmacao = this.obterRecursosI18n().getText(variavelConfirmacaoRemocao);

    MessageBox.confirm(mensagemConfirmacao, {
        actions: [MessageBox.Action.YES, MessageBox.Action.NO],
        emphasizedAction: MessageBox.Action.YES,
        onClose: (acao) => {
            if (acao == MessageBox.Action.YES) {
                this._removerReserva();
            }
        }
    })
}
    })
})