using FST.Extensions;
using FST.Persistance;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SmokeUnitConverter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private List<Cell> _allDataCells;
        private SQLiteAsyncConnection _connection;

        public DataModel DataModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            // Få en liste af alle Cells
            if (_allDataCells == null)
            {
                _allDataCells = new List<Cell>();

                foreach (var cell in tableView.Root[1])
                {
                    _allDataCells.Add(cell);
                }
            }

            DataModel = new DataModel();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            await _connection.CreateTableAsync<SmokeUnitTable>();

            if (await _connection.Table<SmokeUnitTable>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new SmokeUnitTable()
                {
                    D010Log = 0.0,
                    S = 0.0,
                    S0 = 150.0,
                    Ys = 0.0,
                    Hrr = 1000,
                    Pod = 8700,
                    DeltaHAir = 3000,
                    DeltaHMat = 14000,
                    Rho0 = 1.205,
                    SelectedConvertFrom = SmokeUnits.SmokePotentialArgos,
                    SelectedConvertTo = SmokeUnits.SootYield
                });
            }

            var db = await _connection.Table<SmokeUnitTable>().FirstAsync();

            DataModel.D010Log = db.D010Log;
            DataModel.S = db.S;
            DataModel.S0 = db.S0;
            DataModel.Ys = db.Ys;
            DataModel.Hrr = db.Hrr;
            DataModel.Pod = db.Pod;
            DataModel.DeltaHAir = db.DeltaHAir;
            DataModel.DeltaHMat = db.DeltaHMat;
            DataModel.Rho0 = db.Rho0;
            DataModel.SelectedConvertFrom = db.SelectedConvertFrom;
            DataModel.SelectedConvertTo = db.SelectedConvertTo;

            BindingContext = DataModel;

            base.OnAppearing();
        }

        private async void Clear_Clicked(object sender, EventArgs e)
        {
            DataModel.D010Log = 0.0;
            DataModel.S = 0.0;
            DataModel.S0 = 150.0;
            DataModel.Ys = 0.0;
            DataModel.Hrr = 1000;
            DataModel.Pod = 8700;
            DataModel.DeltaHAir = 3000;
            DataModel.DeltaHMat = 14000;
            DataModel.Rho0 = 1.205;
            DataModel.SelectedConvertFrom = SmokeUnits.SmokePotentialArgos;
            DataModel.SelectedConvertTo = SmokeUnits.SootYield;

            var db = await _connection.Table<SmokeUnitTable>().FirstAsync();

            db.D010Log = DataModel.D010Log;
            db.S = DataModel.S;
            db.S0 = DataModel.S0;
            db.Ys = DataModel.Ys;
            db.Hrr = DataModel.Hrr;
            db.Pod = DataModel.Pod;
            db.DeltaHAir = DataModel.DeltaHAir;
            db.DeltaHMat = DataModel.DeltaHMat;
            db.Rho0 = DataModel.Rho0;
            db.SelectedConvertFrom = DataModel.SelectedConvertFrom;
            db.SelectedConvertTo = DataModel.SelectedConvertTo;

            await _connection.UpdateAsync(db);
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        // Afhængigt af hvilken kombination af From/To skal kun bestemte Cells under "Required Known Conditions" være synlige.
        private void CustomPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DataModel == null)
                return;

            if (DataModel.SelectedConvertFrom == null)
                return;

            if (DataModel.SelectedConvertTo == null)
                return;

            TableSection selectedDataCells = new TableSection();

            if (DataModel.SelectedConvertTo == SmokeUnits.SmokePotentialArgos)
            {
                selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Density));
                selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hair));

                if (DataModel.SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Ys));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Pod));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.S));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hrr));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.D010log));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                }
            }

            if (DataModel.SelectedConvertTo == SmokeUnits.SmokePotentialBurnedFuel)
            {
                if (DataModel.SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Pod));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Ys));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.S));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hrr));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.S0));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hair));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Density));
                }
            }

            if (DataModel.SelectedConvertTo == SmokeUnits.SootYield)
            {
                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.D010log));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Pod));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.S));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hrr));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Pod));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.S0));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hair));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Density));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Pod));
                }
            }

            if (DataModel.SelectedConvertTo == SmokeUnits.SmokeProduction)
            {
                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.D010log));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hrr));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Ys));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hrr));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Pod));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hmat));
                }

                if (DataModel.SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.S0));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hrr));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Hair));
                    selectedDataCells.Add(_allDataCells.First(a => a.ClassId == RequiredKnownConditions.Density));
                }
            }

            tableView.Root[1].Clear();

            foreach (var cell in selectedDataCells.OrderBy(a => (a as DataEntryCell).Label))
            {
                tableView.Root[1].Add(cell);
            }
        }

        private async void Calculate_Clicked(object sender, EventArgs e)
        {
            var db = await _connection.Table<SmokeUnitTable>().FirstAsync();

            db.D010Log = DataModel.D010Log;
            db.S = DataModel.S;
            db.S0 = DataModel.S0;
            db.Ys = DataModel.Ys;
            db.Hrr = DataModel.Hrr;
            db.Pod = DataModel.Pod;
            db.DeltaHAir = DataModel.DeltaHAir;
            db.DeltaHMat = DataModel.DeltaHMat;
            db.Rho0 = DataModel.Rho0;
            db.SelectedConvertFrom = DataModel.SelectedConvertFrom;
            db.SelectedConvertTo = DataModel.SelectedConvertTo;

            await _connection.UpdateAsync(db);
                
            var result = DataModel.Calculate();
            
            await Navigation.PushAsync(new ResultPage(result));
        }
    }
}