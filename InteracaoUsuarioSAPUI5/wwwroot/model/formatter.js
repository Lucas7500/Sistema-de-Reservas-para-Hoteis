sap.ui.define([], () => {
    "use strict";

    const PARAMETRO_MODEL = "i18n";

    return {
        formataSexo(sexo) {
            const textoSexoMasculino = "sexo0";
            const textoSexoFeminino = "sexo1";

            const resourceBundle = this.getOwnerComponent().getModel(PARAMETRO_MODEL).getResourceBundle();

            let valorSexoMasculino = 0;
            let textoSexo = (sexo == valorSexoMasculino) ? textoSexoMasculino : textoSexoFeminino;

            return resourceBundle.getText(textoSexo);
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

            const resourceBundle = this.getOwnerComponent().getModel(PARAMETRO_MODEL).getResourceBundle();

            let valorPagamentoFoiEfetuado = true;
            let textoPagamentoEfetuado = (pagamentoEfetuado == valorPagamentoFoiEfetuado) ? textoPagamentoFoiEfetuado : textoPagamentoNaoFoiEfetuado;

            return resourceBundle.getText(textoPagamentoEfetuado);
        }
    }
});