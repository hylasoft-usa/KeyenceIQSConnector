using System.Collections.Generic;
using System.Collections.ObjectModel;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Dto
{
  public class KeyenceFooter : IKeyenceMessageSegment
  {
    public Collection<KeyenceLine> GetLines()
    {
      var footerLines = new List<KeyenceLine>
      {
        new KeyenceLine("EN", "9C")
      };

      return new Collection<KeyenceLine>(footerLines);
    }
  }
}
