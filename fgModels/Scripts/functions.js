function getVirtDir() {
    var Path = location.host;
    var VirtualDirectory;
    if (Path.indexOf("localhost") >= 0 && Path.indexOf(":") >= 0) {
        VirtualDirectory = "";

    }
    else {
        var pathname = window.location.pathname;
        var VirtualDir = pathname.split('/');
        VirtualDirectory = VirtualDir[1];
        VirtualDirectory = '/' + VirtualDirectory;
    }
    return VirtualDirectory;
}
function blockV3(text) {
    $("<table id='overlay' style='margin-top:60px;'><tbody><tr><td>" +
        "<div class='cssload-square'>" +
        "<div><div><div><div><div></div></div></div></div></div>" +
        "<div><div><div><div><div></div></div></div></div></div>" +
        "</div >" +
        "<div class='cssload-square cssload-two'>" +
        "    <div><div><div><div><div></div></div></div></div></div>" +
        "    <div><div><div><div><div></div></div></div></div></div>" +
        "</div>" +
        "<br/><div id='txtOver'>" + text + "</div>" +
        "</td></tr></tbody></table>").css({
            "position": "fixed",
            "top": "0px",
            "left": "0px",
            "width": "100%",
            "height": "100%",
            "background-color": "rgba(0,0,0,.5)",
            "z-index": "10000",
            "vertical-align": "middle",
            "text-align": "center",
            "color": "#fff",
            "font-size": "40px",
            "font-weight": "bold",
            "cursor": "wait"
        }).appendTo("body");
}
function removeOverlay() {
    $("#overlay").remove();
}

function getLogin() {
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/Account/Login",
        success: function (data) {
            $("#login").html(data);
            return false;
        },
        error: function () { }
    });
}

function getFgView() {
    $.ajax({
        method: "GET",
        url: getVirtDir() + "/fgModels",
        success: function (data) {
            $("#fgView").html(data);
            var tableS = $('#tblModels').DataTable({
                "paging": true,
                "searching": true,
                "lengthChange": false,
                "pageLength": 10,
                "info": true,
                "autoWidth": true,
                "ordering": true,
/*                "order": ['FG Name', 'asc']*/
                //'language': { 'url': getVirtDir() + '/Scripts/Spanish.json' }
            });
            return false;
        },
        error: function () { }
    });
}