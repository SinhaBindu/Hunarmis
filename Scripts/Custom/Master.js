var baseurl = document.baseURI;
$(function () {
    jQuery('.numbersOnly').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });

    //$('.datepicker').datepicker({
    //    dateFormat: 'dd-M-yy',
    //    maxDate: '0',
    //    //maxDate: "+1M +10D",
    //    changeMonth: true,
    //    changeYear: true,
    //});

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

/* -------------------------------------------- Date Range -------------------------------*/
function GetCurrentMonth() {
    var d = new Date(),
        n = d.getMonth(),
        n = n + 1;
    console.log(n)
    return n;
}
function GetCurrentYear() {
    var d = new Date(),
        y = d.getFullYear();
    console.log(y)
    return y;
}
function toDate(dateString) {
    debugger;
    var parts = dateString.split('-');
    return new Date(parts[2] + "-" + parts[1] + "-" + parts[0]);
}
function getAge(dateString) {

    const dates = new Date(dateString); // {object Date}            

    const covdate = new Intl.DateTimeFormat("en-US", {
        year: "numeric",
        month: "2-digit",
        day: "2-digit"
    }).format(dates);
    dateString = covdate;

    var now = new Date();

    var yearNow = now.getFullYear();
    var monthNow = now.getMonth();
    var dateNow = now.getDate();
    //date must be mm/dd/yyyy
    var dob = new Date(dateString.substring(6, 10),
        dateString.substring(0, 2) - 1,
        dateString.substring(3, 5)
    );

    var yearDob = dob.getFullYear();
    var monthDob = dob.getMonth();
    var dateDob = dob.getDate();
    var age = {};
    var ageString = "";
    var yearString = "";
    var monthString = "";
    var dayString = "";


    yearAge = yearNow - yearDob;

    if (monthNow >= monthDob)
        var monthAge = monthNow - monthDob;
    else {
        yearAge--;
        var monthAge = 12 + monthNow - monthDob;
    }

    if (dateNow >= dateDob)
        var dateAge = dateNow - dateDob;
    else {
        monthAge--;
        var dateAge = 31 + dateNow - dateDob;

        if (monthAge < 0) {
            monthAge = 11;
            yearAge--;
        }
    }

    age = {
        years: yearAge,
        months: monthAge,
        days: dateAge
    };

    if (age.years > 1) yearString = " years";
    else yearString = " year";
    if (age.months > 1) monthString = " months";
    else monthString = " month";
    if (age.days > 1) dayString = " days";
    else dayString = " day";

    if ((age.years > 15) && (age.months > 0) && (age.days > 0))
        ageString = age.years + yearString + ", " + age.months + monthString + ", and " + age.days + dayString + " old.";
    else if ((age.years > 0) && (age.months > 0) && (age.days > 0))
        ageString = age.years + yearString + ", " + age.months + monthString + ", and " + age.days + dayString + " old.";
    else if ((age.years == 0) && (age.months == 0) && (age.days > 0))
        ageString = "Only " + age.days + dayString + " old!";
    else if ((age.years > 0) && (age.months == 0) && (age.days == 0))
        ageString = age.years + yearString + " old. Happy Birthday!!";
    else if ((age.years > 0) && (age.months > 0) && (age.days == 0))
        ageString = age.years + yearString + " and " + age.months + monthString + " old.";
    else if ((age.years == 0) && (age.months > 0) && (age.days > 0))
        ageString = age.months + monthString + " and " + age.days + dayString + " old.";
    else if ((age.years > 0) && (age.months == 0) && (age.days > 0))
        ageString = age.years + yearString + " and " + age.days + dayString + " old.";
    else if ((age.years == 0) && (age.months > 0) && (age.days == 0))
        ageString = age.months + monthString + " old.";
    // else ageString = "Oops! Could not calculate age!";
    else ageString = ""; //toastr.error("Error", "Can't be valid date.");

    return ageString;
}

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
                $('#' + ElementId + ' option[value="' + GetCurrentYear() + '"]').prop('selected', true);
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
                $('#' + ElementId + ' option[value="' + GetCurrentMonth() + '"]').prop('selected', true);
                //$('#' + ElementId).val(SelectedValue);
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