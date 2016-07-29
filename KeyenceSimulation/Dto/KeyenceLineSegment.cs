namespace KeyenceSimulation.Dto
{
  public class KeyenceLineSegment
  {
    public string Value { get; private set; }

    public KeyenceLineSegment(string value)
    {
      Value = value;
    }

    public override string ToString()
    {
      return Value;
    }
  }
}
