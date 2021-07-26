<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard1.aspx.cs" Inherits="SchoolApp.Web.School.Dashboard1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CSSContent" runat="server">
    <style>
        .panel-success > .panel-heading {
            background-color: #1c84c6 !important;
            border-color: #1c84c6;
            color: #ffffff;
        }

        .panel-info > .panel-heading {
            background-color: #23c6c8 !important;
            border-color: #23c6c8;
            color: #ffffff;
        }

        .panel-danger > .panel-heading {
            background-color: #ed5565 !important;
            border-color: #ed5565;
            color: #ffffff;
        }

        .panel-warning > .panel-heading {
            background-color: #f8ac59 !important;
            border-color: #f8ac59;
            color: #ffffff;
        }

        .panel-primary > .panel-heading {
            background-color: #18a689 !important;
            border-color: #18a689;
            color: #ffffff;
        }

        .panel-default > .panel-heading {
            background-color: #f5f5f5 !important;
            border-color: #ddd;
            color: #333;
        }

        .big-icon1 {
            font-size: 40px;
            color: #676a6c;
        }

        .headers {
            font-size: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="jsContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        // Here We will fill chartData
        google.setOnLoadCallback(drawChart);
        google.setOnLoadCallback(drawLineChart);
        function drawChart() {
            var options = {
                title: "Company Revenue",
                pointSize: 5
            };
            //$(document).ready(function () {

            $.ajax({
                url: "Dashboard1.aspx/GetChartData",
                data: '{}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function (data) {
                    //chartData = data.d;
                    var data1 = google.visualization.arrayToDataTable(data.d);
                    var chart = new google.visualization.PieChart(document.getElementById('chart-div'));                    
                    chart.draw(data1, options);                    
                },
                failure: function (data) {
                    alert(data.d);
                },
                error: function (data) {
                    alert(data.d);
                }
            })

            //}
            //);
        };

        function drawLineChart() {
            var options = {
                title: "Company Revenue",
                pointSize: 5
            };
            //$(document).ready(function () {

            $.ajax({
                url: "Dashboard1.aspx/GetChartData1",
                data: '{}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function (data) {
                    //chartData = data.d;
                    //var data1 = google.visualization.arrayToDataTable(data.d);
                    //var chart = new google.visualization.LineChart(document.getElementById('chart-div1'));                    
                    //chart.draw(data1, options);
                    drawChart1(data.d);
                },
                failure: function (data) {
                    alert(data.d);
                },
                error: function (data) {
                    alert(data.d);
                }
            })

            //}
            //);
        }


        function drawChart1(d) {
            var chartData = d;
            var data = null;
            data = google.visualization.arrayToDataTable(chartData);

            var view = new google.visualization.DataView(data);
            view.setColumns([0, {
                type: 'number',
                label: data.getColumnLabel(0),
                calc: function () { return 0; }
            }, {
                    type: 'number',
                    label: data.getColumnLabel(1),
                    calc: function () { return 0; }
                }, {
                    type: 'number',
                    label: data.getColumnLabel(2),
                    calc: function () { return 0; }
                }, {
                    type: 'number',
                    label: data.getColumnLabel(3),
                    calc: function () { return 0; }
                }]);

            var chart = new google.visualization.ColumnChart(document.getElementById('visualization'));
            var options = {
                title: 'Sales Report',
                legend: 'bottom',
                hAxis: {
                    title: 'Month',
                    format: '#'
                },
                vAxis: {
                    minValue: 0,
                    maxValue: 1000000,
                    title: 'Sales Amount'
                },
                chartArea: {
                    left: 100, top: 50, width: '70%', height: '50%'
                },
                animation: {
                    duration: 1000
                }
            };

            var runFirstTime = google.visualization.events.addListener(chart, 'ready', function () {
                google.visualization.events.removeListener(runFirstTime);
                chart.draw(data, options);
            });

            chart.draw(view, options);
        }

    </script>
    
    
     <div id="chart-div" style="width: 550px; height: 400px">
                        </div>
    <div id="chart-div1" style="width: 550px; height: 400px">
    </div>
        
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div id="chart-div" style="width: 500px; height: 400px">
    </div>--%>
</asp:Content>
