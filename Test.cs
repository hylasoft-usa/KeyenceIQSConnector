using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyence2IQS
{
    /// <summary>
    /// The Test object is used to keep all information regarding a certain measurement, including name, unit of measurement, spec limits, and value.
    /// </summary>
    public class Test
    {
        ///<summary>The Name of the measurement being done.</summary>
        public String Name { get; set; }
        
        ///<summary>The actual value of this measurement.</summary>
        public double Value { get; set; }

      public double Target { get; set; }
      public double USL { get; set; }
      public double LSL { get; set; }
      public string Unit { get; set; }
      public string Group { get; set; }
    }
}
