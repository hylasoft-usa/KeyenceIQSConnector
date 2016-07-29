using System.Collections.ObjectModel;
using KeyenceSimulation.Dto;

namespace KeyenceSimulation.Interfaces
{
  public interface IKeyenceMessageSegment
  {
    Collection<KeyenceLine> GetLines();
  }
}
