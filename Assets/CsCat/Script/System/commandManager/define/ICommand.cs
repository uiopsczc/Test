namespace CsCat
{
  public interface ICommand
  {
    void Execute(ICommandMessage commandMessage);
  }
}