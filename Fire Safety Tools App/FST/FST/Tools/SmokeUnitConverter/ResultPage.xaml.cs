using FST.Persistance;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SmokeUnitConverter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage   
    {
        private SQLiteAsyncConnection _connection;
        private double _result;

        public string PrerequesitsText { get; set; }
        public string ConvertFromText { get; set; }
        public string ResultText { get; set; }

        public ResultPage(double result)
        {
            InitializeComponent();

            _result = result;

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            var db = await _connection.Table<SmokeUnitTable>().FirstAsync();

            if (db.SelectedConvertTo == SmokeUnits.SmokePotentialArgos)
            {
                if (db.SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    PrerequesitsText = " - " + SmokeUnits.AirDensity + " = " + db.Rho0 + " kg/m³" + Environment.NewLine +
                        " - " + SmokeUnits.EnthalpyForAir + " = " + db.DeltaHAir + " kj/kg" + Environment.NewLine +
                        " - " + SmokeUnits.ExtenctionCoefficient + " = " + db.Pod + " m²/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg";

                    ConvertFromText = string.Format("The {0} of {1} is equivalent to:", SmokeUnits.SootYield, db.Ys);

                    ResultText = SmokeUnits.SmokePotentialArgos + " = " + _result + " ob";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    PrerequesitsText = " - " + SmokeUnits.AirDensity + " = " + db.Rho0 + " kg/m³" + Environment.NewLine +
                        " - " + SmokeUnits.EnthalpyForAir + " = " + db.DeltaHAir + " kj/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatReleaseRate + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob m³/s is equivalent to:", SmokeUnits.SmokeProduction, db.S);

                    ResultText = SmokeUnits.SmokePotentialArgos + " = " + _result + " ob";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    PrerequesitsText = " - " + SmokeUnits.AirDensity + " = " + db.Rho0 + " kg/m³" + Environment.NewLine +
                        " - " + SmokeUnits.EnthalpyForAir + " = " + db.DeltaHAir + " kj/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob m³/g is equivalent to:", SmokeUnits.SmokePotentialBurnedFuel, db.D010Log);

                    ResultText = SmokeUnits.SmokePotentialArgos + " = " + _result + " ob";
                }
            }

            if (db.SelectedConvertTo == SmokeUnits.SmokePotentialBurnedFuel)
            {
                if (db.SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    PrerequesitsText = " - " + SmokeUnits.ExtenctionCoefficient + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} is equivalent to:", SmokeUnits.SootYield, db.Ys);

                    ResultText = SmokeUnits.SmokePotentialBurnedFuel + " = " + _result + " ob m³/g";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    PrerequesitsText = " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatReleaseRate + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob m³/s is equivalent to:", SmokeUnits.SmokeProduction, db.S);

                    ResultText = SmokeUnits.SmokePotentialBurnedFuel + " = " + _result + " ob m³/g";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    PrerequesitsText = " - " + SmokeUnits.AirDensity + " = " + db.Rho0 + " kg/m³" + Environment.NewLine +
                        " - " + SmokeUnits.EnthalpyForAir + " = " + db.DeltaHAir + " kj/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob is equivalent to:", SmokeUnits.SmokePotentialArgos, db.S0);

                    ResultText = SmokeUnits.SmokePotentialBurnedFuel + " = " + _result + " ob m³/g";
                }
            }

            if (db.SelectedConvertTo == SmokeUnits.SootYield)
            {
                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    PrerequesitsText = " - " + SmokeUnits.ExtenctionCoefficient + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob m³/g is equivalent to:", SmokeUnits.SmokePotentialBurnedFuel, db.D010Log);

                    ResultText = SmokeUnits.SootYield + " = " + _result + "";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    PrerequesitsText = " - " + SmokeUnits.ExtenctionCoefficient + " = " + db.Pod + " m²/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatReleaseRate + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob m³/s is equivalent to:", SmokeUnits.SmokeProduction, db.S);

                    ResultText = SmokeUnits.SootYield + " = " + _result + "";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    PrerequesitsText = " - " + SmokeUnits.AirDensity + " = " + db.Rho0 + " kg/m³" + Environment.NewLine +
                        " - " + SmokeUnits.EnthalpyForAir + " = " + db.DeltaHAir + " kj/kg" + Environment.NewLine +
                        " - " + SmokeUnits.ExtenctionCoefficient + " = " + db.Pod + " m²/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob is equivalent to:", SmokeUnits.SmokePotentialArgos, db.S0);

                    ResultText = SmokeUnits.SootYield + " = " + _result + "";
                }
            }

            if (db.SelectedConvertTo == SmokeUnits.SmokeProduction)
            {
                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    PrerequesitsText = " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatReleaseRate + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob m³/g is equivalent to:", SmokeUnits.SmokePotentialBurnedFuel, db.D010Log);

                    ResultText = SmokeUnits.SmokePotentialBurnedFuel + " = " + _result + " ob m³/g";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    PrerequesitsText = " - " + SmokeUnits.ExtenctionCoefficient + " = " + db.Pod + " m²/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatOfCombustion + " = " + db.DeltaHMat + " kJ/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatReleaseRate + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} is equivalent to:", SmokeUnits.SootYield, db.Ys);

                    ResultText = SmokeUnits.SmokePotentialBurnedFuel + " = " + _result + " ob m³/g";
                }

                if (db.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    PrerequesitsText = " - " + SmokeUnits.AirDensity + " = " + db.Rho0 + " kg/m³" + Environment.NewLine +
                        " - " + SmokeUnits.EnthalpyForAir + " = " + db.DeltaHAir + " kj/kg" + Environment.NewLine +
                        " - " + SmokeUnits.HeatReleaseRate + " = " + db.Pod + " m²/kg";

                    ConvertFromText = string.Format("The {0} of {1} ob is equivalent to:", SmokeUnits.SmokePotentialArgos, db.S0);

                    ResultText = SmokeUnits.SmokeProduction + " = " + _result + " ob m³/s";
                }
            }

            BindingContext = this;

            base.OnAppearing();
        }
    }
}