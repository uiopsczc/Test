namespace CsCat
{
  public class UserFactory : DoerFactory
  {
    protected override Doer _NewDoer(string id)
    {
      var doer = this.AddChildWithoutInit<User>(null);
      return doer;
    }
  }
}