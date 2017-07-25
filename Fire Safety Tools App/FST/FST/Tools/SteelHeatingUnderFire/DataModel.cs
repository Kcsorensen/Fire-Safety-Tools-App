using FST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.Tools.SteelHeatingUnderFire
{
    public class DataModel : BaseModel
    {
        private double _steelSectionFactor;

        public double SteelSectionFactor
        {
            get { return _steelSectionFactor; }
            set { SetValue(ref _steelSectionFactor, value); }
        }
    }
}
