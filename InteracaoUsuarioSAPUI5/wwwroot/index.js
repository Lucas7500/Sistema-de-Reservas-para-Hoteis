sap.ui.define([
    "sap/ui/core/mvc/XMLView"
], (XMLView) => {
    "use strict";

    XMLView.create({
        viewName: "reservas.hoteis.view.App"
    }).then((oView) => oView.placeAt("content"));
});