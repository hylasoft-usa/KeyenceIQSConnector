namespace KeyenceSimulation.Dto
{
  public class KeyenceSnapshot
  {
    public int Sequence { get; private set; }

    public double Length { get; private set; }

    public double Width { get; private set; }

    public double Xoffset { get; private set; }

    public double Yoffset { get; private set; }

    public string Name { get; private set; }

    public string Uom { get; private set; }

    public KeyenceSnapshot(int sequence, string name, double length, double width, double xoffset, double yoffset, string uom)
    {
      Uom = uom;
      Yoffset = yoffset;
      Xoffset = xoffset;
      Width = width;
      Length = length;
      Name = name;
      Sequence = sequence;
    }
  }
}
