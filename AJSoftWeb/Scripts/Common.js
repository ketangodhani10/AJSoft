
var JQ_pagination = 10;
var JQ_rowList = new Array(10, 25, 50, 100, 200, 1000, 100000000);

// set varibale for <a> target
var target = '_self';

//Append Random Parameter to URL to avoid caching
$(document).ajaxSend(function (event, request, settings) {
    var randomNum = Math.floor((Math.random() * 999) + 1); //returns random no between 1 to 999
    if (settings.url.indexOf('?') > 0)
        settings.url = settings.url + "&rand_ie=" + randomNum;
    else
        settings.url = settings.url + "?rand_ie=" + randomNum;
});

//Common Ajax Error
jQuery(document).ajaxError(function (event, jqXHR, settings, exception) {

    //if (jqXHR.status === 0) {
    //    AlertBox('Not connect.\n Verify Network.');
    //} else if (jqXHR.status == 404) {
    //    AlertBox('Requested page not found. [404]');
    //} else if (jqXHR.status == 500) {
    //    AlertBox('Internal Server Error [500].');
    //} else if (exception === 'parsererror') {
    //    AlertBox('Requested JSON parse failed.');
    //} else if (exception === 'timeout') {
    //    AlertBox('Time out error.');
    //} else if (exception === 'abort') {
    //    AlertBox('Ajax request aborted.');
    //} else {
    //    AlertBox('Uncaught Error.\n' + jqXHR.responseText);
    //}

    if (exception === 'timeout') {
        AlertBox('Time out error.');
    }
});

//**********JQ Grid**************
var JQ_DateFormat = 'm/d/Y';
var JQ_sopt_int = JSON.parse('{"sopt": ["eq","ne","le","lt","gt","ge"]}');
var JQ_sopt_Guid = JSON.parse('{"sopt": ["eq"]}');
var JQ_sopt_string = JSON.parse('{"sopt": ["cn","eq"]}');
var JQ_sopt_bool = JSON.parse('{"sopt": ["eq","ne"], "value": ":All;True:Yes;False:No"}');
var JQ_sopt_date = JSON.parse('{"sopt": ["eq","gt"]}');
var JQ_Paging_Opt = [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 50000];
var JQ_RowNum_Opt = 10;

function ReloadGrid(grid) {
    $("#" + grid).trigger('reloadGrid');
}

function ResizeJqgrid() {
    $(".jqgrid").each(function () {
        var gridId = $(this).children(":first").attr("id").replace("gbox_", "");
        $("#" + gridId).jqGrid('setGridWidth', $(this).width());
    });
}
//resize to fit page size
$(window).on('resize.jqgrid', function () {
    ResizeJqgrid();
    resizeChosen();
})
//**********JQ Grid**************

function resizeChosen() {
    $(".chosen-container").each(function () {
        $(this).attr('style', 'width: 100%');
    });
}
var JQry_DatePickerFormat = 'dd/mm/yy';

//jQuery(document).ready(function () {
//    $.datepicker.setDefaults({ dateFormat: 'mm/dd/yy' });
//});

function SuccessMessage(data) {
    $('body').showMessage({
        'thisMessage': [data],
        className: 'success',
        displayNavigation: false,
        location: 'top',
        autoClose: true,
        useEsc: true
    });
}

function ErrorMessage(data) {
    $('body').showMessage({
        'thisMessage': [data],
        className: 'fail',
        displayNavigation: false,
        location: 'top',
        useEsc: true,
        autoClose: true
    });
}

function InfoMessage(data) {
    $('body').showMessage({
        'thisMessage': [data],
        className: 'info',
        displayNavigation: false,
        location: 'top',
        useEsc: true,
        autoClose: true
    });
}



//Get URL Parameter
function GetURLParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

//convert to limited string
function Get_CutOff_String(str, counts) {
    if (str.length > counts)
        return str.substring(0, counts) + " ...";
    else
        return str;
}

//convert string date to Date
function DateFormatter(dateObject) {

    if (dateObject == "" || dateObject == null)
        return "";

    var d = new Date(dateObject);
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = month + "/" + day + "/" + year;

    return date;
};

function setDateFormatforCSharp(dateObject) {
    var d = new Date(dateObject.split("-").reverse().join("-"));
    var dd = d.getDate();
    var mm = d.getMonth() + 1;
    var yy = d.getFullYear();
    var newdate = yy + "-" + mm + "-" + dd;
    return newdate;
}

function SetIndianDateFormatter(dateObject) {

    if (dateObject == "" || dateObject == null)
        return "";

    var d = new Date(dateObject);
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = day + "-" + month + "-" + year;

    return date;
}

function set_cnt(div, cnt) {
    if (cnt == 0) {
        $(div).html(cnt); $(div).hide();
    }
    else {
        $(div).html(cnt); $(div).show();
    }
}

function GenerateHtmlTable(data) {
    var icon = '<i class="ace-icon fa fa-file-o blue bigger-120"></i>';
    var table = "<table border='1' class='table table-striped table-bordered table-hover'><thead><tr><th></th><th>Document Name</th><th>Document Type</th></tr></thead><tbody>";
    $.each(data, function (i, item) {

        if (item.ExtentionType == "doc" || item.ExtentionType == "docx")
            icon = '<i class="ace-icon fa fa-file-word-o blue bigger-120"></i>';
        else if (item.ExtentionType == "pdf")
            icon = '<i class="ace-icon fa fa-file-pdf-o blue bigger-120"></i>';
        else if (item.ExtentionType == "xls" || item.ExtentionType == "xlsx")
            icon = '<i class="ace-icon fa fa-file-excel-o blue bigger-120"></i>';
        else if (item.ExtentionType == "jpg" || item.ExtentionType == "jpeg" || item.ExtentionType == "png" || item.ExtentionType == "gif" || item.ExtentionType == "bmp")
            icon = '<i class="ace-icon fa fa-file-image-o blue bigger-120"></i>';
        else if (item.ExtentionType == "txt")
            icon = '<i class="ace-icon fa fa-file-text-o blue bigger-120"></i>';

        table = table + "<tr><td>" + icon + "</td><td>" + item.DocumentName + "</td><td>" + item.DocType + "</td></tr>";
    });
    table = table + "</tbody></table>";
    return table;
}

function isEqual(stringA, stringB) {
    return stringA.toLowerCase() == stringB.toLowerCase();
}

function RestrictEnteryKey(formName) {
    $('#' + formName).on("keyup keypress", function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            return false;
        }
    });
}

function ConvertUTCtoLocalTime() {
    
    $(".sCaseTimeStamp").each(function (index) {
        //var arg = $(this).text();
        //var inputFormat = new Date(arg + ' UTC');

        /*START CST TIME*/
        var arg = $(this).text();
        var inputFormat = new Date(arg.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3"));
        /*END CST TIME*/

        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        $(this).text([pad(d.getMonth() + 1), pad(d.getDate()), d.getFullYear()].join('/') + ' ' + pad(d.getHours()) + ':' + pad(d.getMinutes()));
    });
}

function ConvertUTCtoLocalDate() {
    $(".sCaseTransDate").each(function (index) {
        //var arg = $(this).text();
        //var inputFormat = new Date(arg + ' UTC');

        /*START CST TIME*/
        var arg = $(this).text();
        var inputFormat = new Date(arg.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3"));
        
        inputFormat = new Date(inputFormat.getTime() - ((1 * 60 * 60 * 1000) * 5));
        /*END CST TIME*/

        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        $(this).text([pad(d.getMonth() + 1), pad(d.getDate()), d.getFullYear()].join('/'));
    });
}

$(document).ready(function () {
    $.validator.setDefaults({
        onkeyup: false
    });


});