using OxyPlot.Series;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SteelHeatingUnderFire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultDataTablePage : ContentPage
    {
        private List<LineSeries> _lineSeries;

        public List<DataSet> DataTable { get; set; }

        public ResultDataTablePage(List<LineSeries> lineSeries)
        {
            _lineSeries = lineSeries;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            DataTable = new List<DataSet>();

            if (_lineSeries.Count == 1)
            {
                foreach (var item in _lineSeries[0].Points)
                {
                    DataTable.Add(new DataSet()
                    {
                        Time = Math.Round(item.X,1),
                        UnprotectedTemperature = Math.Round(item.Y, 2)
                    });
                }
            }

            if (_lineSeries.Count == 2)
            {
                for (int i = 0; i < _lineSeries[0].Points.Count; i++)
                {
                    var _time = _lineSeries[0].Points[i].X;
                    var _protected = _lineSeries[0].Points[i].Y;
                    var _unprotected = _lineSeries[1].Points[i].Y;

                    DataTable.Add(new DataSet()
                    {
                        Time = Math.Round(_time, 1),
                        UnprotectedTemperature = Math.Round(_unprotected, 2),
                        ProtectedTemperature = Math.Round(_protected, 2)
                    });
                }
            }

            BindingContext = this;

            base.OnAppearing();
        }
    }

    public class DataSet
    {
        public double Time { get; set; }
        public double UnprotectedTemperature { get; set; }
        public double ProtectedTemperature { get; set; }
    }
}