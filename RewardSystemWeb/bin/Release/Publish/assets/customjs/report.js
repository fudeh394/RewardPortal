/// <reference path="jquery-2.0.3.min.js" />


$(document).ready(function () {

    
   





    //Bxes
    //=================================
    (function getBoxItems() {


        var uri = "api/Report/GetGlobalBoxItems";
        $.ajax({
            type: "GET",
            url: uri,
            success: function (result) {
                tableBoxSuccess(result)
            },
            error: function (e) {
                console.log(e.statusCode);
                console.log(e.statusText);
                console.log(e.status);

            },
            complete: function () {
                setTimeout(getBoxItems, 20000);
            }
        })
    })();

    function tableBoxSuccess(items) {

        $("#boxes tbody").empty();
        // Iterate over the collection of data

        if ($("#boxes tbody").length == 0) {
            $("#boxes").append("<tbody></tbody>");



        }


        var ret1 = "<tr>" +
            "<td></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn blue tooltips no-tooltip-on-touch-device responsive\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\" data-desktop=\"tooltips\" data-original-title=\"?\">GROSS AMOUNT</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn blue tooltips no-tooltip-on-touch-device responsive\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\" data-desktop=\"tooltips\" data-original-title=\"?\">MERCHANT DISCOUNT</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn blue tooltips no-tooltip-on-touch-device responsive\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\" data-desktop=\"tooltips\" data-original-title=\"?\">MERCHANT RECEIVABLES</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn blue tooltips no-tooltip-on-touch-device responsive\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\" data-desktop=\"tooltips\" data-original-title=\"?\">TRANSACTION VOLUME</button></td>" +

            "</tr>";


        $("#boxes tbody").append(ret1);
        $("#boxes tbody").append(buildBoxTableRow_Naira(items.BoxObjectInNaira));
        $("#boxes tbody").append(buildBoxTableRow_Dollar(items.BoxObjectInDollar));

    }

    function buildBoxTableRow_Naira(rowObject) {
        var ret = "<tr>" +
            "<td><button id=\"confirm_opener\" class=\"btn blue tooltips no-tooltip-on-touch-device responsive\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\" data-desktop=\"tooltips\" data-original-title=\"?\">IN NAIRA</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.GrossAmount + "</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.MerchantDiscount + "</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.MerchantReceivable + "</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.VolumeTransaction + "</button></td>" +
            "</tr>";
        return ret;
    }

    function buildBoxTableRow_Dollar(rowObject) {
        var ret = "<tr>" +
            "<td><button id=\"confirm_opener\" class=\"btn blue tooltips no-tooltip-on-touch-device responsive\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\" data-desktop=\"tooltips\" data-original-title=\"?\">IN DOLLAR </button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.GrossAmount + "</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.MerchantDiscount + "</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.MerchantReceivable + "</button></td>" +
            "<td><button id=\"confirm_opener\" class=\"btn green\" style=\"width: 100% ;table-layout:fixed; border-collapse:collapse\">" + rowObject.VolumeTransaction + "</button></td>" +
            "</tr>";
        return ret;
    }
    //=========================================




   


    
   
 

});