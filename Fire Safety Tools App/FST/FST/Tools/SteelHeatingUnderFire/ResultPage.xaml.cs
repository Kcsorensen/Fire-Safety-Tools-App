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

            var plotModel = new PlotModel { Title = "Transient Steel Heating" };
            
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

            plotModel.Series.Add(lineSeries);

            BindingContext = plotModel;
        }
    }
}