namespace KeyenceSimulation.Interfaces
{
  public interface IKeyenceMessageBody : IKeyenceMessageSegment
  {
    void AddSnapshot(string name, double length, double width, double xoffset, double yoffset, string uom);
  }
}
