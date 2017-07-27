using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SteelHeatingUnderFire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultGraphPage : ContentPage
    {
        private List<LineSeries> _lineSeries;

        public ResultGraphPage(List<LineSeries> lineSeries)
        {
            _lineSeries = lineSeries;
            
            InitializeComponent();

            var plotModel = new PlotModel { Title = "Steal Heating vs Time" };
            plotModel.LegendPlacement = LegendPlacement.Inside;
            plotModel.LegendPosition = LegendPosition.TopLeft;

            plotModel.Axes.Add(new LinearAxis
            {
                Title = "Time [min]",
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorStep = 60,
                MajorGridlineStyle = LineStyle.Solid,
                Minimum = 0.0,
                MinorStep = 10,
                MinorGridlineStyle = LineStyle.Solid,
                MinorGridlineThickness = 0.5,

            });

            plotModel.Axes.Add(new LinearAxis
            {
                Title = "Temperature [°C]",
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Solid,
                Minimum = 0.0,
                MinorGridlineStyle = LineStyle.Solid,
                MinorGridlineThickness = 0.5
            });

            if (lineSeries.Count == 1)
            {
                plotModel.IsLegendVisible = false;
                plotModel.Series.Add(lineSeries[0]);
            }

            if (lineSeries.Count == 2)
            {
                plotModel.IsLegendVisible = true;
                plotModel.Series.Add(lineSeries[0]);
                plotModel.Series.Add(lineSeries[1]);
            }

            BindingContext = plotModel;
        }

        private void Forward_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ResultDataTablePage(_lineSeries));
        }
    }
}