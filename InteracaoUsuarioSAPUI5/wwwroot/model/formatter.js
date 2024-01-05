sap.ui.define([
    "sap/ui/core/format/DateFormat"
], (DateFormat) => {
    "use strict";

    const MODEL_I18N = "i18n";

    return {
        formataSexo(sexo) {
            const textoSexoMasculino = "sexo0";
            const textoSexoFeminino = "sexo1";

            const resourceBundle = this.getOwnerComponent()
                .getModel(MODEL_I18N)
                .getResourceBundle();

            return resourceBundle.getText(sexo ? textoSexoFeminino : textoSexoMasculino)
        },

        formataPrecoEstadia(precoEstadia) {
            const duasCasasDecimais = 2;
            const charPonto = '.';
            const charVirgula = ',';
            
            let stringPrecoEstadia = `R$ ${Number(precoEstadia).toFixed(duasCasasDecimais)}`

            return stringPrecoEstadia.replace(charPonto, charVirgula);
        },

        formataPagamentoEfetuado(pagamentoEfetuado) {
            const textoPagamentoFoiEfetuado = "pagamentoEfetuadoTrue";
            const textoPagamentoNaoFoiEfetuado = "pagamentoEfetuadoFalse";

            const resourceBundle = this.getOwnerComponent()
                .getModel(MODEL_I18N)
                .getResourceBundle();

            return resourceBundle.getText(pagamentoEfetuado ? textoPagamentoFoiEfetuado : textoPagamentoNaoFoiEfetuado);
        },

        formataData(data) {
            const formatoData = "yyyy-MM-dd";

            let formatador = DateFormat.getDateInstance({
                pattern: formatoData
            });
            
            return formatador.format(data);
        }
    }
});