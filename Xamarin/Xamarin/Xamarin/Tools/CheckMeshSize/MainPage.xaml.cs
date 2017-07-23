using SQLite;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Persistance;

namespace Xamarin.Tools.CheckMeshSize
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
            await _connection.CreateTableAsync<CheckMeshSizeTable>();

            if (await _connection.Table<CheckMeshSizeTable>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new CheckMeshSizeTable()
                {
                    Temperature = 20,
                    CellSize = 0.0,
                    Gravity = 9.82,
                    SpecificHeat = 1.0,
                    Density = 1.205,
                    HeatReleaseRate = 0.0,
                    FireDiameter = 0.0,
                    Ratio = 0.0
                });
            }

            var db = await _connection.Table<CheckMeshSizeTable>().FirstAsync();

            // Overfører værdier fra db til DataModel
            DataModel.Temperature = db.Temperature;
            DataModel.CellSize = db.CellSize;
            DataModel.Gravity = db.Gravity;
            DataModel.SpecificHeat = db.SpecificHeat;
            DataModel.Density = db.Density;
            DataModel.HeatReleaseRate= db.HeatReleaseRate;
            DataModel.FireDiameter = db.FireDiameter;
            DataModel.Ratio= db.Ratio;

            BindingContext = DataModel;

            base.OnAppearing();
        }

        private async void Calculate_Clicked(object sender, EventArgs e)
        {
            // Call calculate method to get values for fireDiameter and ratio
            await DataModel.CalculateAsync();

            if (double.IsNaN(DataModel.Ratio))
            {
                await DisplayAlert("Result", "Value has to be numeric", "OK");
            }
            else if (double.IsInfinity(DataModel.Ratio))
            {
                await DisplayAlert("Result", "Cell Size or Ambient Conditions cannot be zero", "OK");
            }
            else
            {
                // Gem alle værdierne i DataModel i SQLite Db
                await UpdateDb();

                await Navigation.PushAsync(new ResultPage());
            }
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private async void Clear_Clicked(object sender, EventArgs e)
        {
            await ClearValues();
        }

        private async Task ClearValues()
        {
            var db = await _connection.Table<CheckMeshSizeTable>().FirstAsync();

            // Reset værdier i SQLite Db
            db.Temperature = 20;
            db.CellSize = 0.0;
            db.Gravity = 9.82;
            db.SpecificHeat = 1.0;
            db.Density = 1.205;
            db.HeatReleaseRate = 0.0;
            db.FireDiameter = 0.0;
            db.Ratio = 0.0;

            await _connection.UpdateAsync(db);

            // Overfører værdier fra db til DataModel
            DataModel.Temperature = db.Temperature;
            DataModel.CellSize = db.CellSize;
            DataModel.Gravity = db.Gravity;
            DataModel.SpecificHeat = db.SpecificHeat;
            DataModel.Density = db.Density;
            DataModel.HeatReleaseRate = db.HeatReleaseRate;
            DataModel.FireDiameter = db.FireDiameter;
            DataModel.Ratio = db.Ratio;
        }

        private async Task UpdateDb()
        {
            var db = await _connection.Table<CheckMeshSizeTable>().FirstAsync();

            db.Temperature = DataModel.Temperature;
            db.CellSize = DataModel.CellSize;
            db.Gravity = DataModel.Gravity;
            db.SpecificHeat = DataModel.SpecificHeat;
            db.Density = DataModel.Density;
            db.HeatReleaseRate = DataModel.HeatReleaseRate;
            db.FireDiameter = DataModel.FireDiameter;
            db.Ratio = DataModel.Ratio;

            await _connection.UpdateAsync(db);
        }
    }
}

