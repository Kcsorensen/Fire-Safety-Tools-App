using SQLite;
using System;
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

            _connection.CreateTableAsync<CheckMeshSizeTable>();

            var table = _connection.Table<CheckMeshSizeTable>().ToListAsync();

            //DataModel.UpdateValuesAsync();
        }

        protected async override void OnAppearing()
        {
            //await DataModel.LoadDataBaseAsync();

            await DataModel.LoadDataBaseAsync();

            BindingContext = DataModel.DB;

            base.OnAppearing();
        }

        private void Calculate_Clicked(object sender, EventArgs e)
        {
            //// Call calculate method to get values for fireDiameter and ratio
            //await DataModel.CalculateAsync();

            //if (double.IsNaN(DataModel.Ratio))
            //{
            //    await DisplayAlert("Result", "Value has to be numeric", "OK");
            //}
            //else if (double.IsInfinity(DataModel.Ratio))
            //{
            //    await DisplayAlert("Result", "Cell Size or Ambient Conditions cannot be zero", "OK");
            //}
            //else
            //{
            //    // Save all updated values to the Application.Current.Property DB
            //    await DataModel.SaveAllValuesAsync();



            //    await Navigation.PushAsync(new ResultPage());
            //}


        }

        

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            //await DataModel.ClearAllValuesAsync();
        }
    }
}

