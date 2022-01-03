using System.Security.Cryptography;

namespace CsCat
{
	public class MD5Util
	{
		/// <summary>
		///   MD5加密
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string Encrypt(string s)
		{
			var md5Hash = MD5.Create();
			var datas = md5Hash.ComputeHash(s.GetBytes());
			using (var scope = new StringBuilderScope())
			{
				for (var i = 0; i < datas.Length; i++)
				{
					var data = datas[i];
					scope.stringBuilder.Append(data.ToString(StringConst.String_x2));
				}

				return scope.stringBuilder.ToString();
			}
		}
	}
}