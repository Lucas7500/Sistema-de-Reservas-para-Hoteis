sap.ui.define([
    "sap/m/MessageBox"
], (MessageBox) => {
    "use strict";

    return {
        processarEvento(acao) {
            try {
                const tipoDaPromise = "catch", tipoBuscado = "function";
                let promise = acao();

                if (promise && typeof (promise[tipoDaPromise]) == tipoBuscado) {
                    promise.catch(erro => MessageBox.warning(erro.message));
                }
            } catch (erro) {
                MessageBox.warning(erro.message);
            }
        }
    }
})