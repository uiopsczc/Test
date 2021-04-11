namespace CsCat
{
  public abstract class OutputStream : StreamCat
  {
    public abstract void Flush();

    public abstract bool Write(byte[] buf, int offset, int length);
  }
}