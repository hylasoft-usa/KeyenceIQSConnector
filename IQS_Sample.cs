using System;
using System.Collections.Generic;
using System.Text;

namespace Keyence2IQS
{
    /// <summary>
    /// The IQS_Table is the object containing all necessary information for inserting a measurement result into an IQS Table.
    /// This is the final result of the main method, not counting the string representation.
    /// </summary>
    public class IQS_Sample
    {
        ///<summary>An alternative to the NewLine characters.</summary>
        String NL = Environment.NewLine;

        /// <summary>An alternative to the insert tab character.</summary>
        String T = "\t";

        ///<summary>This holds the part ID that needs to already be defined in ProFicient MI.</summary>
        public String PartNumber { get; set; }

        /// <summary>This holds part group defined in ProFicient MI associated to the part ID being scanned.</summary>
        public String PartGroup { get; set; }

        ///<summary>This holds the name of machine where the parts were created and must exist in ProFicient MI.</summary>
        public String ProcessNumber { get; set; }

        ///<summary>TBD.</summary>
        public String SerialCounter { get; set; }

        ///<summary>This olds the Batch ID for the subgroup of parts.</summary>
        public String BatchNumber { get; set; }

        ///<summary>This will hold the Lot ID the part being scanned is part of.</summary>
        public String LotID { get; set; }

        ///<summary>TBD.</summary>
        public String Name { get; set; }

        ///<summary>This holds the shift of the operator that created the parts.</summary>
        public String Shift { get; set; }

        ///<summary>TBD.</summary>
        public String ClockNumber { get; set; }

        ///<summary>This holds the ID of the Keyence machine taking the measurements.</summary>
        public String KeyenceAssetNumber { get; set; }

        ///<summary>This is a pre-define list that is configurable with  the Hyla Soft application.</summary>
        public String DataType { get; set; }

        ///<summary>The list of measurements that were done.</summary>
        public List<Test> Tests { get; set; }

        /// <summary>
        /// Constructor: Creates a new Table with the number of test entries equal to TestNo.  it also initializes each test in the array.
        /// </summary>
        /// <param name="TestNo">The Number of tests expected to be included in the measurement result.</param>
        public IQS_Sample()
        {
            Tests = new List<Test>();
        }

        /// <summary>
        /// Converts an IQS_Sample Object into a string.
        /// </summary>
        /// <returns>A string representing an IQS_Table.</returns>
        public override String ToString()
        {
            StringBuilder S = new StringBuilder();
            foreach (Test t in this.Tests)
            {
                S.AppendFormat("{0}{1}", PartGroup, T);
                S.AppendFormat("{0}{1}", PartNumber, T);
                S.AppendFormat("{0}{1}", ProcessNumber, T);
                S.AppendFormat("{0}{1}", BatchNumber, T);
                S.AppendFormat("{0}{1}", Shift, T);
                S.AppendFormat("{0}{1}", ClockNumber, T);
                S.AppendFormat("{0}{1}", KeyenceAssetNumber, T);
                S.AppendFormat("{0}{1}", LotID, T);
                S.AppendFormat("{0}{1}", SerialCounter, T);
                S.AppendFormat("{0}{1}", Name, T);
                S.AppendFormat("{0}{1}", t.Name, T);
                S.AppendFormat("{0}{1}", t.Value, NL);
            }
            return S.ToString();
        }
    }
}
