$(document).ready(function () {

    // Botuni
    var clearTimeButtons = $(".clear-time")
    var joinRoom = $(".btn-joinRoom");
    var filterButton = $("#filterbutton");

    // Selectori
    var kvart_selector = $("#kvart_selector");
    var teren_selector = $("#teren_selector");
    var sport_selector = $("#Sportovi")

    // Datetimepickeri
    var selectorFrom = $("#input_from")
    var selectorTo = $("#input_to")
   
    // Soba table container
    var tableContainer = $("#soba_table_container")

    /*
    // Button callbacks
    */

    // Clear time button callback
    clearTimeButtons.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        // Clear time picker
        $(this).parent().closest(".datetimepicker_filter").data("DateTimePicker").clear();
    });

    joinRoom.click(function (e) {
        e.stopImmediatePropagation(); 
        var href = $(this).attr('href');
        console.log($(this))
        console.log(href);
       // window.location.href = href; 
    });

    //filterButton.click(function (e) {
    //    e.preventDefault();
    //    filterRooms();
    //})
    /*
       // Selector callbacks
   */
    kvart_selector.change(function () {
        var optionSelected = $(this).find("option:selected");
        var valueSelected = optionSelected.val();
        var textSelected = optionSelected.text();

        // Ocisti teren selector
        teren_selector.empty();
        teren_selector.append($('<option value="0">Svi tereni</option>'))

        // Extract terena iz odabranog kvarta
        var tereni = optionSelected.data("tereni");
        for (var index in tereni) {
            var teren = tereni[index];
            var optionTeren = $('<option value="' + teren.Id + '">' + teren.Naziv + '</option>');
            // Append option to teren selector
            teren_selector.append(optionTeren);
        }

        // Trigger room filtering
        filterRooms();
    });

    sport_selector.change(function () {
        // Trigger room filtering
        filterRooms();
    });

    teren_selector.change(function () {
        // Trigger room filtering
        filterRooms();
    });

    // Datetimepicker init
    $('.datetimepicker_filter').datetimepicker({
        locale: "hr"
    }).on("dp.change", function () {
        filterRooms();
    });

    // Filter toggles
    $(".filtergumb").click(function () {
        $(this).next(".filter").slideToggle("slow");
    });

    /**
     * Functions
     */
    function filterRooms() {
        var timeFrom = selectorFrom.val();
        var timeTo = selectorTo.val();
        var sportId = sport_selector.val();
        var terenId = teren_selector.val();
        var kvartId = kvart_selector.val();

        sobaFilter = {
            TimeFrom: timeFrom.trim() == "" ? null : timeFrom,
            TimeTo: timeTo.trim() == "" ? null : timeTo,
            SportId: sportId == 0 ? null : sportId,
            TerenId: terenId == 0 ? null : terenId,
            KvartId: kvartId == 0 ? null : kvartId          
        }

        // Make ajax request for filter
        $.ajax({
            url: "/Sobas/FilterSobe",
            type: "POST",
            data: sobaFilter,
            success: function (data) {
                tableContainer.html(data);
               
            },
            error: function () {

            }
        });
    }
});
