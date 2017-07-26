using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.Tools.SteelHeatingUnderFire
{
    public class SteelHeatingTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double SimulationTime { get; set; }

        public double SteelSectionFactor { get; set; }
        public double SteelDensity { get; set; }
        public double SteelSpecificHeat { get; set; }
        public double SteelEmissivity { get; set; }

        public int FireCurveType { get; set; }
        public double HeatTransferCoeffficient { get; set; }

        public bool IsSteelProtected { get; set; }

        public double IsoThickness { get; set; }
        public double IsoThermalConductivity { get; set; }
        public double IsoDensity { get; set; }
        public double IsoSpecificHeat { get; set; }

    }
}
