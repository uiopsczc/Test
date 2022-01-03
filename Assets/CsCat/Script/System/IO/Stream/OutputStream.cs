namespace CsCat
{
	public abstract class OutputStream : StreamCat
	{
		public abstract void Flush();

		public abstract bool Write(byte[] buffer, int offset, int length);
	}
}