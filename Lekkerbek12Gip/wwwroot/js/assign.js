$(document).ready(function () {


    $("#button").click(function (e) {

        var slotNumberChef1 = $("#slotNumberChef1").text();

        var slotNumberChef2 = $("#slotNumberChef2").text();

        var selectedChef = $("#chefList").val();

        if (slotNumberChef1 == "0 Slot Left" && selectedChef == 1) {
            e.preventDefault();
            alert("no slot left for Chef1")
        }

        if (slotNumberChef2 == "0 Slot Left" && selectedChef == 2) {
            e.preventDefault();
            alert("no slot left for Chef2")
        }
    });

});