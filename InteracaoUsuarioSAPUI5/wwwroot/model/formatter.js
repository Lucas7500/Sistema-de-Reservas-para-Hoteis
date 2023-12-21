sap.ui.define([], () => {
    "use strict";
    
    const PARAMETRO_MODEL = "i18n";
    
    return {
        formataSexo(sexo) {
            const SexoMasculino = "sexo0";
            const SexoFeminino = "sexo1";
            
            const resourceBundle = this.getOwnerComponent().getModel(PARAMETRO_MODEL).getResourceBundle();
            
            switch (sexo) {
                case 0:
                    return resourceBundle.getText(SexoMasculino);
                case 1:
                    return resourceBundle.getText(SexoFeminino);
                default:
                    return sexo;
            }
        },
        formataPrecoEstadia(precoEstadia) {
            const duasCasasDecimais = 2;
            const charPonto = '.';
            const charVirgula = ',';
            let stringPrecoEstadia = `R$ ${precoEstadia.toFixed(duasCasasDecimais)}`

            return stringPrecoEstadia.replace(charPonto, charVirgula);
        },
        formataPagamentoEfetuado(pagamentoEfetuado) {
            const pagamentoFoiEfetuado = "pagamentoEfetuadoTrue";
            const pagamentoNaoFoiEfetuado = "pagamentoEfetuadoFalse";

            const resourceBundle = this.getOwnerComponent().getModel(PARAMETRO_MODEL).getResourceBundle();
            
            switch (pagamentoEfetuado) {
                case true:
                    return resourceBundle.getText(pagamentoFoiEfetuado);
                case false:
                    return resourceBundle.getText(pagamentoNaoFoiEfetuado);
                default:
                    return pagamentoEfetuado;
            }
        }
    };
});