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
    const STRING_VAZIA = "";

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
            window.arr = this
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
            this._limparValueStateInputs();
            this._definirValorPadraoRadioButton();
            this._definirReservaPeloId(this._obterIdPeloParametro(evento));
        },

        _modeloReserva(reserva) {
            return reserva
                ? this.modelo(MODELO_RESERVA, reserva)
                : this._definirModeloPadrao(MODELO_RESERVA);
        },

        _definirModeloPadrao(nomeModelo) {
            let reserva = {
                nome: STRING_VAZIA,
                cpf: STRING_VAZIA,
                telefone: STRING_VAZIA,
                idade: STRING_VAZIA,
                sexo: STRING_VAZIA,
                checkIn: new Date().toISOString(),
                checkOut: new Date().toISOString(),
                precoEstadia: STRING_VAZIA,
                pagamentoEfetuado: false
            };

            this.modelo(nomeModelo, reserva);
        },

        _obterIdPeloParametro(evento) {
            const parametroArguments = "arguments";
            return evento.getParameter(parametroArguments).id;
        },

        _definirValorPadraoRadioButton() {
            const idRadioButtonPagamentoNaoEfetuado = "radioButtonPagamentoNaoEfetuado";
            this.byId(idRadioButtonPagamentoNaoEfetuado).setSelected(true);
        },

        _definirValueStateInputValidado(input, valueStateText) {
            const valueStateError = "Error";
            const valueStateSuccess = "Success";

            valueStateText
                ? input.setValueState(valueStateError).setValueStateText(valueStateText)
                : input.setValueState(valueStateSuccess);
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

        _obterInputsSemAlteracao() {
            let inputsSemAlteracao = []

            for (let idInput of ARRAY_ID_INPUTS) {
                let valueStateInput = this.byId(idInput).getValueState();

                if (valueStateInput == ValueState.None) {
                    inputsSemAlteracao.push(idInput);
                }
            }

            return inputsSemAlteracao;
        },

        _obterReservaPreenchida() {
            let reserva = this.modelo(MODELO_RESERVA);

            reserva.idade = Number(reserva.idade);
            reserva.sexo = Number(reserva.sexo);
            reserva.precoEstadia = Number(Formatter.desformataPrecoEstadia(reserva.precoEstadia));

            return reserva;
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

        _criarReserva(reservaParaCriar, navegarTelaReservaCriada) {
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
                            navegarTelaReservaCriada(ROTA_DETALHES, reservaCriada.id)
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

        aoClicarNavegarParaTelaAnterior() {
            try {
                let idReserva = this.modelo(MODELO_RESERVA).id;

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
                let reservaPreenchida = this._obterReservaPreenchida();

                let inputsSemAlteracao = this._obterInputsSemAlteracao();
                let listaErrosValidacao = Validacao.obterListaErros();

                if (inputsSemAlteracao.length) {
                    Validacao.validarReserva(reservaPreenchida);
                    listaErrosValidacao = Validacao.obterListaErros();
                    this._definirValueStateInputsSemAlteracao(listaErrosValidacao);
                }

                let mensagensErroValidacao = Formatter.formataListaErros(listaErrosValidacao);

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
                let controllerCadastro = this;

                MessageBox.confirm(mensagemConfirmacao, {
                    actions: [MessageBox.Action.YES, MessageBox.Action.NO],
                    emphasizedAction: MessageBox.Action.YES,
                    onClose: function (acao) {
                        if (acao == MessageBox.Action.YES) {
                            controllerCadastro.navegarPara(ROTA_LISTAGEM);
                        }
                    }
                });
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

                const regexValoresNaoPermitidos = /[^0-9\.,]+/g;
                let valorPrecoEstadia = evento
                    .getParameter(PARAMETRO_VALUE)
                    .replace(regexValoresNaoPermitidos, STRING_VAZIA);

                valorPrecoEstadia = Formatter.desformataPrecoEstadia(valorPrecoEstadia);
                let mensagemErroValidacao = Validacao.validarPrecoEstadia(valorPrecoEstadia);

                this._definirValueStateInputValidado(inputPrecoEstadia, mensagemErroValidacao);
                inputPrecoEstadia.setValue(Formatter.formataPrecoEstadia(valorPrecoEstadia));
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})