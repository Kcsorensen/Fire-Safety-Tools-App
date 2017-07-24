using FST.Persistance;
using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Tools.CheckMeshSize
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;

        public double FireDiameter { get; set; }
        public double Ratio { get; set; }
        public double CellSize { get; set; }
        public string FireDiameterResult { get; set; }
        public string RatioResult { get; set; }
        public string Conclusion { get; set; }
        public string GeneralInformation { get; set; }

        public ResultPage()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            var db = await _connection.Table<CheckMeshSizeTable>().FirstAsync();

            FireDiameter = db.FireDiameter;
            Ratio = db.Ratio;
            CellSize = db.CellSize;

            FireDiameterResult = string.Empty;
            RatioResult = string.Empty;
            Conclusion = string.Empty;

            FireDiameterResult = string.Format("The characteristic fire diameter, D*, is {0}", Math.Round(FireDiameter, 2));
            RatioResult = string.Format("With a cell size of {0}, the ratio between the characteristic fire diameter and the cell size, D*/dx, is {1}", CellSize, Math.Round(Ratio, 2));

            if (Ratio < 4)
            {
                Conclusion = string.Format("Because the ratio, D*/dx, is less than 4 the mesh used is considered to be to coarse to give reliable result in FDS");
            }
            else if (Ratio >= 4 && Ratio < 10)
            {
                Conclusion = string.Format("Because the ratio, D*/dx, is larger than 4 and less than 10, the mesh used is considered an acceptable coarse grid. It might be wise to use a more moderate grid near the fire.");
            }
            else if (Ratio >= 10 && Ratio < 16)
            {
                Conclusion = string.Format("Because the ratio, D*/dx, is larger than 10 and less than 16, the mesh used is considered a moderate grid. This grid size generally give good result when used near the fire.");
            }
            else
            {
                Conclusion = string.Format("Because the ratio, D*/dx, is equal or larger than 16, the mesh used is considered a fine grid. Unless used for very specific models, this grid size is most likely to fine. It might be wise to investige if the point of diminishing returns has been reached. Instead use a more coarse grid to utilize a greater number of simulations in less time for statistic purposes.");
            }

            parentStackLayout.BindingContext = this;

            base.OnAppearing();
        }
    }
}