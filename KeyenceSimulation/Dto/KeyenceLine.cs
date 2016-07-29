using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KeyenceSimulation.Dto
{
  public class KeyenceLine
  {
    public Collection<KeyenceLineSegment> Segments { get; private set; }

    public KeyenceLine(string prefix, params string[] segments) : this(BuildSegmentList(prefix, segments))
    {
    }

    public KeyenceLine(IEnumerable<KeyenceLineSegment> segments)
    {
      var segmentList = segments == null
        ? new List<KeyenceLineSegment>()
        : segments.ToList();

      Segments = new Collection<KeyenceLineSegment>(segmentList);
    }

    public override string ToString()
    {
      var line = new StringBuilder();

      var segmentValues = Segments
        .Where(seg => seg != null)
        .Select(seg => seg.Value);

      line.Append(string.Join("\t", segmentValues));

      return line.ToString();
    }

    private static IEnumerable<KeyenceLineSegment> BuildSegmentList(string prefix, IEnumerable<string> segments)
    {
      var segmentList = new List<KeyenceLineSegment>();
      
      if (!string.IsNullOrEmpty(prefix))
        segmentList.Add(new KeyenceLineSegment(prefix));

      if (segments != null)
        segmentList.AddRange(segments.Select(seg => new KeyenceLineSegment(seg)));

      return segmentList;
    }
  }
}
