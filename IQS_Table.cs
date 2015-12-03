using System;
using System.Collections.Generic;
using System.Text;

namespace Keyence2IQS
{
  /// <summary>
  /// The IQS_Table is the object containing all necessary information for inserting a measurement result into an IQS Table.
  /// This is the final result of the main method, not counting the string representation.
  /// </summary>
  public class IQS_Table
  {
    ///<summary>The list of measurements that were done.</summary>
    public List<IQS_Sample> Samples{ get; set; }

    /// <summary>
    /// Constructor: Creates a new Table with the number of test entries equal to TestNo.  it also initializes each test in the array.
    /// </summary>
    /// <param name="TestNo">The Number of tests expected to be included in the measurement result.</param>
    public IQS_Table()
    {
      Samples = new List<IQS_Sample>();
    }

    /// <summary>
    /// Converts an IQS_Table Object into a string.
    /// This method is not necessary for the program, but is useful for checking for bugs.
    /// </summary>
    /// <returns>A string representing an IQS_Table.</returns>
    public override String ToString()
    {
      StringBuilder S = new StringBuilder();
      foreach (IQS_Sample sample in Samples)
      {
        S.AppendFormat("{0}",sample);
      }
      return S.ToString();
    }
  }
}
