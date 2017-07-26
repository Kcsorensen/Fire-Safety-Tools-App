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
                    FireCurveType = 0,
                    HeatTransferCoeffficient = 25,
                    IsSteelProtected = true,
                    IsoThickness = 0.05,
                    IsoThermalConductivity = 0.2,
                    IsoDensity = 150,
                    IsoSpecificHeat = 1200
                });
            }

            var db = await _connection.Table<SteelHeatingTable>().FirstAsync();

            // Overfører værdier fra db til DataModel
            DataModel.SimulationTime = db.SimulationTime;
            DataModel.SteelSectionFactor = db.SteelSectionFactor;
            DataModel.SteelDensity = db.SteelDensity;
            DataModel.SteelSpecificHeat = db.SteelSpecificHeat;
            DataModel.SteelEmissivity = db.SteelEmissivity;
            DataModel.FireCurveType = db.FireCurveType;
            DataModel.HeatTransferCoeffficient = db.HeatTransferCoeffficient;
            DataModel.IsSteelProtected = db.IsSteelProtected;
            DataModel.IsoThickness = db.IsoThickness;
            DataModel.IsoThermalConductivity = db.IsoThermalConductivity;
            DataModel.IsoDensity = db.IsoDensity;
            DataModel.IsoSpecificHeat = db.IsoSpecificHeat;

            BindingContext = DataModel;

            base.OnAppearing();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {

        }

        private void Info_Clicked(object sender, EventArgs e)
        {

        }

        private async Task Calculate_Clicked(object sender, EventArgs e)
        {
            await DataModel.UpdateFireCurveAsync();

            var lineSeries = await DataModel.GetLinesSeriesForPlotModelAsync();

            await Navigation.PushAsync(new ResultPage(lineSeries));
        }
    }
}