$(document).ready(function () {


    $("#button").click(function (e) {
        
        var slotNumberChef1 = $("#slotNumberChef1").text();

        var slotNumberChef2 = $("#slotNumberChef2").text();

        var selectedChef = $("#chefList").val();

        if (slotNumberChef1 == "0" && selectedChef == 1) {
            e.preventDefault();
            alert("no slot left for chef John")
        }

        if (slotNumberChef2 == "0" && selectedChef == 2) {
            e.preventDefault();
            alert("no slot left for chef Micheal")
        }
    });

});