$(document).ready(function () {
    var geoSirina = $("#geo-sirina").val().trim();
    var geoDuzina = $("#geo-duzina").val().trim();

    geoSirina = geoSirina == "" ? null : geoSirina;
    geoDuzina = geoDuzina == "" ? null : geoDuzina;

    var googleMaps = new GoogleMaps($("#geo-sirina"), $("#geo-duzina"), geoSirina, geoDuzina);

    googleMaps.initMap();

    $.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }

    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^[0-9.,]+$/.test(value);
    }
});