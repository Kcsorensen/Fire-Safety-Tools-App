using SQLite;
using System.Collections.Generic;

namespace FST.Tools.SmokeUnitConverter
{
    public class SmokeUnitTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double D010Log { get; set; }
        public double S { get; set; }
        public double S0 { get; set; }
        public double Ys { get; set; }
        public double Hrr { get; set; }
        public double Pod { get; set; }
        public double DeltaHAir { get; set; }
        public double DeltaHMat { get; set; }
        public double Rho0 { get; set; }

        public string SelectedConvertFrom { get; set; }
        public string SelectedConvertTo { get; set; }

        public List<string> SortedListConvertFrom { get; set; }
        public List<string> SortedListConvertTo { get; set; }
    }
}
