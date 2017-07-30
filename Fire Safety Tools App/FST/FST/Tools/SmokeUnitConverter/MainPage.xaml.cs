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
            //if (DataModel == null)
            //    return;

            //if (DataModel.SelectedConvertFrom == null)
            //    return;

            //if (DataModel.SelectedConvertTo == null)
            //    return;

            //// Afhængigt af hvilken kombination af From/To skal kun bestemte Cells under "Required Known Conditions" være synlige.

            //// Få en liste af alle Cells
            //var allDataCells = tableView.Root[1];

            //var test = allDataCells.Where(a => (a as DataEntryCell).ClassId == RequiredKnownConditions.Ys);

            //if (DataModel.SelectedConvertTo == SmokeUnits.SmokePotentialArgos)
            //{
            //    List<Cell> selectedDataCells = new List<Cell>();

            //    selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.Density));
            //    selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hair));

            //    if (DataModel.SelectedConvertFrom == SmokeUnits.SootYield)
            //    {
            //        selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.Ys));
            //        selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.Pod));
            //        selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
            //    }

            //    if (DataModel.SelectedConvertFrom == SmokeUnits.SmokeProduction)
            //    {
            //        selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.S));
            //        selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hrr));
            //    }

            //    if (DataModel.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
            //    {
            //        selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.D010log));
            //        selectedDataCells.Add(allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
            //    }
            //}

            //var saveDataCell = (tableView.Root[1][0] as DataEntryCell);

            //tableView.Root[1].RemoveAt(0);

            //tableView.Root[1].Add(saveDataCell);
        }
    }
}