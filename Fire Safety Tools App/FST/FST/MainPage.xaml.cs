using System;
using Xamarin.Forms;

namespace FST
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CheckMeshSize_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FST.Tools.CheckMeshSize.MainPage());
        }
    }
}
