﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Models;

namespace Xamarin.Tools.CheckMeshSize
{
    public class DataBase : BaseModel
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
    }
}