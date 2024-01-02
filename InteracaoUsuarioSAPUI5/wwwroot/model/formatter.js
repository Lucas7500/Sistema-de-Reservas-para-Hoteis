sap.ui.define([], () => {
    "use strict";

    const PARAMETRO_MODEL = "i18n";

    return {
        formataSexo(sexo) {
            const textoSexoMasculino = "sexo0";
            const textoSexoFeminino = "sexo1";

            const resourceBundle = this.getOwnerComponent()
                .getModel(PARAMETRO_MODEL)
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
                .getModel(PARAMETRO_MODEL)
                .getResourceBundle();

            return resourceBundle.getText(pagamentoEfetuado ? textoPagamentoFoiEfetuado : textoPagamentoNaoFoiEfetuado);
        }
    }
});