namespace CsCat
{
  //创建的时候，如果该类中有SingleInit,会调用SingleInit，
  //public static XXX instance { get { return SingletonFactory.instance.Get<XXX>(); } }
  //public static XXX instance { get { return SingletonFactory.instance.GetMono<XXX>(); } }

  public interface ISingleton
  {
    void SingleInit();
  }
}