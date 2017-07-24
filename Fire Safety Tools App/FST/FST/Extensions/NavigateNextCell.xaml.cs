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
    public partial class NavigateNextCell : ViewCell
    {
        public static readonly BindableProperty LabelProperty =
            BindableProperty.Create("Label", typeof(string), typeof(ViewCell), null);

        public static readonly BindableProperty TappedProperty =
            BindableProperty.Create("Tapped", typeof(string), typeof(ViewCell), null, BindingMode.Default, null, propertyChanged: (bindable, oldValue, newValue) =>
            {
                ((NavigateNextCell)bindable).OnTapped();
            });

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public NavigateNextCell()
        {
            InitializeComponent();

            BindingContext = this;
        }
    }
}