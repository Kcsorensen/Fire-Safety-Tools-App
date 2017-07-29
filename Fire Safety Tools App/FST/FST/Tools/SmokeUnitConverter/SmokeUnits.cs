using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.Tools.SmokeUnitConverter
{
    public class SmokeUnits
    {
        public const string SmokePotentialBurnedFuel = "Smoke Potential (Burned Fuel)";
        public const string SmokePotentialArgos = "Smoke Potential (Argos)";
        public const string SmokeProduction = "Smoke Production";
        public const string SootYield = "Soot Yield";
        public const string HeatReleaseRate = "Heat Release Rate";
        public const string HeatOfCombustion = "Heat of Combustion";
        public const string ExtenctionCoefficient = "Extinction Coefficient";
        public const string AirDensity = "Ambient Air Density";
        public const string EnthalpyForAir = "Ambient Air Enthalpy";

        public static List<string> List = new List<string>()
        {
            SmokePotentialBurnedFuel,
            SmokePotentialArgos,
            SmokeProduction,
            SootYield,
            HeatReleaseRate,
            HeatOfCombustion,
            ExtenctionCoefficient,
            AirDensity,
            EnthalpyForAir
        };
    }

    


}
