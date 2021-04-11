namespace CsCat
{
  public interface IRestore
  {
    /// <summary>
    ///   产生需要还原的原因
    /// </summary>
    object cause { set; get; }

    /// <summary>
    ///   进行还原
    /// </summary>
    void Restore();
  }
}