using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Dto
{
  public class KeyenceHeader : IKeyenceMessageSegment
  {
    public string CameraName { get; private set; }

    public string CameraSpec { get; private set; }

    public DateTime MessageDate { get; private set; }

    public int Sequence { get; private set; }

    public KeyenceHeader(string cameraName, string cameraSpec, int sequence)
    {
      Sequence = sequence;
      CameraSpec = cameraSpec;
      CameraName = cameraName;
      MessageDate = DateTime.Now;
    }

    public Collection<KeyenceLine> GetLines()
    {
      var lines = new List<KeyenceLine>
      {
        new KeyenceLine("SE", CameraName, CameraSpec, "26"),
        new KeyenceLine("DA", "Std Part Top", "E7"),
        new KeyenceLine("LO", "A4"),
        new KeyenceLine("SC", Sequence.ToString("D04"), "6A"),
        new KeyenceLine("CH", "94")
      };

      return new Collection<KeyenceLine>(lines);
    }
  }
}
