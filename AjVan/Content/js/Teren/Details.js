$(document).ready(function () {
    var geoSirina = $("#geo-sirina").val().trim();
    var geoDuzina = $("#geo-duzina").val().trim();

    geoSirina = geoSirina == "" ? null : geoSirina;
    geoDuzina = geoDuzina == "" ? null : geoDuzina;

    var googleMaps = new GoogleMaps($("#geo-sirina"), $("#geo-duzina"), geoSirina, geoDuzina);

    googleMaps.initMap();
    googleMaps.disableInputs();
});