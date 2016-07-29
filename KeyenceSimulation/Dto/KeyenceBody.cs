using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Dto
{
  public class KeyenceBody : IKeyenceMessageBody
  {
    protected const string Prefix = "IT";
    protected const string Confirmation = "OK";

    protected readonly List<KeyenceSnapshot> _snapshots;
    protected readonly Random _random;

    public KeyenceBody()
    {
      _snapshots = new List<KeyenceSnapshot>();
      _random = new Random();
    }

    public void AddSnapshot(string name, double length, double width, double xoffset, double yoffset, string uom)
    {
      var sequence = _snapshots.Count + 1;
      _snapshots.Add(new KeyenceSnapshot(sequence, name, length, width, xoffset, yoffset, uom));
    }

    public Collection<KeyenceLine> GetLines()
    {
      var lines = _snapshots
        .Where(snap => snap != null)
        .Select(ToLine)
        .ToList();

      return new Collection<KeyenceLine>(lines);
    }

    protected KeyenceLine ToLine(KeyenceSnapshot snapshot)
    {
      if (snapshot == null)
        return null;

      var length = snapshot.Length.ToString("F3");
      var width = snapshot.Width.ToString("F3");
      var uom = snapshot.Uom;
      var name = snapshot.Name;
      var x = snapshot.Xoffset.ToString("F3");
      var y = snapshot.Yoffset.ToString("F3");

      var end = _random.Next(18, 245).ToString("X02");
      return new KeyenceLine(Prefix, length, uom, name, width, x, y, Confirmation, end);
    }
  }
}
