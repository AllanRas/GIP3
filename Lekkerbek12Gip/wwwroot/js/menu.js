$(document).ready(function () {

    var categories = $("#categories");
    var first = $("#categories").children("option:selected").val();
    var firstSelectedCategory = $("#" + first);
    console.log(firstSelectedCategory);
    firstSelectedCategory.css("display", "block")

    $("#categories").change(function () {
        var selected = $(this).children("option:selected").val();

        var menu = $("#menuBody").children();
        menu.css("display", "none");

        var selectedCategory = $("#" + selected);
        selectedCategory.css("display", "block");
    });
});