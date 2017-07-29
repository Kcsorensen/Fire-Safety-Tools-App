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
                populateOppesiteConvertList(value);

                SetValue(ref _selectedConvertFrom, value);
            }
        }
        public string SelectedConvertTo
        {
            get { return _selectedConvertTo; }
            set
            {
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

        private void populateOppesiteConvertList(string value, [CallerMemberName] string updatedSelected = null)
        {
            if (updatedSelected == "SelectedConvertFrom")
            {
                if (SortedListConvertTo != null)
                {
                    SortedListConvertTo.Remove(_selectedConvertFrom);
                    SortedListConvertTo.Add(value);
                }

                if (SortedListConvertTo == null)
                {
                    SortedListConvertTo = new ObservableCollection<string>();

                    foreach (var smokeUnit in SmokeUnits.List)
                    {
                        SortedListConvertTo.Add(smokeUnit);
                    }

                    SortedListConvertTo.Remove(value);
                }
            }

            if (updatedSelected == "SelectedConvertTo")
            {
                if (SortedListConvertFrom != null)
                {
                    SortedListConvertFrom.Remove(_selectedConvertTo);
                    SortedListConvertFrom.Add(value);
                }

                if (SortedListConvertFrom == null)
                {
                    SortedListConvertFrom = new ObservableCollection<string>();

                    foreach (var smokeUnit in SmokeUnits.List)
                    {
                        SortedListConvertFrom.Add(smokeUnit);
                    }

                    SortedListConvertFrom.Remove(value);
                }
            }
        }
    }
}
