using FST.Models;
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
        private int _fireCurve;
        private double _heatTransferCoeffficient;
        private bool _isSteelProtected;
        private double _isoThickness;
        private double _isoThermalConductivity;
        private double _isoDensity;
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
        public int FireCurve
        {
            get { return _fireCurve; }
            set { SetValue(ref _fireCurve, value); }
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
        #endregion




    }
}
