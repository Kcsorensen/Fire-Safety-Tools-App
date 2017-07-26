using FST.Models;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<string> ListOfFireCurveTypes { get; set; }
        public List<Tuple<double, double>> FireCurve { get; set; }


        #endregion

        public DataModel()
        {
            ListOfFireCurveTypes = new List<string>()
            {
                FireCurveTypes.ISO834,
                FireCurveTypes.ASTME119
            };

            // Hver time deles op i 120 dele
            _dt = 1.0 / 120;

            // Stefan–Boltzmann constant
            _epsilon = 5.670367 * Math.Pow(10, -8);
        }

        public async Task UpdateFireCurveAsync()
        {
            // Bestem hvor mange tidsskridt der skal være i beregningen ud fra den angivet simuleringstid. 
            // Hvis dt ikke går op i den angivet simuleringstid, rundes tidsskridtet op til nærmeste heltal.
            int timeSteps = Convert.ToInt32(Math.Ceiling( SimulationTime / _dt ));

            // Clear listen for tid.
            List<double> _t = new List<double>();

            // Clear listen for Stålets temperatur og sæt første værdi til 20 °C
            List<double> _T_steel = new List<double>() { 20 };

            // Clear listen med data for FireCurve og sæt første dataset til tid = 0 min og ståltemperatur = 20 °C.
            FireCurve = new List<Tuple<double, double>>() { new Tuple<double, double>(0, 20) };

            // Lav en liste med alle tidsskridt i minutter, som også er x-akse på grafen. [0, 0.5, 1, 1.5, 2, ....]
            await Task.Run(() =>
            {
                for (int i = 0; i <= timeSteps; i++)
                {
                    _t.Add(_dt * i * 60.0);
                }
            });

            if (SelectedFireCurveType == FireCurveTypes.ISO834)
            {
                for (int i = 0; i < timeSteps; i++)
                {
                    var _dt_half = _t[i] + (_dt * 60) / 2.0;

                    var fireTemperature = await _getFireCurveValueAsync(_dt_half, SelectedFireCurveType);

                    var _dT_steel = await _getDeltaSteelTemperatureAsync(_T_steel[i], fireTemperature);

                    _T_steel.Add(_T_steel[i] + _dT_steel);

                    FireCurve.Add(new Tuple<double, double>(_t[i + 1], _T_steel[i + 1]));
                }
            }
        }

        public async Task<LineSeries> GetLinesSeriesForPlotModelAsync()
        {
            var lineSeries = new LineSeries()
            {
                MarkerType = MarkerType.None,
                Color = OxyColors.Red,
                
            };

            await Task.Run(() =>
            {
                foreach (var dataSet in FireCurve)
                {
                    lineSeries.Points.Add(new DataPoint(dataSet.Item1, dataSet.Item2));
                }
            });

            return lineSeries;
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
            });

            return result;
        }

        /// <summary>
        /// Beregn ståltemperaturen for det nye tidskridt ud fra det tidligere tidsskridts stråltemperatur og flammetemperaturen for tiden t + dt/2
        /// </summary>
        /// <param name="_T_steel_pre"></param>
        /// <param name="fireTemperature"></param>
        /// <returns></returns>
        private async Task<double> _getDeltaSteelTemperatureAsync(double _T_steel_pre, double fireTemperature)
        {
            double result = 0.0;

            await Task.Run(() =>
            {
                result = SteelSectionFactor * 1.0 / (SteelDensity * SteelSpecificHeat) *
                    (HeatTransferCoeffficient * (fireTemperature - _T_steel_pre) + SteelEmissivity * _epsilon * (Math.Pow(fireTemperature + 273, 4) - Math.Pow(_T_steel_pre + 273, 4))) * _dt * 3600;
            });

            return result;
        }
    }
}
