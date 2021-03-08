

const data = [];
const count = [];
$(function () {
    console.log($(bestellingId).text());
    $(".btn-success").click(function () {
        let val = parseInt($(this).siblings("input").val()) + 1;
        $(this).siblings("input").val(val)
    });

    $(".btn-danger").click(function () {
        let val = parseInt($(this).siblings("input").val()) - 1
        if (val <= 0) {
            $(this).siblings("input").val(0);
        }
        else {
            $(this).siblings("input").val(val)
        }
    })
    $("#dataTake").click(() => {
        var dok = document.getElementsByClassName("input");
        for (var i = 0; i < dok.length; i++) {
            let object = {
                gerechtId: dok.item(i).id,
                Aantal: parseInt(dok.item(i).value)
            }
            data.push(object)
        }

        $.ajax({
            method: "POST",
            url: "./GerechPOST",
            data: { "BestellingId": parseInt($(bestellingId).text()), "gerechts": data }                       
        })
    })
})

