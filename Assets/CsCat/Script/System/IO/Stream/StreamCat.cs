namespace CsCat
{
	public abstract class StreamCat
	{
		protected int _pos;
		protected int _length;


		public abstract byte[] GetBuffer();
		public abstract void Seek(int length);
		public abstract void Skip(int length);


		public virtual void Close()
		{
		}

		public virtual int CurrentPosition()
		{
			return _pos;
		}

		public virtual bool Eof()
		{
			return _pos >= _length;
		}

		public virtual int GetLength()
		{
			return _length;
		}

		public virtual void Reset()
		{
			_pos = 0;
		}
	}
}