var baseurl = document.baseURI;
$(function () {

});

/* Only Digit Allowed */
function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

$(".numberonly").on("input", function (evt) {
    var self = $(this);
    self.val(self.val().replace(/[^0-9.]/g, ''));
    if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
        evt.preventDefault();
    }
});

function BindYear(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    $('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Home/GetDDLYear",
        type: "Post",
        data: '',
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                if (SelectAll) {
                    $('#' + ElementId).append($("<option>").val('0').text('All'));
                }
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + ElementId).trigger("chosen:updated");
}

function BindFinYear(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Home/GetDDLFinYear",
        type: "Post",
        data: '',
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = resp.res;
                if (SelectAll) {
                    $('#' + ElementId).append($("<option>").val('0').text('All'));
                }
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + ElementId).trigger("chosen:updated");
}

function BindDistrict(ElementId, SelectedValue, SelectAll) {
        $('#' + ElementId).empty();
        $('#' + ElementId).prop("disabled", false);
        //$('#' + ElementId).append($("<option>").val('').text('Select'));
        $.ajax({
            //url: document.baseURI + "/Master/GetHSCDistrict",
            url: document.baseURI + "Master/GetDistrictList",
            type: "Post",
            data: JSON.stringify({ 'SelectAll': SelectAll }),
            contentType: "application/json; charset=utf-8",
            //global: false,
            //async: false,
            dataType: "json",
            success: function (resp) {
                if (resp.IsSuccess) {
                    var data = resp.res;
                    $.each(data, function (i, exp) {
                        $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                    });
                    $('#' + ElementId).val(SelectedValue);
                }
                else {
                    //alert(resp.IsSuccess);
                }
            },
            error: function (req, error) {
                if (error === 'error') { error = req.statusText; }
                var errormsg = 'There was a communication error: ' + error;
                //Do To Message display
            }
        });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
//function GetBlocks(ElementId, EleSelectVal, SelectedValue, SelectAll) {
    
//    $('#' + ElementId).empty();
//    $('#' + ElementId).prop("disabled", false);
//    //$('#' + ElementId).append($("<option>").val('').text('Select'));
//    $.ajax({
//        //url: document.baseURI + "/Master/GetHSCDistrict",
//        url: document.baseURI + "Home/GetDDLBlocks",
//        type: "Post",
//        data: JSON.stringify({ 'DistrictId': SelectedValue }),
//        contentType: "application/json; charset=utf-8",
//        //global: false,
//        //async: false,
//        dataType: "json",
//        success: function (resp) {
//            if (resp.IsSuccess) {
//                var data = resp.res;
//                if (SelectAll) {
//                    $('#' + ElementId).append($("<option>").val('0').text('All'));
//                }
//                $.each(data, function (i, exp) {
//                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
//                });
//                $('#' + ElementId).val(EleSelectVal);
//            }
//            else {
//                //alert(resp.IsSuccess);
//            }
//        },
//        error: function (req, error) {
//            if (error === 'error') { error = req.statusText; }
//            var errormsg = 'There was a communication error: ' + error;
//            //Do To Message display
//        }
//    });

//    //console.log('select value-'+SelectedValue);
//    $('#' + ElementId).trigger("chosen:updated");
//}

function parseToNumber(str) {
    var val = parseFloat(str);
    return val ? val : 0;
}