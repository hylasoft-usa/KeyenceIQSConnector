using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Dto
{
  public class KeyenceMessage : IKeyenceMessage
  {
    protected const string DefaultUom = "mm";

    protected readonly Collection<IKeyenceMessageSegment> _messageSegments;

    public KeyenceMessage(string cameraName, string cameraSpec, int sequence)
    {
      var segments = new List<IKeyenceMessageSegment>
      {
        new KeyenceHeader(cameraName, cameraSpec, sequence),
        new KeyenceBody(),
        new KeyenceFooter()
      };

      _messageSegments = new Collection<IKeyenceMessageSegment>(segments);
    }

    public void AddSnapshot(string name, double length, double width, double xoffset, double yoffset, string uom = DefaultUom)
    {
      foreach(var body in GetMessageBodies())
        body.AddSnapshot(name, length, width, xoffset, yoffset, uom);
    }

    protected IKeyenceMessageBody[] GetMessageBodies()
    {
      return _messageSegments == null
        ? new IKeyenceMessageBody[0]
        : _messageSegments
          .OfType<IKeyenceMessageBody>()
          .Where(seg => seg != null)
          .ToArray();
    }

    public string ToCharStream()
    {
      if (_messageSegments == null)
        return string.Empty;

      var lines = _messageSegments.SelectMany(seg => seg.GetLines());
      return string.Join(Environment.NewLine, lines.Select(line => line.ToString()));
    }
  }
}
