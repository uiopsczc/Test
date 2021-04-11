namespace CsCat
{
  /// <summary>
  ///   【重点：通过partial 将各自的AOP对应的处理分而治之 分布在各自所属的类型文件中 】
  ///   AOPAttribute都要继承这个接口
  ///   AOPAttriubte  如果有特殊的的情况则优先用特殊情况，否则用默认的该接口的IAOPAttribute实现
  ///   特殊情况调用优先顺序
  ///   1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】+被切面方法的参数)
  ///   1.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】+被切面方法的参数）
  ///   2.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(被切面方法的参数)
  ///   2.1.被切面的方法的名称_AOPMethodType的类型（被切面方法的参数）
  ///   3.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】)
  ///   3.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】）
  ///   4.被切面的方法的名称_AOPMethodType的类型（）
  ///   4.1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型()
  /// </summary>
  public interface IAOPAttribute
  {
    /// <summary>
    ///   默认前处理
    /// </summary>
    void Pre_AOP_Handle();

    /// <summary>
    ///   默认后处理
    /// </summary>
    void Post_AOP_Handle();
  }
}