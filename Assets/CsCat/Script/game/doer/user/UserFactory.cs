namespace CsCat
{
  public class UserFactory : DoerFactory
  {
    protected override Doer __NewDoer(string id)
    {
      var doer = this.AddChildWithoutInit<User>(null);
      return doer;
    }
  }
}