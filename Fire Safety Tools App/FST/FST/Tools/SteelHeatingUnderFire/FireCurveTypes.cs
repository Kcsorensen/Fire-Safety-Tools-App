using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.Tools.SteelHeatingUnderFire
{
    public class FireCurveTypes
    {
        // Singleton
        public static FireCurveTypes Current = new FireCurveTypes();

        public const string ISO834 = "EN 1993-1-2 / ISO 834-1";
        public const string ASTME119 = "ASTM E119";

        public List<string> List { get; set; }

        public FireCurveTypes()
        {
            List = new List<string>()
            {
                ISO834,
                ASTME119
            };
        }
    }
}
