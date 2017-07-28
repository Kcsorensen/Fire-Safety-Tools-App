using FST.Models;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FST.Tools.SteelHeatingUnderFire
{
    public class DataModel : BaseModel
    {
        #region private fields
        private double _simulationTime;
        private double _steelSectionFactor;
        private double _steelDensity;
        private double _steelSpecificHeat;
        private double _isoSpecificHeat;
        private double _steelEmissivity;
        private string _selectedFireCurveType;
        private double _heatTransferCoeffficient;
        private bool _isSteelProtected;
        private bool _showDetailedDataTable;
        private double _isoThickness;
        private double _isoThermalConductivity;
        private double _isoDensity;

        private double _dt;
        private double _epsilon;
        #endregion

        #region public properties
        public double SimulationTime
        {
            get { return _simulationTime; }
            set { SetValue(ref _simulationTime, value); }
        }
        public double SteelSectionFactor
        {
            get { return _steelSectionFactor; }
            set { SetValue(ref _steelSectionFactor, value); }
        }
        public double SteelDensity
        {
            get { return _steelDensity; }
            set { SetValue(ref _steelDensity, value); }
        }
        public double SteelSpecificHeat
        {
            get { return _steelSpecificHeat; }
            set { SetValue(ref _steelSpecificHeat, value); }
        }
        public double SteelEmissivity
        {
            get { return _steelEmissivity; }
            set { SetValue(ref _steelEmissivity, value); }
        }
        public string SelectedFireCurveType
        {
            get { return _selectedFireCurveType; }
            set { SetValue(ref _selectedFireCurveType, value); }
        }
        public double HeatTransferCoeffficient
        {
            get { return _heatTransferCoeffficient; }
            set { SetValue(ref _heatTransferCoeffficient, value); }
        }
        public bool IsSteelProtected
        {
            get { return _isSteelProtected; }
            set { SetValue(ref _isSteelProtected, value); }
        }
        public bool ShowDetailedDataTable
        {
            get { return _showDetailedDataTable; }
            set { SetValue(ref _showDetailedDataTable, value); }
        }
        public double IsoThickness
        {
            get { return _isoThickness; }
            set { SetValue(ref _isoThickness, value); }
        }
        public double IsoThermalConductivity
        {
            get { return _isoThermalConductivity; }
            set { SetValue(ref _isoThermalConductivity, value); }
        }
        public double IsoDensity
        {
            get { return _isoDensity; }
            set { SetValue(ref _isoDensity, value); }
        }
        public double IsoSpecificHeat
        {
            get { return _isoSpecificHeat; }
            set { SetValue(ref _isoSpecificHeat, value); }
        }

        public List<Tuple<double, double>> FireCurveUnprotected { get; set; }
        public List<Tuple<double, double>> FireCurveProtected { get; set; }
        #endregion

        public DataModel()
        {
            // Hver time deles op i 120 dele
            _dt = 1.0 / 120;

            // Stefan–Boltzmann constant
            _epsilon = 5.670367 * Math.Pow(10, -8);
        }

        public async Task UpdateFireCurvesAsync()
        {
            // Bestem hvor mange tidsskridt der skal være i beregningen ud fra den angivet simuleringstid. 
            // Hvis dt ikke går op i den angivet simuleringstid, rundes tidsskridtet op til nærmeste heltal.
            int timeSteps = Convert.ToInt32(Math.Ceiling(SimulationTime / _dt));

            // Clear listen for tid.
            List<double> _t = new List<double>();

            // Clear listen for Stålets temperatur og sæt første værdi til 20 °C
            List<double> _T_steelUnprotected = new List<double>() { 20 };
            List<double> _T_steelProtected = new List<double>() { 20 };

            // Clear listen med data for FireCurve og sæt første dataset til tid = 0 min og ståltemperatur = 20 °C.
            FireCurveUnprotected = new List<Tuple<double, double>>() { new Tuple<double, double>(0, 20) };
            FireCurveProtected = new List<Tuple<double, double>>() { new Tuple<double, double>(0, 20) };

            // Lav en liste med alle tidsskridt i minutter, som også er x-akse på grafen. [0, 0.5, 1, 1.5, 2, ....]
            await Task.Run(() =>
            {
                for (int i = 0; i <= timeSteps; i++)
                {
                    _t.Add(_dt * i * 60.0);
                }
            });

            if (IsSteelProtected == true)
            {
                for (int i = 0; i < timeSteps; i++)
                {
                    var _dt_half = _t[i] + (_dt * 60) / 2.0;

                    var fireTemperature = await _getFireCurveValueAsync(_dt_half, SelectedFireCurveType);

                    var _dT_steelUnprotected = await _getDeltaTemperatureForUnprotectedSteelAsync(_T_steelUnprotected[i], fireTemperature);
                    var _dT_steelprotected = await _getDeltaTemperatureForProtectedSteelAsync(_T_steelProtected[i], fireTemperature);

                    _T_steelUnprotected.Add(_T_steelUnprotected[i] + _dT_steelUnprotected);
                    _T_steelProtected.Add(_T_steelProtected[i] + _dT_steelprotected);

                    FireCurveUnprotected.Add(new Tuple<double, double>(_t[i + 1], _T_steelUnprotected[i + 1]));
                    FireCurveProtected.Add(new Tuple<double, double>(_t[i + 1], _T_steelProtected[i + 1]));
                }
            }
            else
            {
                for (int i = 0; i < timeSteps; i++)
                {
                    var _dt_half = _t[i] + (_dt * 60) / 2.0;

                    var fireTemperature = await _getFireCurveValueAsync(_dt_half, SelectedFireCurveType);

                    var _dT_steel = await _getDeltaTemperatureForUnprotectedSteelAsync(_T_steelUnprotected[i], fireTemperature);

                    _T_steelUnprotected.Add(_T_steelUnprotected[i] + _dT_steel);

                    FireCurveUnprotected.Add(new Tuple<double, double>(_t[i + 1], _T_steelUnprotected[i + 1]));
                }
            }
        }

        public async Task<List<LineSeries>> GetLinesSeriesForPlotModelAsync()
        {
            if (IsSteelProtected == true)
            {
                var lineSeries = new List<LineSeries>()
                {
                    new LineSeries()
                    {
                        MarkerType = MarkerType.None,
                        Color = OxyColors.Red,
                        LineStyle = LineStyle.Solid,
                        Title = "Protected"

                    },
                    new LineSeries()
                    {
                        MarkerType = MarkerType.None,
                        Color = OxyColors.Blue,
                        LineStyle = LineStyle.LongDash,
                        Title = "Unprotected"
                    }
                };

                await Task.Run(() =>
                {
                    foreach (var dataSet in FireCurveProtected)
                    {
                        lineSeries[0].Points.Add(new DataPoint(dataSet.Item1, dataSet.Item2));
                    }

                    foreach (var dataSet in FireCurveUnprotected)
                    {
                        lineSeries[1].Points.Add(new DataPoint(dataSet.Item1, dataSet.Item2));
                    }

                });

                return lineSeries;
            }
            else
            {
                var lineSeries = new List<LineSeries>()
                {
                    new LineSeries()
                    {
                        MarkerType = MarkerType.None,
                        Color = OxyColors.Red,
                        LineStyle = LineStyle.Solid,
                        Title = "Unprotected"
                    }
                };

                await Task.Run(() =>
                {
                    foreach (var dataSet in FireCurveUnprotected)
                    {
                        lineSeries[0].Points.Add(new DataPoint(dataSet.Item1, dataSet.Item2));
                    }
                });

                return lineSeries;
            }
        }

        /// <summary>
        /// Beregn brandens flammetemperatur til en given tid [min] og type af standardbrand.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="fireCurveType"></param>
        /// <returns></returns>
        private async Task<double> _getFireCurveValueAsync(double time, string fireCurveType)
        {
            double result = 0.0;

            await Task.Run(() =>
            {
                if (fireCurveType == FireCurveTypes.ISO834)
                {
                    result = 20.0 + 345.0 * Math.Log10(8.0 * time + 1);
                }

                if (fireCurveType == FireCurveTypes.ASTME119)
                {
                    result = (750.0 * (1 - Math.Exp(-3.79553 * Math.Sqrt(time / 60.0))) + 170.41 * Math.Sqrt(time / 60.0) + 20);
                }
            });

            return result;
        }

        /// <summary>
        /// Beregn den ubeskyttede ståltemperatur for det nye tidskridt ud fra det tidligere tidsskridts stråltemperatur og flammetemperatur for tiden t + dt/2
        /// </summary>
        /// <param name="_T_steel_pre"></param>
        /// <param name="fireTemperature"></param>
        /// <returns></returns>
        private async Task<double> _getDeltaTemperatureForUnprotectedSteelAsync(double _T_steel_pre, double fireTemperature)
        {
            double result = 0.0;

            await Task.Run(() =>
            {
                result = SteelSectionFactor * 1.0 / (SteelDensity * SteelSpecificHeat) *
                    (HeatTransferCoeffficient * (fireTemperature - _T_steel_pre) + SteelEmissivity * _epsilon * (Math.Pow(fireTemperature + 273, 4) - Math.Pow(_T_steel_pre + 273, 4))) * _dt * 3600;
            });

            return result;
        }

        /// <summary>
        /// Beregn den beskyttede ståltemperatur for det nye tidskridt ud fra det tidligere tidsskridts stråltemperatur og flammetemperatur for tiden t + dt/2
        /// </summary>
        /// <param name="_T_steel_pre"></param>
        /// <param name="fireTemperature"></param>
        /// <returns></returns>
        private async Task<double> _getDeltaTemperatureForProtectedSteelAsync(double _T_steel_pre, double fireTemperature)
        {
            double result = 0.0;

            await Task.Run(() =>
            {
                result = SteelSectionFactor * IsoThermalConductivity / (IsoThickness * SteelDensity * SteelSpecificHeat) *
                    ((SteelDensity * SteelSpecificHeat) / (SteelDensity * SteelSpecificHeat + (SteelSectionFactor * IsoThickness * IsoDensity * IsoSpecificHeat) / 2.0)) * (fireTemperature - _T_steel_pre) * _dt * 3600;
            });

            return result;
        }
    }
}
