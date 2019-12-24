/* Formatting function for row details - modify as you need */
function format(d) {
    var htmlString = "";
    var htmlString1 = "";
    // `d` is the original data object for the row
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
    //return '<table cellpadding="5" id="example3" class="display table table-bordered table-responsive" width="100%" cellspacing="0" border="0" style="padding-left:50px;">' + htmlString + '</table>';
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

$(document).ready(function () {


    var fv = "";
    var ft;
    var fr;
    var qwery;
    var obj;
    var parentUri = "api/audit/selectAudit";
    var table = $('#tbll').DataTable({
        "ajax": parentUri,
        "columns": [
            {
                "className": 'details-control',
                "orderable": true,
                "data": null,
                "defaultContent": ''
            },
            { "data": "id" },
            { "data": "operation" },
            { "data": "description" },
            { "data": "machineID" },
            { "data": "date" },
            { "data": "user" }        
        ],
        "order": [[1, 'asc']]
    });


    $('#export_tbll').on('click', function () {

        $.ajax({
            url: "api/audit/selectAudit",
            type: 'GET',
            dataType: 'json',
            success: function (data1) {
                ExportToExcel('Audits', 'Audits', data1.data);
            }

        });
    });
    

    //Add event listener for opening and closing details
    $('#tbll tbody').on('click', 'td.details-control', function () {
  
    });

    hideLoadingImage();

});