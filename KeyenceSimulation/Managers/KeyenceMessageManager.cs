using System;
using System.Collections.Generic;
using System.Linq;
using KeyenceSimulation.Dto;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Managers
{
  public class KeyenceMessageManager : IKeyenceMessageManager
  {
    protected readonly Random _random;
    protected int _sequence;

    public KeyenceMessageManager()
    {
      _random = new Random();
      _sequence = 0;
    }

    public IKeyenceMessage BuildMessage()
    {
      return GenerateKeyenceMessage();
    }

    protected IKeyenceMessage GenerateKeyenceMessage()
    {
      const string spec = "3.52";
      var cameraName = GenerateCameraName();

      var snapshots = GenerateSnapshots();
      var message = new KeyenceMessage(cameraName, spec, ++_sequence);

      if (snapshots == null) return message;

      foreach (var snapshot in snapshots.Where(snap => snap != null))
        message.AddSnapshot(snapshot.Name, snapshot.Length, snapshot.Width, snapshot.Xoffset, snapshot.Yoffset, snapshot.Uom);

      return message;
    }


    protected string GenerateCameraName()
    {
      const string prefix = "CC";
      var first = Between(200);
      var second = Between(350);

      return string.Format("{0}{1:D03}{2:D03}", prefix, first, second);
    }

    protected int Between(int upper, int lower = 1)
    {
      return _random.Next(lower, upper);
    }

    protected double Between(double upper, double lower)
    {
      var rndDble = _random.NextDouble();
      return ((upper - lower) * rndDble) + lower;
    }

    protected List<KeyenceSnapshot> GenerateSnapshots()
    {

      var sequence = 1;
      var snapshots = new List<KeyenceSnapshot>();
      
      snapshots.AddRange(GenerateSnapshotLines(ref sequence));
      snapshots.AddRange(GenerateSnapshotCls(ref sequence));
      snapshots.AddRange(GenerateSnapshotAngles(ref sequence));
      snapshots.AddRange(GenerateSnapshotCorners(ref sequence));
      snapshots.AddRange(GenerateSnapshotArcs(ref sequence));
      snapshots.AddRange(GenerateSnapshotDias(ref sequence));

      return snapshots;
    }

    protected IEnumerable<KeyenceSnapshot> GenerateSnapshotLines(ref int sequence)
    {
      return GenerateSnapshotSequence("LN-LN", 3, 18, 2, ref sequence);
    }

    protected IEnumerable<KeyenceSnapshot> GenerateSnapshotCls(ref int sequence)
    {
      return GenerateSnapshotSequence("CL-CL", 2, 24, 8, ref sequence);
    }

    protected IEnumerable<KeyenceSnapshot> GenerateSnapshotAngles(ref int sequence)
    {
      return GenerateSnapshotSequence("ANGLE", 2, 140, 130, ref sequence, GenerateDegreeSnapshot);
    }

    protected IEnumerable<KeyenceSnapshot> GenerateSnapshotCorners(ref int sequence)
    {
      return GenerateSnapshotSequence("CORNER", 4, 2.3, 1.8, ref sequence);
    }

    protected IEnumerable<KeyenceSnapshot> GenerateSnapshotArcs(ref int sequence)
    {
      return GenerateSnapshotSequence("ARC", 2, 2.5, 1.5, ref sequence);
    }

    protected IEnumerable<KeyenceSnapshot> GenerateSnapshotDias(ref int sequence)
    {
      return GenerateSnapshotSequence("DIA", 1, 40, 30, ref sequence);
    }

    protected delegate KeyenceSnapshot SnapshotGenerator(string name, double upper, double lower, ref int sequence);
    protected IEnumerable<KeyenceSnapshot> GenerateSnapshotSequence(string sequenceName, int count, double upper,
      double lower, ref int sequence, SnapshotGenerator generate = null)
    {
      if (generate == null) generate = GenerateDimensionSnapshot;

      var snapshots = new List<KeyenceSnapshot>();
      for(var i=0; i<count; i++)
        snapshots.Add(generate(GenerateSnapshotName(sequenceName, i+1), upper, lower, ref sequence));

      return snapshots;
    }

    protected string GenerateSnapshotName(string sequenceName, int i)
    {
      return string.Format("{0}{1:D03}", sequenceName, i);
    }

    protected KeyenceSnapshot GenerateDimensionSnapshot(string name, double upper, double lower, ref int sequence)
    {
      return GenerateSnapshot("mm", name, upper, lower, ref sequence);
    }

    protected KeyenceSnapshot GenerateDegreeSnapshot(string name, double upper, double lower, ref int sequence)
    {
      return GenerateSnapshot("degree(s)", name, upper, lower, ref sequence);
    }

    protected KeyenceSnapshot GenerateSnapshot(string uom, string name, double upper, double lower, ref int sequence)
    {
      var length = Between(upper, lower);
      var width = Between(upper, lower);
      var offset = Between(.2, .05);

      return new KeyenceSnapshot(sequence++, name, length, width, offset, offset * -1, uom);
    }
  }
}
