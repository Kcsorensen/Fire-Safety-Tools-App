using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SteelHeatingUnderFire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultDataTablePage : ContentPage
    {
        private List<LineSeries> _lineSeries;
        private bool _showDetailedDataTable;

        public List<DataSet> DataTable { get; set; }

        public ResultDataTablePage(List<LineSeries> lineSeries, bool showDetailedDataTable)
        {
            _lineSeries = lineSeries;
            _showDetailedDataTable = showDetailedDataTable;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            DataTable = new List<DataSet>();

            

            if (_lineSeries.Count == 1)
            {
                if (_showDetailedDataTable == true)
                {
                    // Find ud af hvor mange gange 30 min går op i simuleringstiden.
                    var factor =  Math.Floor(_lineSeries[0].Points.Max(a => a.X) / 30.0);

                    for (int i = 0; i <= factor; i++)
                    {
                        DataTable.Add(new DataSet()
                        {
                            Time = 30 * i,
                            UnprotectedTemperature = Math.Round(_lineSeries[0].Points.First(a => a.X == 30 * i).Y, 2)
                        });
                    }
                }
                else
                {
                    foreach (var item in _lineSeries[0].Points)
                    {
                        DataTable.Add(new DataSet()
                        {
                            Time = Math.Round(item.X, 1),
                            UnprotectedTemperature = Math.Round(item.Y, 2)
                        });
                    }
                }

                
            }

            if (_lineSeries.Count == 2)
            {
                if (_showDetailedDataTable == true)
                {
                    // Find ud af hvor mange gange 30 min går op i simuleringstiden.
                    var factor = Math.Floor(_lineSeries[0].Points.Max(a => a.X) / 30.0);

                    for (int i = 0; i <= factor; i++)
                    {
                        DataTable.Add(new DataSet()
                        {
                            Time = 30 * i,
                            UnprotectedTemperature = Math.Round(_lineSeries[1].Points.First(a => a.X == 30 * i).Y, 2),
                            ProtectedTemperature = Math.Round(_lineSeries[0].Points.First(a => a.X == 30 * i).Y , 2)
                        });
                    }
                }
                else
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