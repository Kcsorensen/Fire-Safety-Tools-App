using FST.Models;
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
                if (value != _selectedConvertFrom && value != null)
                    populateOppesiteConvertList(value);

                SetValue(ref _selectedConvertFrom, value);
            }
        }
        public string SelectedConvertTo
        {
            get { return _selectedConvertTo; }
            set
            {
                if (value != _selectedConvertTo && value != null)
                    populateOppesiteConvertList(value);

                SetValue(ref _selectedConvertTo, value);
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

            SelectedConvertFrom = SmokeUnits.SmokePotentialArgos;
            SelectedConvertTo = SmokeUnits.SootYield;
        }

        private void populateOppesiteConvertList(string value, [CallerMemberName] string updatedSelected = null)
        {
            if (updatedSelected == "SelectedConvertFrom")
            {
                if (SmokeUnits.List.Any(a => a == value))
                    SortedListConvertTo.Remove(value);

                if (SmokeUnits.List.Any(a => a == _selectedConvertFrom))
                    SortedListConvertTo.Add(_selectedConvertFrom);

                var sorted = SortedListConvertTo.OrderBy(a => a).ToList();

                var selectedConvertTo = _selectedConvertTo;

                if (_selectedConvertFrom != null)
                {
                    int correctIndex = sorted.BinarySearch(_selectedConvertFrom);

                    if (correctIndex >= 0)
                        SortedListConvertTo.Move(SortedListConvertTo.Count - 1, correctIndex);
                }

                // TODO: Et hack for at undgå at SelectedConvertTo bliver null efter at SortedListConvertFrom bliver alfabetisk sorteret.
                _selectedConvertTo = selectedConvertTo;
                SelectedConvertTo = selectedConvertTo;
            }

            if (updatedSelected == "SelectedConvertTo")
            {
                if (SmokeUnits.List.Any(a => a == value))
                    SortedListConvertFrom.Remove(value);

                if (SmokeUnits.List.Any(a => a == _selectedConvertTo))
                    SortedListConvertFrom.Add(_selectedConvertTo);

                var sorted = SortedListConvertFrom.OrderBy(a => a).ToList();

                var selectedConvertFrom = _selectedConvertFrom;

                if (_selectedConvertTo != null)
                {
                    int correctIndex = sorted.BinarySearch(_selectedConvertTo);

                    if (correctIndex >= 0)
                        SortedListConvertFrom.Move(SortedListConvertFrom.Count - 1, correctIndex);
                }

                // TODO: Et hack for at undgå at SelectedConvertFrom bliver null efter at SortedListConvertTo bliver alfabetisk sorteret.
                _selectedConvertFrom = selectedConvertFrom;
                SelectedConvertFrom = selectedConvertFrom;
            }
        }
    }
}
