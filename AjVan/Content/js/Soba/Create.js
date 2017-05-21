$(document).ready(function () {

    // Selectori
    var kvart_selector = $("#kvart_selector");
    var teren_selector = $("#teren_selector");

    /*
      // Selector callbacks
    */
    kvart_selector.change(function () {
        var optionSelected = $(this).find("option:selected");
        var valueSelected = optionSelected.val();
        var textSelected = optionSelected.text();

        // Ocisti teren selector
        teren_selector.empty();

        // Extract terena iz odabranog kvarta
        var tereni = $(optionSelected.data("tereni"));
        for (var index in tereni.toArray()) {
            var teren = tereni[index];
            var optionTeren = $('<option value="' + teren.Id + '">' + teren.Naziv + '</option>');
            // Append option to teren selector
            teren_selector.append(optionTeren);
        }



    });

    $(".datetimepicker_select").datetimepicker({
        locale: "hr",
        //format: "DD.MM.YYYY HH:mm",
        defaultDate: moment()
    });
  $('#date_pocetak').rules('remove');;

    $(".timepicker_select").datetimepicker({
        locale: "hr",
        format: "HH:mm",
        defaultDate: moment().startOf("day")
    });


});