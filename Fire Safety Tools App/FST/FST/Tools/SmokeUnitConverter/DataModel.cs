﻿using FST.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FST.Tools.SmokeUnitConverter
{
    public class DataModel : BaseModel
    {
        #region private fields
        private double _d010Log;
        private double _s;
        private double _s0;
        private double _ys;
        private double _hrr;
        private double _pod;
        private double _deltaHAir;
        private double _deltaHMat;
        private double _rho0;
        private string _selectedConvertFrom;
        private string _selectedConvertTo;
        private ObservableCollection<string> _sortedListConvertFrom;
        private ObservableCollection<string> _sortedListConvertTo;

        private bool _applyChangesToSelectedFrom;
        private bool _applyChangesToSelectedTo;
        #endregion

        #region public properties

        public double D010Log
        {
            get { return _d010Log; }
            set { SetValue(ref _d010Log, value); }
        }
        public double S
        {
            get { return _s; }
            set { SetValue(ref _s, value); }
        }
        public double S0
        {
            get { return _s0; }
            set { SetValue(ref _s0, value); }
        }
        public double Ys
        {
            get { return _ys; }
            set { SetValue(ref _ys, value); }
        }
        public double Hrr
        {
            get { return _hrr; }
            set { SetValue(ref _hrr, value); }
        }
        public double Pod
        {
            get { return _pod; }
            set { SetValue(ref _pod, value); }
        }
        public double DeltaHAir
        {
            get { return _deltaHAir; }
            set { SetValue(ref _deltaHAir, value); }
        }
        public double DeltaHMat
        {
            get { return _deltaHMat; }
            set { SetValue(ref _deltaHMat, value); }
        }
        public double Rho0
        {
            get { return _rho0; }
            set { SetValue(ref _rho0, value); }
        }
        public string SelectedConvertFrom
        {
            get { return _selectedConvertFrom; }
            set
            {
                if (_applyChangesToSelectedFrom == true)
                {
                    if (value != null)
                        if (value != _selectedConvertFrom)
                            populateOppesiteConvertList(value);

                    SetValue(ref _selectedConvertFrom, value);
                }
            }
        }
        public string SelectedConvertTo
        {
            get { return _selectedConvertTo; }
            set
            {
                if (_applyChangesToSelectedTo == true)
                {
                    if(value != null)
                        if (value != _selectedConvertTo)
                            populateOppesiteConvertList(value);

                    SetValue(ref _selectedConvertTo, value);
                }
            }
        }
        public ObservableCollection<string> SortedListConvertFrom
        {
            get { return _sortedListConvertFrom; }
            set { SetValue(ref _sortedListConvertFrom, value); }
        }
        public ObservableCollection<string> SortedListConvertTo
        {
            get { return _sortedListConvertTo; }
            set { SetValue(ref _sortedListConvertTo, value); }
        }
        #endregion

        public DataModel()
        {
            SortedListConvertFrom = new ObservableCollection<string>();
            SortedListConvertTo = new ObservableCollection<string>();

            foreach (var smokeUnit in SmokeUnits.List.OrderBy(a => a))
            {
                SortedListConvertFrom.Add(smokeUnit);
                SortedListConvertTo.Add(smokeUnit);
            }

            // TODO: SmokeUnitConverter, Et hack for at få sammenspillet mellem SelectedConvertFrom og SelectedConvertTo til at virke.
            _applyChangesToSelectedFrom = true;
            _applyChangesToSelectedTo = true;

            SelectedConvertFrom = SmokeUnits.SmokePotentialArgos;
            SelectedConvertTo = SmokeUnits.SootYield;
        }

        private void populateOppesiteConvertList(string value, [CallerMemberName] string updatedSelected = null)
        {
            if (updatedSelected == "SelectedConvertFrom")
            {
                // Et hack
                _applyChangesToSelectedTo = false;

                if (SmokeUnits.List.Any(a => a == value))
                    SortedListConvertTo.Remove(value);

                if (SmokeUnits.List.Any(a => a == _selectedConvertFrom))
                    SortedListConvertTo.Add(_selectedConvertFrom);

                // Et hack
                _applyChangesToSelectedTo = true;

                var sorted = SortedListConvertTo.OrderBy(a => a).ToList();

                var selectedConvertTo = _selectedConvertTo;

                if (_selectedConvertFrom != null)
                {
                    int correctIndex = sorted.BinarySearch(_selectedConvertFrom);

                    if (correctIndex >= 0)
                        SortedListConvertTo.Move(SortedListConvertTo.Count - 1, correctIndex);
                }

                // TODO: SmokeUnitConverter, Et hack for at undgå at SelectedConvertTo bliver null efter at SortedListConvertFrom bliver alfabetisk sorteret.
                _selectedConvertTo = selectedConvertTo;
                SelectedConvertTo = selectedConvertTo;

            }

            if (updatedSelected == "SelectedConvertTo")
            {
                // Et hack
                _applyChangesToSelectedFrom = false;

                if (SmokeUnits.List.Any(a => a == value))
                    SortedListConvertFrom.Remove(value);

                if (SmokeUnits.List.Any(a => a == _selectedConvertTo))
                    SortedListConvertFrom.Add(_selectedConvertTo);

                // Et hack
                _applyChangesToSelectedFrom = true;

                var sorted = SortedListConvertFrom.OrderBy(a => a).ToList();

                var selectedConvertFrom = _selectedConvertFrom;

                if (_selectedConvertTo != null)
                {
                    int correctIndex = sorted.BinarySearch(_selectedConvertTo);

                    if (correctIndex >= 0)
                        SortedListConvertFrom.Move(SortedListConvertFrom.Count - 1, correctIndex);
                }

                // TODO: SmokeUnitConverter, Et hack for at undgå at SelectedConvertFrom bliver null efter at SortedListConvertTo bliver alfabetisk sorteret.
                _selectedConvertFrom = selectedConvertFrom;
                SelectedConvertFrom = selectedConvertFrom;

            }
        }

        public double Calculate()
        {
            double result = 0;

            if (SelectedConvertTo == SmokeUnits.SmokePotentialArgos)
            {
                if (SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    result = Math.Round(Pod * Ys * (10.0 / Math.Log(10.0)) * (DeltaHAir / DeltaHMat) * Rho0, 1);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    result = Math.Round((S / Hrr) * DeltaHAir * Rho0, 1);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    result = Math.Round((1000.0 * DeltaHAir * D010Log * Rho0) / DeltaHMat, 1);
                }
            }

            if (SelectedConvertTo == SmokeUnits.SmokePotentialBurnedFuel)
            {
                if (SelectedConvertFrom == SmokeUnits.SootYield)
                {
                    result = Math.Round(( 10.0 * Pod * Ys ) / (1000.0 * Math.Log(10.0)), 2);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    result = Math.Round((S * DeltaHMat) / (1000.0 * Hrr), 2);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    result = Math.Round((S0 * DeltaHMat) / (1000.0 * Rho0 * DeltaHAir), 2);
                }
            }

            if (SelectedConvertTo == SmokeUnits.SootYield)
            {
                if (SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    result = Math.Round((1000 * D010Log * Math.Log(10.0)) / (10.0 * Pod), 4);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokeProduction)
                {
                    result = Math.Round((S * Math.Log(10.0) * DeltaHMat) / (10.0 * Hrr * Pod), 4);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokePotentialArgos)
                {
                    result = Math.Round((S0 * Math.Log(10.0) * DeltaHMat) / (10.0 * Pod * DeltaHAir * Rho0), 4);
                }
            }

            if (SelectedConvertTo == SmokeUnits.SmokeProduction)
            {
                if (SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    result = Math.Round((1000.0 * D010Log * Hrr) / DeltaHMat, 2);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    result = Math.Round((Ys * 10.0 * Hrr * Pod) / (Math.Log(10.0) * DeltaHMat), 2);
                }

                if (SelectedConvertFrom == SmokeUnits.SmokePotentialBurnedFuel)
                {
                    result = Math.Round((S0 * Hrr) / (DeltaHAir * Rho0), 2);
                }
            }
                return result;
        }
    }
}
