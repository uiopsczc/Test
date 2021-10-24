namespace CsCat
{
  public class AOPExample1 : AOPExample
  {
    #region field

    public string name;

    #endregion

    #region public method

    [AOP_Test("chen")] //AOP属性  用于AOP处理
    public void CallHello(string message, Shape2D shape)
    {
      using (new AOP(this, message, shape)) //对AOP的属性处理的调用,xxx,yyy,kkk为该函数的参数
      {
        LogCat.LogError(message);
      }
    }


    [AOP_Test("zhongmou")] //AOP属性  用于AOP处理
    public void CallWorld(string message)
    {
      using (new AOP(this, message)) //对AOP的属性处理的调用,xxx,yyy,kkk为该函数的参数
      {
        LogCat.LogError(message);
      }
    }


    #endregion





  }
}