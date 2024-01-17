sap.ui.define([
    "./BaseController",
    "../model/Formatter",
    "../Repositorios/ReservaRepository",
    "sap/m/MessageBox",
    "../Services/Validacao"
], (BaseController, Formatter, ReservaRepository, MessageBox, Validacao) => {
    "use strict";

    const CAMINHO_ROTA_CADASTRO = "reservas.hoteis.controller.Cadastro";
    const ROTA_LISTAGEM = "listagem";
    const MODELO_RESERVA = "reserva";
    const PARAMETRO_VALUE = "value";
    const VALOR_INICIAL_VALUE_STATE_INPUT = "None";
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
            const recursosi18n = this.obterRecursosI18n();
            const rotaCadastro = "cadastro";
            const rotaEdicao = "edicao";
            Validacao.definirRecursosi18n(recursosi18n);

            this.vincularRota(rotaCadastro, this._aoCoincidirRotaCadastro);
            this.vincularRota(rotaEdicao, this._aoCoincidirRotaEdicao);
        },

        _aoCoincidirRotaCadastro() {
            this._limparValueStateInputs();
            this._definirValorPadraoRadioButton();
            this._definirModeloReserva();
        },

        _aoCoincidirRotaEdicao(evento) {
            this._limparValueStateInputs();
            this._definirValorPadraoRadioButton();
            this._definirReservaPeloId(this._obterIdPeloParametro(evento));
        },

        _definirModeloReserva(reserva) {
            const nomeModelo = "reserva";

            return reserva
                ? this.modelo(nomeModelo, reserva)
                : this._definirModeloPadrao(nomeModelo);
        },

        _definirModeloPadrao(nomeModelo) {
            let reserva = {
                nome: STRING_VAZIA,
                cpf: STRING_VAZIA,
                telefone: STRING_VAZIA,
                idade: STRING_VAZIA,
                sexo: STRING_VAZIA,
                checkIn: Date(),
                checkOut: Date(),
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

                if (valueStateInput == VALOR_INICIAL_VALUE_STATE_INPUT) {
                    this._definirValueStateInputValidado(input, listaErrosValidacao[i]);
                }
            }
        },

        _limparValueStateInputs() {
            for (let idInput of ARRAY_ID_INPUTS) {
                let input = this.byId(idInput);
                input.setValueState(VALOR_INICIAL_VALUE_STATE_INPUT);
            }
        },

        _obterInputsSemAlteracao() {
            let inputsSemAlteracao = []

            for (let idInput of ARRAY_ID_INPUTS) {
                let valueStateInput = this.byId(idInput).getValueState();

                if (valueStateInput == VALOR_INICIAL_VALUE_STATE_INPUT) {
                    inputsSemAlteracao.push(idInput);
                }
            }

            return inputsSemAlteracao;
        },

        _obterReservaPreenchida() {
            let reserva = this.modelo(MODELO_RESERVA);

            reserva.idade = Number(reserva.idade);
            reserva.sexo = Number(reserva.sexo);

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

        _criarReserva(reservaParaCriar) {
            const recursosi18n = this.obterRecursosI18n();
            const variavelSucessoSalvar = "sucessoSalvar";
            const mensagemSucessoSalvar = recursosi18n.getText(variavelSucessoSalvar);

            ReservaRepository.criarReserva(reservaParaCriar)
                .then(async response => {
                    const statusCreated = 201;
                    if (response.status == statusCreated) {
                        let reservaCriada = await response.json();
                        MessageBox.success(mensagemSucessoSalvar, {
                            onClose: () => {
                                controllerCadastro._abrirDetalhesReserva(reservaCriada.id);
                            }
                        });
                    }
                    else {
                        return Promise.reject(response);
                    }
                })
                .catch(async erro => {
                    let mensagemErro = await erro.text();

                    MessageBox.warning(mensagemErro);
                });
        },

        _atualizarReserva(reservaParaAtualizar) {
            const recursosi18n = this.obterRecursosI18n();
            const variavelSucessoEditar = "sucessoEditar";
            const mensagemSucessoEditar = recursosi18n.getText(variavelSucessoEditar);
            const controllerCadastro = this;

            ReservaRepository.atualizarReserva(reservaParaAtualizar)
                .then(response => {
                    const statusNoContent = 204;
                    if (response.status == statusNoContent) {
                        MessageBox.success(mensagemSucessoEditar, {
                            onClose: () => {
                                controllerCadastro._abrirDetalhesReserva(reservaParaAtualizar.id);
                            }
                        })
                    }
                    else {
                        return Promise.reject(response);
                    }
                })
                .catch(async erro => {
                    let mensagemErro = await erro.text();

                    MessageBox.warning(mensagemErro);
                });
        },

        _abrirDetalhesReserva(id) {
            try {
                const rotaDetalhes = "detalhes";
                this.navegarPara(rotaDetalhes, id);
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        },

        aoClicarNavegarParaTelaAnterior(evento) {
            try {
                //pegar o id por algum metodo do sap ui 5 
                //senao pega pelo modelo
                let reserva 

                idReserva
                    ? this._abrirDetalhesReserva(idReserva)
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
                        ? this._atualizarReserva(reservaPreenchida)
                        : this._criarReserva(reservaPreenchida);
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

        aoDigitarValidarIdade(evento) {
            const regexNumeros = "[0-9]";
            let valorInput = evento.getSource().getValue();

            for (let char of valorInput) {
                if (!char.match(regexNumeros)) {
                    let inputValido = valorInput.replace(char, STRING_VAZIA);
                    evento.getSource().setValue(inputValido);
                }
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
                let reserva = this.modelo(MODELO_RESERVA);
                let edicao = Boolean(reserva.id);

                let inputCheckIn = this.byId(ID_INPUT_CHECK_IN);
                let inputCheckOut = this.byId(ID_INPUT_CHECK_OUT);

                let valorCheckIn = inputCheckIn.getValue();
                let valorCheckOut = inputCheckOut.getValue();

                let mensagemErroValidacaoCheckIn = Validacao.validarCheckIn(valorCheckIn, edicao);
                let mensagemErroValidacaoCheckOut = Validacao.validarCheckOut(valorCheckOut, valorCheckIn, edicao);

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
                let valorPrecoEstadia = inputPrecoEstadia.getValue();

                const regexNumerosPontoVirgula = "[0-9\.,]";
                for (let char of valorPrecoEstadia) {
                    if (!char.match(regexNumerosPontoVirgula)) {
                        valorPrecoEstadia = valorPrecoEstadia.replace(char, STRING_VAZIA);
                    }
                }

                const charPonto = ".";
                const regexPontos = /\./g;
                const regexVirgulas = /,/g;
                valorPrecoEstadia = valorPrecoEstadia
                    .replace(regexPontos, STRING_VAZIA)
                    .replace(regexVirgulas, charPonto);

                let mensagemErroValidacao = Validacao.validarPrecoEstadia(valorPrecoEstadia);

                this._definirValueStateInputValidado(inputPrecoEstadia, mensagemErroValidacao);
                inputPrecoEstadia.setValue(Formatter.formataPrecoEstadia(valorPrecoEstadia));
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    })
})