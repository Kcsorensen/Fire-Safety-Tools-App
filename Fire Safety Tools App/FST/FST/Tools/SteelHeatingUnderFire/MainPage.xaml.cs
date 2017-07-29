using FST.Persistance;
using SQLite;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.SteelHeatingUnderFire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;

        public DataModel DataModel { get; set; }

        public MainPage()
        {
            InitializeComponent();

            DataModel = new DataModel();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            await _connection.CreateTableAsync<SteelHeatingTable>();

            if (await _connection.Table<SteelHeatingTable>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new SteelHeatingTable()
                {
                    SimulationTime = 2,
                    SteelSectionFactor = 200,
                    SteelDensity = 7850,
                    SteelSpecificHeat = 600,
                    SteelEmissivity = 0.6,
                    SelectedFireCurveType = FireCurveTypes.ISO834,
                    HeatTransferCoeffficient = 25,
                    IsSteelProtected = true,
                    IsoThickness = 0.05,
                    IsoThermalConductivity = 0.2,
                    IsoDensity = 150,
                    IsoSpecificHeat = 1200,
                    ShowDetailedDataTable = true
                });
            }

            var db = await _connection.Table<SteelHeatingTable>().FirstAsync();

            // Overfører værdier fra db til DataModel
            DataModel.SimulationTime = db.SimulationTime;
            DataModel.SteelSectionFactor = db.SteelSectionFactor;
            DataModel.SteelDensity = db.SteelDensity;
            DataModel.SteelSpecificHeat = db.SteelSpecificHeat;
            DataModel.SteelEmissivity = db.SteelEmissivity;
            DataModel.SelectedFireCurveType = db.SelectedFireCurveType;
            DataModel.HeatTransferCoeffficient = db.HeatTransferCoeffficient;
            DataModel.IsSteelProtected = db.IsSteelProtected;
            DataModel.IsoThickness = db.IsoThickness;
            DataModel.IsoThermalConductivity = db.IsoThermalConductivity;
            DataModel.IsoDensity = db.IsoDensity;
            DataModel.IsoSpecificHeat = db.IsoSpecificHeat;
            DataModel.ShowDetailedDataTable = db.ShowDetailedDataTable;

            BindingContext = DataModel;

            base.OnAppearing();
        }

        private async void Clear_Clicked(object sender, EventArgs e)
        {
            DataModel.SimulationTime = 2;
            DataModel.SteelSectionFactor = 200;
            DataModel.SteelDensity = 7850;
            DataModel.SteelSpecificHeat = 600;
            DataModel.SteelEmissivity = 0.6;
            DataModel.SelectedFireCurveType = FireCurveTypes.ISO834;
            DataModel.HeatTransferCoeffficient = 25;
            DataModel.IsSteelProtected = true;
            DataModel.IsoThickness = 0.05;
            DataModel.IsoThermalConductivity = 0.2;
            DataModel.IsoDensity = 150;
            DataModel.IsoSpecificHeat = 1200;
            DataModel.ShowDetailedDataTable = true;

            var db = await _connection.Table<SteelHeatingTable>().FirstAsync();

            // Overfører værdier fra db til DataModel
            db.SimulationTime = DataModel.SimulationTime;
            db.SteelSectionFactor = DataModel.SteelSectionFactor;
            db.SteelDensity = DataModel.SteelDensity;
            db.SteelSpecificHeat = DataModel.SteelSpecificHeat;
            db.SteelEmissivity = DataModel.SteelEmissivity;
            db.SelectedFireCurveType = DataModel.SelectedFireCurveType;
            db.HeatTransferCoeffficient = DataModel.HeatTransferCoeffficient;
            db.IsSteelProtected = DataModel.IsSteelProtected;
            db.IsoThickness = DataModel.IsoThickness;
            db.IsoThermalConductivity = DataModel.IsoThermalConductivity;
            db.IsoDensity = DataModel.IsoDensity;
            db.IsoSpecificHeat = DataModel.IsoSpecificHeat;
            db.ShowDetailedDataTable = DataModel.ShowDetailedDataTable;

            await _connection.UpdateAsync(db);
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private async void Calculate_Clicked(object sender, EventArgs e)
        {
            var db = await _connection.Table<SteelHeatingTable>().FirstAsync();

            // Overfører værdier fra db til DataModel
            db.SimulationTime = DataModel.SimulationTime;
            db.SteelSectionFactor = DataModel.SteelSectionFactor;
            db.SteelDensity = DataModel.SteelDensity;
            db.SteelSpecificHeat = DataModel.SteelSpecificHeat;
            db.SteelEmissivity = DataModel.SteelEmissivity;
            db.SelectedFireCurveType = DataModel.SelectedFireCurveType;
            db.HeatTransferCoeffficient = DataModel.HeatTransferCoeffficient;
            db.IsSteelProtected = DataModel.IsSteelProtected;
            db.IsoThickness = DataModel.IsoThickness;
            db.IsoThermalConductivity = DataModel.IsoThermalConductivity;
            db.IsoDensity = DataModel.IsoDensity;
            db.IsoSpecificHeat = DataModel.IsoSpecificHeat;
            db.ShowDetailedDataTable = DataModel.ShowDetailedDataTable;

            await _connection.UpdateAsync(db);

            activityIndicator.IsRunning = true;

            await DataModel.UpdateFireCurvesAsync();

            activityIndicator.IsRunning = false;

            var lineSeries = await DataModel.GetLinesSeriesForPlotModelAsync();

            await Navigation.PushAsync(new ResultGraphPage(lineSeries, DataModel.ShowDetailedDataTable));
        }
    }
}