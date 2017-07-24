using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FST.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataEntryCell : ViewCell
    {
        public static readonly BindableProperty LabelProperty =
            BindableProperty.Create("Label", typeof(string), typeof(ViewCell));

        public static readonly BindableProperty UnitProperty =
            BindableProperty.Create("Unit", typeof(string), typeof(ViewCell));

        public static readonly BindableProperty ValueProperty =
        BindableProperty.Create("Value", typeof(string), typeof(ViewCell), null, defaultBindingMode: BindingMode.TwoWay, validateValue: null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((DataEntryCell)bindable).OnPropertyChanged();
        });

        //public static readonly BindableProperty ValueProperty =
        //    BindableProperty.Create("Value", typeof(string), typeof(ViewCell));

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create("Placeholder", typeof(string), typeof(ViewCell));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public DataEntryCell()
        {
            InitializeComponent();
        }
    }
}