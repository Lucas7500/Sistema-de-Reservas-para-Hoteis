sap.ui.define([], () => {
    "use strict";

    return {
        formataSexo(sexo) {
            const oResourceBundle = this.getOwnerComponent().getModel("i18n").getResourceBundle();
            switch (sexo) {
                case 0:
                    return oResourceBundle.getText("sexo0");
                case 1:
                    return oResourceBundle.getText("sexo1");
                default:
                    return sexo;
            }
        },
        formataPagamentoEfetuado(pagamentoEfetuado) {
            const oResourceBundle = this.getOwnerComponent().getModel("i18n").getResourceBundle();
            switch (pagamentoEfetuado) {
                case true:
                    return oResourceBundle.getText("pagamentoEfetuadoTrue");
                case false:
                    return oResourceBundle.getText("pagamentoEfetuadoFalse");
                default:
                    return pagamentoEfetuado;
            }
        }
    };
});