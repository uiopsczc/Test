namespace CsCat
{
	public partial class GLUtil
	{
		public static GLPushMatrixScope PushMatrix()
		{
			return new GLPushMatrixScope();
		}
	}
}