namespace CsCat
{
	public abstract class InputStream : StreamCat
	{
		public abstract void Peek(byte[] buffer, int offset, int length);

		public abstract void Read(byte[] buffer, int offset, int length);
	}
}