/// <reference path="jquery-1.5.1.min.js" />

function changeImportance(systemName, projectId) {
    var divName = "#forImportance" + systemName;
    $(divName).load("/Project/ChangeImportance", { projectId: projectId, systemName: systemName });
}



function my_function_v1(urlToPost) {
    $.ajax({
        url: urlToPost,
        success: function (partial) {
            $(".warkingButton").css('display', 'none');
            $("#TestContainer").html(partial);
        },
        error: function (answer, other, oneMore) {
            $("#errorMsg").css('display', 'visible');
            $("#errorMsg").html(other);
        }
    });
}


function modifyValue(urlForAction, id) {
    var newValueContainer = "#newValue_"+id;
    var newValue = $(newValueContainer).attr("value");
    $.ajax({
        url: urlForAction,
        data: {Value:newValue},
        type: "post",
        success: function (partial) {
            var divName = "#property_" + id;
            $(divName).html(partial);
        },
        error: function (answer, message) {
            var errBox = "#error_" + id;
            $(errBox).css('display', 'visible');
            $(errBox).html(message);
        }
    });
}

function backToValue(urlForAction, id) {
    $.ajax({
        url: urlForAction,
        type: "get",
        success: function (partial) {
            var divName = "#property_" + id;
            $(divName).html(partial);
        },
        error: function (answer, message) {
            var errBox = "#error_" + id;
            $(errBox).css('display', 'visible');
            $(errBox).html(message);
        }
    });
}



function xValue(urlForAction) {
    var name = $("#centerDiv #Name").val();
    var sysname = $("#centerDiv #SystemName").val();
    var type = $("select option:selected").text();
    var arrayOfValues;
    if (type == "Select" || type == "Multyselect") {
        arrayOfValues = $("#AvailableValues").text();
    }
    else {
        arrayOfValues = "";
    }
    $.ajax({
        url: urlForAction,
        data: { Name: name, SystemName: sysname, Type: type, AvailableValues: arrayOfValues },
        type: "post",
        success: function () {
            $("#result").dialog("close");
            window.location.reload();
        },
        error: function (answer, message) {
            $(".errordata").css("background","red");
            $(".errordata").html(message);
        }
    });
}


$(document).ready(function () {
    $("#dialog").dialog(
        {
            resizable: false,
            position: ['center', 'top']
        }
    );
});