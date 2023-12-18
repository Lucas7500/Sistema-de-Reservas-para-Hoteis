sap.ui.define([
    "sap/ui/core/ComponentContainer"
], (ComponentContainer) => {
    "use strict";
 
    new ComponentContainer({
        name: "reservas.hoteis",
        settings: {
            id: "hoteis"
        },
        async: true
    }).placeAt("content");
});