using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SteelHeatingUnderFire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        public ResultPage(LineSeries lineSeries)
        {
            InitializeComponent();

            var plotModel = new PlotModel { Title = "Steal Heating vs Time" };

            plotModel.Axes.Add(new LinearAxis
            {
                Title = "Time [min]",
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                Minimum = 0.0
            });

            plotModel.Axes.Add(new LinearAxis
            {
                Title = "Temperature [°C]",
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                Minimum = 0.0
            });

            plotModel.Series.Add(lineSeries);

            BindingContext = plotModel;
        }
    }
}