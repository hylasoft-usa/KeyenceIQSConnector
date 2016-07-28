using KeyenceSimulation.Dto;
using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Managers
{
  public class KeyenceMessageManager : IKeyenceMessageManager
  {
    public IKeyenceMessage BuildMessage()
    {
      return GenerateKeyenceMessage();
    }

    protected IKeyenceMessage GenerateKeyenceMessage()
    {
      return new KeyenceMessage();
    }
  }
}
