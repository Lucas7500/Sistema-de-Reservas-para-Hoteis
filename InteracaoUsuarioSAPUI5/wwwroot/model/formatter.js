sap.ui.define([
    "sap/ui/core/format/DateFormat",
    "sap/ui/core/format/NumberFormat"
], (DateFormat, NumberFormat) => {
    "use strict";

    const MODEL_I18N = "i18n";

    return {
        formataSexo(sexo) {
            const textoSexoMasculino = "sexo0";
            const textoSexoFeminino = "sexo1";
            const resourceBundle = this.getOwnerComponent().getModel(MODEL_I18N).getResourceBundle();

            return resourceBundle.getText(sexo ? textoSexoFeminino : textoSexoMasculino)
        },

        formataPrecoEstadia(precoEstadia) {
            var formatador = NumberFormat.getCurrencyInstance({
                currencyCode: false
            });

            return formatador.format(parseFloat(precoEstadia));
        },

        desformataPrecoEstadia(precoEstadia) {
            const regexPontos = /\./g;
            const regexVirgulas = /,/;
            const charPonto = ".";
            const stringVazia = "";

            return String(precoEstadia)
                .replace(regexPontos, stringVazia)
                .replace(regexVirgulas, charPonto);
        },

        formataPagamentoEfetuado(pagamentoEfetuado) {
            const textoPagamentoFoiEfetuado = "pagamentoEfetuadoTrue";
            const textoPagamentoNaoFoiEfetuado = "pagamentoEfetuadoFalse";
            const recursosI18n = this.getOwnerComponent().getModel(MODEL_I18N).getResourceBundle();

            return recursosI18n.getText(pagamentoEfetuado ? textoPagamentoFoiEfetuado : textoPagamentoNaoFoiEfetuado);
        },

        formataData(data) {
            const formatoData = "yyyy-MM-dd";
            let formatador = DateFormat.getDateInstance({
                pattern: formatoData
            });

            return formatador.format(new Date(data));
        },

        formataListaErros(listaErros) {
            let separador = "\n";
            let mensagensErro = listaErros.
                filter(mensagemErro => mensagemErro != undefined)
                .join(separador);

            return mensagensErro;
        }
    }
});