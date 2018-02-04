using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace Inventry_Management.TestingPages
{
    public class sample : ContentPage
    {
        public sample()
        {
            SfChart chart = new SfChart();

            CategoryAxis primaryAxis = new CategoryAxis();
            
            primaryAxis.Title.Text = "Name";

            chart.PrimaryAxis = primaryAxis;

            //Initializing secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();

            secondaryAxis.LabelRotationAngle = -45;

            secondaryAxis.Title.Text = "Height (in cm)";

            chart.SecondaryAxis = secondaryAxis;

            chart.SecondaryAxis.IsVisible = false;

            chart.SecondaryAxis.LabelsIntersectAction = AxisLabelsIntersectAction.MultipleRows;
            //Initializing column series
            ColumnSeries series = new ColumnSeries();
            series.ItemsSource = new List<Person>()
            {
                new Person { Name = "David", Height = 180 },
                new Person { Name = "Michael", Height = 170 },
                new Person { Name = "Ujjwal", Height = 150 },
                new Person { Name = "Sumit", Height = 200 },
                new Person { Name = "Steve", Height = 160 },
                new Person { Name = "Joel", Height = 182 },
                new Person { Name = "David", Height = 180 },
                new Person { Name = "Michael", Height = 170 },
                new Person { Name = "Ujjwal", Height = 150 },
                new Person { Name = "Sumit", Height = 200 },
                new Person { Name = "Steve", Height = 160 },
                new Person { Name = "David", Height = 180 },
            };

            series.XBindingPath = "Name";

            series.YBindingPath = "Height";

            series.DataMarker = new ChartDataMarker();

            series.Label = "Heights";

            series.EnableTooltip = true;

            chart.Legend = new ChartLegend();

            chart.Series.Add(series);

            //chart.Series.Add(barSeries);
            this.Content = chart;
        }
    }

    class Person
    {
        public string Name { get; set; }

        public double Height { get; set; }
    }
  
    class datamodel
    {
        public string Year { get; set; }
        public double Value { get; set; }
    }
}