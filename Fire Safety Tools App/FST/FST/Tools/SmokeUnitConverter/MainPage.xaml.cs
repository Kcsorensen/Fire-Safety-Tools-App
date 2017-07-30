using FST.Extensions;
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
    public partial class MainPage : ContentPage
    {
        public DataModel DataModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            DataModel = new DataModel()
            {
                D010Log = 0.0,
                S = 0.0,
                S0 = 5.0,
                Ys = 0.0,
                Hrr = 1000,
                Pod = 8700,
                DeltaHAir = 0,
                DeltaHMat = 14000,
                Rho0 = 1.205
            };

            BindingContext = DataModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            // TODO: Mangler Clear-funktion.
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            // TODO: Mangler InfoPage-funktion.
        }

        private void CustomPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Afhængigt af hvilken kombination af From/To skal kun bestemte Cells under "Required Known Conditions" være synlige.

            var test = (tableView.Root[1][0] as DataEntryCell).ClassId;

            if ((tableView.Root[1][0] as DataEntryCell).Label.Equals(RequiredKnownConditions.D010log))
            {
                int i = 1;
            }

            // Få en lise af alle Cells
            //var allDataCells = tableView.Root[1];

            //if (DataModel.SelectedConvertTo == SmokeUnits.SmokePotentialArgos)
            //{
            //    if (DataModel.SelectedConvertTo == SmokeUnits.SootYield)
            //    {
            //        var selectedDataCells = allDataCells.Where(a => ((DataEntryCell)a).Label == SmokeUnits.po)
            //    }

            //    if (DataModel.SelectedConvertTo == SmokeUnits.SmokeProduction)
            //    {

            //    }

            //    if (DataModel.SelectedConvertTo == SmokeUnits.SmokePotentialBurnedFuel)
            //    {

            //    }
            //}

            //var saveDataCell = (tableView.Root[1][0] as DataEntryCell);

            //tableView.Root[1].RemoveAt(0);

            //tableView.Root[1].Add(saveDataCell);
        }
    }
}