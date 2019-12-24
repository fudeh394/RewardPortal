var myTilesInfo = (function () {

    var _divId;

    function run() {

        AppInfo

        
        myTiles.set(result.loginInfo, _divId);
        $('#loadingImageBase').hide();

        //$.ajax({
        //    type: "GET",
        //    url: 'api/Admin/GetAppInfo',
        //    async: true,
        //    success: function (result) {
        //        debugger;
        //        myTiles.set(result.loginInfo, _divId);
        //        $('#loadingImageBase').hide();

        //    },
        //    error: function (e) {
        //        debugger;
                
        //        console.log(JSON.parse(JSON.stringify(e)))
        //    }

        //});

    }


    return {
        set: function (domID) {

            _divId = domID;
            run();

        }
    }


})();

var myTiles = (function () {
    var thata;
    var divID;

    function run() {

        divID = "#" + divID;
        var e = "";
        var f = "";
        var d = document.getElementById("f");//(divID);
        if (thata.length > 0) {
            for (a in thata) {
                e = e + buildATileGroupWithBiggerSpan(thata[a])
            }
        }

        var g = "<div class=\"span12\" style=\"margin-left: 0px;\">" + f + "</div>";
        e = e + g;
        $(divID).html(e);

    }



    //for a department
    function buildATileGroupWithBiggerSpan(datta) {

        var ress = "<div class=\"span12\" style=\"margin-left: 0px;\">";
        ress = ress + "<span class=\"tile_title\">" + datta.name + "</span><div class=\"flex-container\">";
        for (i in datta.apps) {
            var eyD = parseInt(i) + 1;
            ress = ress + buildAFinalTile(datta.apps[i].tilecolor, datta.apps[i].appUrl, datta.apps[i].tileIcon, datta.apps[i].tileType, datta.apps[i].description, datta.apps[i].appName, eyD);
        }
        ress = ress + " </div><br /></div>";
        return ress;
    }
    //for a department
    function buildATileGroup(datta) {

        var ress = "<div class=\"span4\" style=\"margin-left: 0px;width:32.623931623931625% ;margin-right: 10px\">";
        ress = ress + "<span class=\"tile_title\">" + datta.name + "</span><div class=\"flex-container\">";
        for (i in datta.apps) {
            var eyD = parseInt(i) + 1;
            ress = ress + buildAFinalTile(datta.apps[i].tilecolor, datta.apps[i].appUrl, datta.apps[i].tileIcon, datta.apps[i].tileType, datta.apps[i].description, datta.apps[i].appName, eyD);
        }
        ress = ress + " </div></div>";
        return ress;
    }



    function buildAFinalTile(tilecolor, tileurl, tileicon, tiletype, description, title, id) {

        tilecolor = "tile bg-" + tilecolor;
        var res = "<div class=\"" + tilecolor + "\" onclick=\"window.open('" + tileurl + "','_self',false);\">";
        res = res + "<div class=\"tile-body\" style=\"cursor: pointer;\"><i class=\"" + tileicon + "\"></i> </div>";
        res = res + " <div class=\"tile-object\"><div class=\"name\">" + title + "</div><div class=\"number\">" + "" + " </div> </div></div>";
        return res;



    }


    return {
        set: function (value, domID) {

            thata = value.departments;
            divID = domID;
            run();
        }
    }


})();

var mySideMenuInfo = (function () {

    function run() {
        $.ajax({
            type: "POST",
            url: './api/Admin/GetAppInfo',
            success: function (result) {
                $("#___username").html(localStorage.getItem('BiisUsername'));
                mySideMenu.set(result.loginInfo, "_menus");
            },
            error: function (e) {
                //window.location.replace('./index.html');
            }
        });
    }


    return {
        set: function () {
            return run();
        }
    }


})();


var mySideMenu = (function () {

    var Datta;
    var domID;

    function ExecuteAction() {

        divID = "#" + domID;
        var e = "<li style=\"padding-top: 2px;\"> <!-- BEGIN SIDEBAR TOGGLER BUTTON --><div class=\"sidebar-toggler hidden-phone\"></div><!-- BEGIN SIDEBAR TOGGLER BUTTON --> </li> ";
        e = e + "<li><!-- BEGIN RESPONSIVE QUICK SEARCH FORM --><form class=\"sidebar-search\"> <div class=\"input-box\"><a href=\"javascript:;\" class=\"remove\"></a><input type=\"text\" placeholder=\"Search...\" /><input type=\"button\" class=\"submit\" value=\" \" /></div></form><!-- END RESPONSIVE QUICK SEARCH FORM --> </li>";
        e = e + "<li class=\"start active \"><a href=\"base.html\"><i class=\"icon-home\"></i><span class=\"title\">Dashboard</span><span class=\"selected\"></span></a></li>";
        if (Datta.length > 0) {
            for (a in Datta) {
                e = e + buildSideMenu(Datta[a], Datta[a].id)
            }
        }

        $(divID).html(e);

    }

    function buildSideMenu(datta, _ids) {




        //if (_ids == 0)
        //    var ress = " <li id=\"_1\" class=\"start active \"><a href=\"javascript:;\"><i class=\"" + datta.apps[0].tileIcon + "\"></i><span class=\"title\">" + datta.name + "</span><span class=\"arrow \"></span></a><ul class=\"sub-menu\">";
        //else
        {
            a = "_" + _ids;
            var ress = " <li id=\"" + a + "\" class=\"\"><a href=\"javascript:;\"><i class=\"" + datta.apps[0].tileIcon + "\"></i><span class=\"title\">" + datta.name + "</span><span class=\"arrow \"></span></a><ul class=\"sub-menu\">";

        }

        for (i in datta.apps) {
            var eyD = parseInt(i) + 1;
            ress = ress + buildInnerSideMenu(datta.apps[i].appUrl, datta.apps[i].tileIcon, datta.apps[i].appName, eyD);
        }
        ress = ress + "</ul></li>";
        return ress;


    }

    function buildInnerSideMenu(url, icon, name, id) {



        var r = "<li><a style=\" font-size:inherit !important;\" href=\"" + url + "\"><i style=\" padding-right:3px;\" class=\"" + icon + "\"></i>" + name + "</a></li>";
        return r;
    }

    return {
        set: function (vale, dID) {
            Datta = vale.departments;
            domID = dID;
            ExecuteAction();
        }
    }
})();




