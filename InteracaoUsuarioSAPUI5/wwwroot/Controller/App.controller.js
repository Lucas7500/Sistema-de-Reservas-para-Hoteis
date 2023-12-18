sap.ui.define([
   "sap/ui/core/mvc/Controller",
   "sap/m/MessageToast"
], (Controller, MesssageToast) => {
   "use strict";

   return Controller.extend("reservas.hoteis.controller.App", {
      aoClicarAdicionarReserva() {
         MesssageToast.show("Botão tá funfando ainda");
      }
   });
});