namespace CsCat
{
  public abstract class InputStream : StreamCat
  {
    public abstract void Peek(byte[] buf, int offset, int length);

    public abstract void Read(byte[] buf, int offset, int length);
  }
}