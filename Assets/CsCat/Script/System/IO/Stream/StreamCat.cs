namespace CsCat
{
  public abstract class StreamCat
  {
    #region field

    protected int pos;
    protected int length;

    #endregion

    #region abstract method

    public abstract byte[] GetBuffer();
    public abstract void Seek(int length);
    public abstract void Skip(int length);

    #endregion

    #region virtual method

    public virtual void Close()
    {
    }

    public virtual int CurrentPosition()
    {
      return pos;
    }

    public virtual bool Eof()
    {
      return pos >= length;
    }

    public virtual int GetLength()
    {
      return length;
    }

    public virtual void Reset()
    {
      pos = 0;
    }

    #endregion
  }
}