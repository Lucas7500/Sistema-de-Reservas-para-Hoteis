sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox",
    "../Services/Validacao",
    "sap/ui/core/ValueState"
], (BaseController, Formatter, ReservaRepository, MessageBox, Validacao, ValueState) => {
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
            const recursosi18n = this.obterRecursosI18n();

            Validacao.definirRecursosi18n(recursosi18n);
            this.vincularRota(rotaCadastro, this._aoCoincidirRotaCadastro);
            this.vincularRota(rotaEdicao, this._aoCoincidirRotaEdicao);
        },

        _aoCoincidirRotaCadastro() {
            this._limparValueStateInputs();
            this._definirValorPadraoRadioButton();
            this._modeloReserva();
        },

        _aoCoincidirRotaEdicao(evento) {
            try {
                this._limparValueStateInputs();
                this._definirValorPadraoRadioButton();
                this._definirReservaPeloId(this._obterIdPeloParametro(evento));
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
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
            let reserva = this.modelo(MODELO_RESERVA);

            reserva.idade = Number(reserva.idade);
            reserva.sexo = Number(reserva.sexo);
            reserva.precoEstadia = Number(Formatter.desformataPrecoEstadia(reserva.precoEstadia));

            return reserva;
        },

        _messageBoxConfirmacaoCancelamento(mensagemConfirmacao, navegarParaTelaAnterior) {
            MessageBox.confirm(mensagemConfirmacao, {
                actions: [MessageBox.Action.YES, MessageBox.Action.NO],
                emphasizedAction: MessageBox.Action.YES,
                onClose: (acao) => {
                    if (acao == MessageBox.Action.YES) {
                        navegarParaTelaAnterior();
                    }
                }
            });
        },

        _definirReservaPeloId(id) {
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
        },

        _criarReserva(reservaParaCriar, navegarParaTelaReservaCriada) {
            const recursosi18n = this.obterRecursosI18n();
            const variavelSucessoSalvar = "sucessoSalvar";
            const mensagemSucessoSalvar = recursosi18n.getText(variavelSucessoSalvar);

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
                            navegarParaTelaReservaCriada(ROTA_DETALHES, reservaCriada.id)
                        }
                    })
                })
                .catch(async erro => {
                    let mensagemErro = await erro.text();
                    MessageBox.warning(mensagemErro);
                });
        },

        _atualizarReserva(reservaParaAtualizar, navegarTelaReservaEditada) {
            const recursosi18n = this.obterRecursosI18n();
            const variavelSucessoEditar = "sucessoEditar";
            const mensagemSucessoEditar = recursosi18n.getText(variavelSucessoEditar);

            ReservaRepository.atualizarReserva(reservaParaAtualizar)
                .then(response => {
                    const statusNoContent = 204;
                    return response.status == statusNoContent
                        ? MessageBox.success(mensagemSucessoEditar, {
                            onClose: () => {
                                navegarTelaReservaEditada(ROTA_DETALHES, reservaParaAtualizar.id)
                            }
                        })
                        : Promise.reject(response)
                })
                .catch(async erro => {
                    let mensagemErro = await erro.text();
                    MessageBox.warning(mensagemErro);
                });
        },

        _navegarParaTelaAnterior() {
            const idReserva = this.modelo(MODELO_RESERVA).id;

            idReserva
                ? this.navegarPara(ROTA_DETALHES, idReserva)
                : this.navegarPara(ROTA_LISTAGEM);
        },

        aoClicarNavegarParaTelaAnterior() {
            try {
                this._navegarParaTelaAnterior();
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarSalvarReserva() {
            try {
                let reservaPreenchida = this._obterReservaPreenchida();
                Validacao.validarReserva(reservaPreenchida);
                
                let listaErrosValidacao = Validacao.obterListaErros();
                let mensagensErroValidacao = Formatter.formataListaErros(listaErrosValidacao);
                
                this._definirValueStateInputsSemAlteracao(listaErrosValidacao);

                mensagensErroValidacao
                    ? MessageBox.warning(mensagensErroValidacao)
                    : reservaPreenchida.id
                        ? this._atualizarReserva(reservaPreenchida, this.navegarPara)
                        : this._criarReserva(reservaPreenchida, this.navegarPara);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarCancelarCadastro() {
            try {
                const recursosi18n = this.obterRecursosI18n();
                const variavelConfirmacaoCancelar = "confirmacaoCancelar";
                const mensagemConfirmacao = recursosi18n.getText(variavelConfirmacaoCancelar);

                this._messageBoxConfirmacaoCancelamento(mensagemConfirmacao, this._navegarParaTelaAnterior);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarNome(evento) {
            try {
                let inputNome = evento.getSource();
                let valorNome = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarNome(valorNome);

                this._definirValueStateInputValidado(inputNome, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarCpf(evento) {
            try {
                let inputCpf = evento.getSource();
                let valorCpf = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarCpf(valorCpf);

                this._definirValueStateInputValidado(inputCpf, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarTelefone(evento) {
            try {
                let inputTelefone = evento.getSource();
                let valorTelefone = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarTelefone(valorTelefone);

                this._definirValueStateInputValidado(inputTelefone, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarIdade(evento) {
            try {
                let inputIdade = evento.getSource();
                let valorIdade = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarIdade(valorIdade);

                this._definirValueStateInputValidado(inputIdade, mensagemErroValidacao);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarCheckInECheckOut() {
            try {
                let inputCheckIn = this.byId(ID_INPUT_CHECK_IN);
                let inputCheckOut = this.byId(ID_INPUT_CHECK_OUT);

                let valorCheckIn = inputCheckIn.getValue();
                let valorCheckOut = inputCheckOut.getValue();

                let mensagemErroValidacaoCheckIn = Validacao.validarCheckIn(valorCheckIn);
                let mensagemErroValidacaoCheckOut = Validacao.validarCheckOut(valorCheckOut, valorCheckIn);

                this._definirValueStateInputValidado(inputCheckIn, mensagemErroValidacaoCheckIn);
                this._definirValueStateInputValidado(inputCheckOut, mensagemErroValidacaoCheckOut);
            }
            catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoMudarValidarPrecoEstadia(evento) {
            try {
                let inputPrecoEstadia = evento.getSource();
                let valorPrecoEstadia = evento.getParameter(PARAMETRO_VALUE);
                let mensagemErroValidacao = Validacao.validarPrecoEstadia(valorPrecoEstadia);

                this._definirValueStateInputValidado(inputPrecoEstadia, mensagemErroValidacao);

                if (!mensagemErroValidacao) inputPrecoEstadia.setValue(Formatter.formataPrecoEstadia(valorPrecoEstadia));
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})