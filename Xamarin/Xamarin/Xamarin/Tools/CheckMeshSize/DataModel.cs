using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Models;

namespace Xamarin.Tools.CheckMeshSize
{
    public class DataModel : BaseModel
    {

        #region private fields
        private double _heatReleaseRate;
        private double _density;
        private double _specificHeat;
        private double _gravity;
        private double _cellSize;
        private double _temperature;
        #endregion

        #region public properties

        public double HeatReleaseRate
        {
            get { return _heatReleaseRate; }
            set { SetValue(ref _heatReleaseRate, value); }
        }

        public double Density
        {
            get { return _density; }
            set { SetValue(ref _density, value); }
        }

        public double SpecificHeat
        {
            get { return _specificHeat; }
            set { SetValue(ref _specificHeat, value); }
        }

        public double Gravity
        {
            get { return _gravity; }
            set { SetValue(ref _gravity, value); }
        }

        public double CellSize
        {
            get { return _cellSize; }
            set { SetValue(ref _cellSize, value); }
        }

        public double Temperature
        {
            get { return _temperature; }
            set { SetValue(ref _temperature, value); }
        }

        public double FireDiameter { get; set; }
        public double Ratio { get; set; }
        #endregion

        public DataModel()
        {
            SaveAllValuesAsync();
        }

        public void UpdateValuesAsync()
        {
            // Set Initial Values
            if (Density <= 0.0)
                Application.Current.Properties["Density_CheckMeshSize"] = 1.205;
            if (SpecificHeat <= 0.0)
                Application.Current.Properties["SpecificHeat_CheckMeshSize"] = 1.0;
            if (Gravity <= 0)
                Application.Current.Properties["Gravity_CheckMeshSize"] = 9.82;
            if (Temperature <= 0)
                Application.Current.Properties["Temperature_CheckMeshSize"] = 20.0;
            if (Application.Current.Properties.Any(a => a.Key != "HeatReleaseRate_CheckMeshSize"))
                Application.Current.Properties["HeatReleaseRate_CheckMeshSize"] = 0.0;
            if (Application.Current.Properties.Any(a => a.Key != "CellSize_CheckMeshSize"))
                Application.Current.Properties["CellSize_CheckMeshSize"] = 0.0;

            // Load Values
            HeatReleaseRate = (double)Application.Current.Properties["HeatReleaseRate_CheckMeshSize"];
            Density = (double)Application.Current.Properties["Density_CheckMeshSize"];
            SpecificHeat = (double)Application.Current.Properties["SpecificHeat_CheckMeshSize"];
            Gravity = (double)Application.Current.Properties["Gravity_CheckMeshSize"];
            Temperature = (double)Application.Current.Properties["Temperature_CheckMeshSize"];
            CellSize = (double)Application.Current.Properties["CellSize_CheckMeshSize"];
        }

        public async Task CalculateAsync()
        {
            await Task.Run(() => 
            {
                FireDiameter = Math.Pow((HeatReleaseRate / (Density * SpecificHeat * (273 + Temperature) * Math.Sqrt(Gravity))), 2.0 / 5.0);
                Ratio = FireDiameter / CellSize;
            });
        }

        public async Task SaveAllValuesAsync()
        {
            await Task.Run(() =>
            {
                Application.Current.Properties["HeatReleaseRate_CheckMeshSize"] = HeatReleaseRate;
                Application.Current.Properties["Density_CheckMeshSize"] = Density;
                Application.Current.Properties["SpecificHeat_CheckMeshSize"] = SpecificHeat;
                Application.Current.Properties["Gravity_CheckMeshSize"] = Gravity;
                Application.Current.Properties["CellSize_CheckMeshSize"] = CellSize;
                Application.Current.Properties["Temperature_CheckMeshSize"] = Temperature;
                Application.Current.Properties["FireDiameter_CheckMeshSize"] = FireDiameter;
                Application.Current.Properties["Ratio_CheckMeshSize"] = Ratio;
            });
        }

        public async Task ClearAllValuesAsync()
        {
            await Task.Run(() =>
            {
                HeatReleaseRate = 0.0;
                Density = 1.205;
                SpecificHeat = 1.0;
                Gravity = 9.82;
                Temperature = 20.0;
                CellSize = 0.0;

                Application.Current.Properties["HeatReleaseRate_CheckMeshSize"] = HeatReleaseRate;
                Application.Current.Properties["Density_CheckMeshSize"] = Density;
                Application.Current.Properties["SpecificHeat_CheckMeshSize"] = SpecificHeat;
                Application.Current.Properties["Gravity_CheckMeshSize"] = Gravity;
                Application.Current.Properties["CellSize_CheckMeshSize"] = CellSize;
                Application.Current.Properties["Temperature_CheckMeshSize"] = Temperature;
            });
        }

    }
}
