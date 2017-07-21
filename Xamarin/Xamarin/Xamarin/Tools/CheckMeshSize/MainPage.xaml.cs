﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Tools.CheckMeshSize
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public DataModel DataModel { get; set; }

        public MainPage()
        {
            DataModel = new DataModel();

            BindingContext = DataModel;

            InitializeComponent();
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
                // Save all updated values to the Application.Current.Property DB
                await DataModel.SaveAllValuesAsync();



                await Navigation.PushAsync(new ResultPage());
            }


        }

        protected override void OnAppearing()
        {
            DataModel.UpdateValuesAsync();

            base.OnAppearing();
        }

        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private async void Clear_Clicked(object sender, EventArgs e)
        {
            await DataModel.ClearAllValuesAsync();


        }
    }
}

