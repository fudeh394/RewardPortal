/* Formatting function for row details - modify as you need */
function format(d) {
    var htmlString = "";
    var htmlString1 = "";
    htmlString = '<thead><th></th><th>ID</th><th>Operation</th><th>Details</th><th>Machine IP</th> <th>Date</th> <th>User</th></tr></thead>';
    htmlString1 = '<table cellpadding="5" id="child" class="display table table-bordered table-responsive" width="100%" cellspacing="0" border="0" style="padding-left:50px;">' + htmlString + '</table>';
    return htmlString1;
}

function formatChild(d) {

    var htmlString = '<thead><th>Booking</th></thead><tbody>';
    for (i in d) {
        htmlString += '<tr><td>' + d[i]["BOOKING"] + '</td></tr>';
    }
    htmlString += '</tbody>';
}

function ExportToExcel(filename, sheetName, obj) {
    var columnNames = [];
    var data = [];
    var ep = new ExcelPlus();
    var excelFile = ep.createFile(sheetName);
    for (i in obj[0]) {
        columnNames.push(i);
    }
    data.push(columnNames);
    for (i = 0; i < obj.length; i++) {
        var row = [];
        for (j in obj[i]) {
            row.push(obj[i][j]);
        }
        data.push(row);
    }
    excelFile.write({ "content": data }).saveAs(filename);
    console.log(data);
}

function isNullOrEmpty(txtBxName) {

    var res = true;
    var txt = $.trim($(txtBxName).val());
    if (txt == "")
        res = true;
    else
        res = false;
    return res;
}

$(document).ready(function () {



    $('#tbll').DataTable(
    {

        "processing": true,
        "serverSide": true,
        "ajax":
          {
            "url": "api/Report/SelectApprovedDudCheques",
              "type": "POST",
              "dataType": "JSON",
              "error": function (xhr, error, thrown) {
                  alert(thrown);
              },
          },
            "columns": [
                { "data": "MerchantAccountName" },
                { "data": "CustomerAccountNumber" },
                { "data": "TransactionDate" },
                { "data": "Amount" },
                { "data": "MerchantDiscount" },
                { "data": "MerchantReceivable" },
                { "data": "Currency" },
                { "data": "MerchantLocation" },
                { "data": "STAN" },
                { "data": "TerminalId" },
                { "data": "MerchantId" },
                { "data": "ProcessedBy" },  
        ]
    });











    $(function () {
        $("#export_tbll").on('click', function () {
            $("#tbll").table2excel({
                exclude: ".noExl",
                name: "Report",
                filename: "Report",
                fileext: ".xls",
                exclude_img: true,
                exclude_links: true,
                exclude_inputs: true
            });
        });
    })


    //Add event listener for opening and closing details
    $('#tbll tbody').on('click', 'td.details-control', function () {

    });


    $('#btn_Search').on('click', function () {

        var start = $('#txtStartDate').val();
        var end = $('#txtEndDate').val();
        var terminalId = $('#txtTerminalId').val();

        //do something here
        var uri = "api/Report/SetSearchCriteria?start=" + encodeURI(start) + "&end=" + encodeURI(end) + "&terminalId=" + encodeURI(terminalId);


        // Call Web API to update product
        $.ajax({
            url: uri,
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                window.location.reload()
            },
            error: function (request, message, error) {
            }
        });

    });

  

});