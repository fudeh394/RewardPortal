
function initSideMenuAndTiles() {
   
    myTilesInfo.set("#f");
}

$(document).ready(function () {

    initSideMenuAndTiles();

});

window.onload = function () {
    initSideMenuAndTiles();
}


function showLoadingImage() {
    try {
        $('#loadingImage').show();
    } catch (e) {
        alert(e.message);
    }
}
function hideLoadingImage() {
    $('#loadingImage').hide();
}