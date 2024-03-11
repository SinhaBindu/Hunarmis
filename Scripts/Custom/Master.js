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

function BindYearList(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetYearList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                 var data = JSON.parse(resp.res);
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

function BindMonthList(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetMonthList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                 var data = JSON.parse(resp.res);
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

function BindStateList(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetStateList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                 var data = JSON.parse(resp.res);
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
function BindDistrict(ElementId, SelectedValue, SelectAll,Para) {
        $('#' + ElementId).empty();
        $('#' + ElementId).prop("disabled", false);
        //$('#' + ElementId).append($("<option>").val('').text('Select'));
        $.ajax({
            //url: document.baseURI + "/Master/GetHSCDistrict",
            url: document.baseURI + "Master/GetDistrictList",
            type: "Post",
            data: JSON.stringify({ 'SelectAll': SelectAll, 'StateId': Para }),
            contentType: "application/json; charset=utf-8",
            //global: false,
            //async: false,
            dataType: "json",
            success: function (resp) {
                if (resp.IsSuccess) {
                     var data = JSON.parse(resp.res);
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
function BindBatchList(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetBatchList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                 var data = JSON.parse(resp.res);
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
function BindTrainingAgency(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetTrainingAgencyList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
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
function BindTrainingCenter(ElementId, SelectedValue, SelectAll, Para1,Para2) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetTrainingCenterList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'DistrictId': Para1, 'TrainingAgencyId': Para2 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        //async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                 var data = JSON.parse(resp.res);
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

function parseToNumber(str) {
    var val = parseFloat(str);
    return val ? val : 0;
}