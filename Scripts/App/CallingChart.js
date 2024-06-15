$(document).ready(function () {
    LoadReport();

    //$('#em').on('input', function () {
    //    chart.container.style.fontSize = this.value + 'em';
    //    // Update layout based on new font and line sizes
    //    chart.isDirtyLegend = true;
    //    chart.redraw(false);
    //});
    $('#MonthId').change(function () {
        LoadReport();
    });
    $('#YearId').change(function () {
        LoadReport();
    })

});

let groupBy = (keys) => (array) =>
    array.reduce((objectsByKeyValue, obj) => {
        let value = keys.map((key) => obj[key]).join("-");
        objectsByKeyValue[value] = (objectsByKeyValue[value] || []).concat(obj);
        return objectsByKeyValue;
    }, {});


function LoadReport() {
    var filtermodel = new Object();
    filtermodel.MonthId = $('#MonthId').val() == '' ? '0' : $('#MonthId').val();
    filtermodel.YearId = $('#YearId').val() == '' ? '0' : $('#YearId').val();
    var formData = $('#formid').serialize();
    $('#errormsg').html("");
    $('#global-loader').css('background', 'transparent').css('display', 'block');
    $('#errormsg').removeClass("text-danger");
    $.ajax({
        url: document.baseURI + "/Home/GetCallingChartMonth",
        type: "POST",
        data: filtermodel,
        // contentType: "application/json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var resdata = JSON.parse(resp.Data);
                var resdata1 = JSON.parse(resp.Data2);
                var resdata2 = JSON.parse(resp.Data3);
                var resdata3 = JSON.parse(resp.Data4);
                $('#errormsg').addClass("text-black");
                $('#global-loader').css('background', 'transparent').css('display', 'none');
                var Data1 = [], Data2 = [], Data2 = [],
                    Data3 = [], Data4 = [], Data5 = [];
                if (resdata) {
                    //BatchData = resdata.filter(x => x.Type == "Batch");
                    ChartCallMonth(resdata);
                }
                if (resdata1) {
                    ChartSalaryPackage(resdata1);
                }
                if (resdata2) {
                    ChartWorkLocation(resdata2);
                }
                if (resdata3) {
                    ChartPalce(resdata3);
                }
            }
            else {
                $('#errormsg').html(resp.Data); $('#errormsg').addClass("text-danger");
                //CreateToasterMessage("Error", response.Message, response.StatusType);
                $('#global-loader').css('background', 'transparent').css('display', 'none');
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            $('#errormsg').html(errormsg); $('#errormsg').addClass("text-danger");
            $('#global-loader').css('background', 'transparent').css('display', 'none');
        }
    });
}

function ChartCallMonth(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var Dlist = [], cate = [];
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].MonthYear);
            //var obj = [Data[i].ColumnName, Data[i].NoofData];
            Dlist.push(Data[i].NoofData);
        }

        // Set up the chart
        chart = Highcharts.chart('chartCallmonth', {
            chart: {
                type: 'line',
                borderWidth: 1,
                //options3d: {
                //    enabled: true,
                //    alpha: 10,
                //    depth: 50,
                //    viewDistance: 50
                //}
            },
            xAxis: {
                categories: cate
            },
            title: {
                text: 'Monthly Calling'
            },
            credits: {
                enabled: false
            },
            //plotOptions: {
            //    series: {
            //        dataLabels: {
            //            enabled: true,
            //            format: '<b>{point.name}</b> ({point.y:,.0f})',
            //            allowOverlap: true,
            //            x: 10,
            //            y: -5
            //        },
            //        width: '60%',
            //        height: '80%',
            //        center: ['50%', '45%']
            //    }
            //},
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: [{
                name: 'Monthly Calling',
                data: Dlist
            }]
        });
    }
}
function ChartSalaryPackage(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var Dlist = [], cate = [];
        for (var i = 0; i < Data.length; i++) {
            cate.push(Data[i].MonthYear);
            //var obj = [Data[i].ColumnName, Data[i].NoofData];
            Dlist.push(Data[i].SalaryPackage);
        }

        // Set up the chart
        chart = Highcharts.chart('chartsalarypackage', {
            chart: {
                type: 'line',
                borderWidth: 1,
                //options3d: {
                //    enabled: true,
                //    alpha: 10,
                //    depth: 50,
                //    viewDistance: 50
                //}
            },
            xAxis: {
                categories: cate
            },
            title: {
                text: 'Salary Package'
            },
            subtitle: {
                text:"(CTC in lakhs per annum)"
            },
            credits: {
                enabled: false
            },
            //plotOptions: {
            //    series: {
            //        dataLabels: {
            //            enabled: true,
            //            format: '<b>{point.name}</b> ({point.y:,.0f})',
            //            allowOverlap: true,
            //            x: 10,
            //            y: -5
            //        },
            //        width: '60%',
            //        height: '80%',
            //        center: ['50%', '45%']
            //    }
            //},
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: [{
                name: 'Salary Package',
                data: Dlist
            }]
        });
    }
}
function ChartWorkLocation(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var Dlist = [], cate = [];
        for (var i = 0; i < Data.length; i++) {
            //var obj = [Data[i].ColumnName, Data[i].NoofData];
            Dlist.push({ name: Data[i].name, y: Data[i].NoofData });
        }

        // Set up the chart
        (function (H) {
            H.seriesTypes.pie.prototype.animate = function (init) {
                const series = this,
                    chart = series.chart,
                    points = series.points,
                    {
                        animation
                    } = series.options,
                    {
                        startAngleRad
                    } = series;

                function fanAnimate(point, startAngleRad) {
                    const graphic = point.graphic,
                        args = point.shapeArgs;

                    if (graphic && args) {

                        graphic
                            // Set inital animation values
                            .attr({
                                start: startAngleRad,
                                end: startAngleRad,
                                opacity: 1
                            })
                            // Animate to the final position
                            .animate({
                                start: args.start,
                                end: args.end
                            }, {
                                duration: animation.duration / points.length
                            }, function () {
                                // On complete, start animating the next point
                                if (points[point.index + 1]) {
                                    fanAnimate(points[point.index + 1], args.end);
                                }
                                // On the last point, fade in the data labels, then
                                // apply the inner size
                                if (point.index === series.points.length - 1) {
                                    series.dataLabelsGroup.animate({
                                        opacity: 1
                                    },
                                        void 0,
                                        function () {
                                            points.forEach(point => {
                                                point.opacity = 1;
                                            });
                                            series.update({
                                                enableMouseTracking: true
                                            }, false);
                                            chart.update({
                                                plotOptions: {
                                                    pie: {
                                                        innerSize: '40%',
                                                        borderRadius: 8
                                                    }
                                                }
                                            });
                                        });
                                }
                            });
                    }
                }

                if (init) {
                    // Hide points on init
                    points.forEach(point => {
                        point.opacity = 0;
                    });
                } else {
                    fanAnimate(points[0], startAngleRad);
                }
            };
        }(Highcharts));

        Highcharts.chart('chartworklocation', {
            chart: {
                type: 'pie',
                borderWidth: 1,
            },
            title: {
                text: 'Workplace distance',
                align: 'center'
            },
            credits: {
                enabled: false
            },
            //subtitle: {
            //    text: 'Workplace distance',
            //    align: 'left'
            //},
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            accessibility: {
                point: {
                    valueSuffix: '%'
                }
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    borderWidth: 2,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b><br>{point.percentage}%',
                        distance: 20
                    }
                }
            },
            series: [{
                // Disable mouse tracking on load, enable after custom animation
                enableMouseTracking: false,
                animation: {
                    duration: 2000
                },
                colorByPoint: true,
                data: Dlist
            }]
        });

    }
}
function ChartPalce(Data) {
    if (Data.length > 0) {
        //var objd = new Object();
        var Dlist = [], cate = [];
        for (var i = 0; i < Data.length; i++) {
            var obj = [Data[i].ColumnName, Data[i].NoofData];
            Dlist.push(obj);
        }

        // Set up the chart
        Highcharts.chart('chartpalce', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: 0,
                plotShadow: false,
                borderWidth: 1,
            },
            title: {
                text: 'Confirmation of placement',
                align: 'center',
                //verticalAlign: 'middle',
                //y: 60,
                style: {
                    fontSize: '1.1em'
                }
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            accessibility: {
                point: {
                    valueSuffix: '%'
                }
            },
            plotOptions: {
                pie: {
                    dataLabels: {
                        enabled: true,
                        distance: -50,
                        style: {
                            fontWeight: 'bold',
                            color: 'white'
                        }
                    },
                    startAngle: -90,
                    endAngle: 90,
                    center: ['50%', '75%'],
                    size: '110%'
                }
            },
            series: [{
                type: 'pie',
                name: '',
                innerSize: '50%',
                data: Dlist
            }]
        });


    }
      
}
