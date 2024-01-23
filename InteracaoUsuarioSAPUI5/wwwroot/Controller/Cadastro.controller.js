sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox",
    "../Services/Validacao",
    "../Services/ProcessadorDeEventos",
    "sap/ui/core/ValueState"
], (BaseController, Formatter, ReservaRepository, MessageBox, Validacao, ProcessadorDeEventos, ValueState) => {
    "use strict";

    const CAMINHO_ROTA_CADASTRO = "reservas.hoteis.controller.Cadastro";
    const ROTA_LISTAGEM = "listagem";
    const ROTA_DETALHES = "detalhes";
    const MODELO_RESERVA = "reserva";
    const PARAMETRO_VALUE = "value";
    const ID_INPUT_NOME = "inputNome";
    const ID_INPUT_CPF = "inputCpf";
    const ID_INPUT_TELEFONE = "inputTelefone";
    const ID_INPUT_IDADE = "inputIdade";
    const ID_INPUT_CHECK_IN = "inputCheckIn";
    const ID_INPUT_CHECK_OUT = "inputCheckOut";
    const ID_INPUT_PRECO_ESTADIA = "inputPrecoEstadia";

    const ARRAY_ID_INPUTS = [
        ID_INPUT_NOME,
        ID_INPUT_CPF,
        ID_INPUT_TELEFONE,
        ID_INPUT_IDADE,
        ID_INPUT_CHECK_IN,
        ID_INPUT_CHECK_OUT,
        ID_INPUT_PRECO_ESTADIA
    ]

    return BaseController.extend(CAMINHO_ROTA_CADASTRO, {
        formatter: Formatter,

        onInit() {
            const rotaCadastro = "cadastro";
            const rotaEdicao = "edicao";

            Validacao.definirRecursosi18n(this.obterRecursosI18n());
            this.vincularRota(rotaCadastro, this._aoCoincidirRotaCadastro);
            this.vincularRota(rotaEdicao, this._aoCoincidirRotaEdicao);
        },

        _aoCoincidirRotaCadastro() {
            this._limparValueStateInputs();
            this._definirValorPadraoRadioButton();
            this._modeloReserva();
        },

        _aoCoincidirRotaEdicao(evento) {
            ProcessadorDeEventos.processarEvento(() => {
                this._limparValueStateInputs();
                this._definirValorPadraoRadioButton();
                this._definirReservaPeloId(this._obterIdPeloParametro(evento));
            })
        },

        _modeloReserva(reserva) {
            return reserva
                ? this._definirModeloPadraoEdicao(reserva)
                : this._definirModeloPadraoCadastro();
        },

        _definirModeloPadraoCadastro() {
            let reserva = {
                nome: String(),
                cpf: String(),
                telefone: String(),
                idade: String(),
                sexo: String(),
                checkIn: new Date().toISOString(),
                checkOut: new Date().toISOString(),
                precoEstadia: String(),
                pagamentoEfetuado: false
            };

            this.modelo(MODELO_RESERVA, reserva);
        },

        _definirModeloPadraoEdicao(reserva) {
            reserva.precoEstadia = Formatter.formataPrecoEstadia(reserva.precoEstadia);
            this.modelo(MODELO_RESERVA, reserva);
        },

        _definirValorPadraoRadioButton() {
            const idRadioButtonPagamentoNaoEfetuado = "radioButtonPagamentoNaoEfetuado";
            this.byId(idRadioButtonPagamentoNaoEfetuado).setSelected(true);
        },

        _definirValueStateInputValidado(input, valueStateText) {
            valueStateText
                ? input.setValueState(ValueState.Error).setValueStateText(valueStateText)
                : input.setValueState(ValueState.Success);
        },

        _definirValueStateInputsSemAlteracao(listaErrosValidacao) {
            for (let i = 0; i < ARRAY_ID_INPUTS.length; i++) {
                let input = this.byId(ARRAY_ID_INPUTS[i]);
                let valueStateInput = input.getValueState();

                if (valueStateInput == ValueState.None) {
                    this._definirValueStateInputValidado(input, listaErrosValidacao[i]);
                }
            }
        },

        _limparValueStateInputs() {
            for (let idInput of ARRAY_ID_INPUTS) {
                let input = this.byId(idInput);
                input.setValueState(ValueState.None);
            }
        },

        _obterIdPeloParametro(evento) {
            const parametroArguments = "arguments";
            return evento.getParameter(parametroArguments).id;
        },

        _obterReservaPreenchida() {
            let reserva = structuredClone(this.modelo(MODELO_RESERVA));

            reserva.idade = Number(reserva.idade);
            reserva.sexo = Number(reserva.sexo);
            reserva.precoEstadia = Number(Formatter.desformataPrecoEstadia(reserva.precoEstadia));

            return reserva;
        },

        _messageBoxConfirmacaoCancelamento(mensagemConfirmacao) {
            const idReserva = this.modelo(MODELO_RESERVA).id;

            MessageBox.confirm(mensagemConfirmacao, {
                actions: [MessageBox.Action.YES, MessageBox.Action.NO],
                emphasizedAction: MessageBox.Action.YES,
                onClose: (acao) => {
                    if (acao == MessageBox.Action.YES) {
                        idReserva
                            ? this.navegarPara(ROTA_DETALHES, idReserva)
                            : this.navegarPara(ROTA_LISTAGEM);
                    }
                }
            });
        },

        _definirReservaPeloId(id) {
            try {
                ReservaRepository.obterPorId(id)
                    .then(response => {
                        return response.ok
                            ? response.json()
                            : Promise.reject(response);
                    })
                    .then(reserva => this._modeloReserva(reserva))
                    .catch(async erro => {
                        let mensagemErro = await erro.text();
                        MessageBox.warning(mensagemErro);
                    });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        _criarReserva(reservaParaCriar) {
            try {
                const variavelSucessoSalvar = "sucessoSalvar";
                const mensagemSucessoSalvar = this.obterRecursosI18n().getText(variavelSucessoSalvar);

                ReservaRepository.criarReserva(reservaParaCriar)
                    .then(response => {
                        const statusCreated = 201;
                        return response.status == statusCreated
                            ? response.json()
                            : Promise.reject(response)
                    })
                    .then(reservaCriada => {
                        MessageBox.success(mensagemSucessoSalvar, {
                            onClose: () => {
                                this.navegarPara(ROTA_DETALHES, reservaCriada.id)
                            }
                        });
                    })
                    .catch(async erro => {
                        let mensagemErro = await erro.text();
                        MessageBox.warning(mensagemErro);
                    });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        _atualizarReserva(reservaParaAtualizar) {
            try {
                const variavelSucessoEditar = "sucessoEditar";
                const mensagemSucessoEditar = this.obterRecursosI18n().getText(variavelSucessoEditar);

                ReservaRepository.atualizarReserva(reservaParaAtualizar)
                    .then(response => {
                        const statusNoContent = 204;
                        return response.status == statusNoContent
                            ? MessageBox.success(mensagemSucessoEditar, {
                                onClose: () => {
                                    this.navegarPara(ROTA_DETALHES, reservaParaAtualizar.id)
                                }
                            })
                            : Promise.reject(response)
                    })
                    .catch(async erro => {
                        let mensagemErro = await erro.text();
                        MessageBox.warning(mensagemErro);
                    });
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarNavegarParaTelaAnterior() {
            try {
                const idReserva = this.modelo(MODELO_RESERVA).id;

                idReserva
                    ? this.navegarPara(ROTA_DETALHES, idReserva)
                    : this.navegarPara(ROTA_LISTAGEM);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarSalvarReserva() {
            try {
                const reservaPreenchida = this._obterReservaPreenchida();
                Validacao.validarReserva(reservaPreenchida);

                const listaErrosValidacao = Validacao.obterListaErros();
                const mensagensErroValidacao = Formatter.formataListaErros(listaErrosValidacao);
                this._definirValueStateInputsSemAlteracao(listaErrosValidacao);

                mensagensErroValidacao
                    ? MessageBox.warning(mensagensErroValidacao)
                    : reservaPreenchida.id
                        ? this._atualizarReserva(reservaPreenchida)
                        : this._criarReserva(reservaPreenchida);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarCancelarCadastro() {
            try {
                const variavelConfirmacaoCancelar = "confirmacaoCancelar";
                const mensagemConfirmacao = this.obterRecursosI18n().getText(variavelConfirmacaoCancelar);

                this._messageBoxConfirmacaoCancelamento(mensagemConfirmacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarNome(evento) {
            ProcessadorDeEventos.processarEvento(() => {
                const inputNome = evento.getSource();
                const valorNome = evento.getParameter(PARAMETRO_VALUE);
                const mensagemErroValidacao = Validacao.validarNome(valorNome);

                this._definirValueStateInputValidado(inputNome, mensagemErroValidacao);
            })
        },

        aoMudarValidarCpf(evento) {
            ProcessadorDeEventos.processarEvento(() => {
                const inputCpf = evento.getSource();
                const valorCpf = evento.getParameter(PARAMETRO_VALUE);
                const mensagemErroValidacao = Validacao.validarCpf(valorCpf);

                this._definirValueStateInputValidado(inputCpf, mensagemErroValidacao);
            })
        },

        aoMudarValidarTelefone(evento) {
            ProcessadorDeEventos.processarEvento(() => {
                const inputTelefone = evento.getSource();
                const valorTelefone = evento.getParameter(PARAMETRO_VALUE);
                const mensagemErroValidacao = Validacao.validarTelefone(valorTelefone);

                this._definirValueStateInputValidado(inputTelefone, mensagemErroValidacao);
            });
        },

        aoMudarValidarIdade(evento) {
            ProcessadorDeEventos.processarEvento(() => {
                const inputIdade = evento.getSource();
                const valorIdade = evento.getParameter(PARAMETRO_VALUE);
                const mensagemErroValidacao = Validacao.validarIdade(valorIdade);

                this._definirValueStateInputValidado(inputIdade, mensagemErroValidacao);
            });
        },

        aoMudarValidarCheckInECheckOut() {
            ProcessadorDeEventos.processarEvento(() => {
                const idReserva = this.modelo(MODELO_RESERVA).id;

                const inputCheckIn = this.byId(ID_INPUT_CHECK_IN);
                const inputCheckOut = this.byId(ID_INPUT_CHECK_OUT);

                const valorCheckIn = inputCheckIn.getValue();
                const valorCheckOut = inputCheckOut.getValue();

                let mensagemErroValidacaoCheckIn, mensagemErroValidacaoCheckOut;

                if (idReserva) {
                    mensagemErroValidacaoCheckIn = Validacao.validarCheckIn(valorCheckIn);
                    mensagemErroValidacaoCheckOut = Validacao.validarCheckOut(valorCheckOut, valorCheckIn);
                }
                else {
                    mensagemErroValidacaoCheckIn = Validacao.validarCheckInCadastro(valorCheckIn);
                    mensagemErroValidacaoCheckOut = Validacao.validarCheckOutCadastro(valorCheckOut, valorCheckIn);
                }

                this._definirValueStateInputValidado(inputCheckIn, mensagemErroValidacaoCheckIn);
                this._definirValueStateInputValidado(inputCheckOut, mensagemErroValidacaoCheckOut);
            });
        },

        aoMudarValidarPrecoEstadia(evento) {
            ProcessadorDeEventos.processarEvento(() => {
                const inputPrecoEstadia = evento.getSource();
                const valorPrecoEstadia = evento.getParameter(PARAMETRO_VALUE);
                const mensagemErroValidacao = Validacao.validarPrecoEstadia(valorPrecoEstadia);

                this._definirValueStateInputValidado(inputPrecoEstadia, mensagemErroValidacao);

                if (!mensagemErroValidacao) inputPrecoEstadia.setValue(Formatter.formataPrecoEstadia(valorPrecoEstadia));
            });
        }
    })
})