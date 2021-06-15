$(function () {
    $('#custtable').DataTable({
        "ajax": {
            "url": "/klants/LoadAllCustomersSS",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "klantId", "name": "klantId" },
            { "data": "name", "name": "name" },
            { "data": "adress", "name": "adress" },
            { "data": "getrouwheidsScore", "name": "getrouwheidsScore" },
            { "data": "geboortedatum", "name": "geboortedatum" },
            { "data": "emailadres", "name": "emailadres" },
            { "data": "bestellings", "name": "bestellings" },
            { "data": null, "defaultContent": "<a href='KlantId' class='del'>Delete</a>" }
        ],
        "columnDefs": [{
            targets: 4, render: function (data) {
                var format = "DD/ MM /YYYY";
                var date = moment(data);
                return date.format(format);
            }
        }],
        "serverSide": true,
        "order": [0, "asc"],
        "proccessing": true
    });
});


$('#custtable tbody').on('click', 'tr', function (e) {
    e.preventDefault();
    var id = $("td:first", this).text();

    var url = `Klants/Delete/${id}`
    var url2 = `Klants/Edit/${id}`

    if ($(e.target).is("a.del")) {

        $.get(url, { id: id }, function (data) {
            $("body").html(data);
        });
        return;
    }
    $.get(url2, { id: id }, function (data) {
        $("body").html(data);
    });
});
