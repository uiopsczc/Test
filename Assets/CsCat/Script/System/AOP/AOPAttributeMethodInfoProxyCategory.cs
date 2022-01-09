using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	///   AOP属性对【被切面的方法】的每个AOPMethodType类型的对应处理函数
	/// </summary>
	public class AOPAttributeMethodInfoProxyCategory
	{
		#region ctor

		public AOPAttributeMethodInfoProxyCategory(IAOPAttribute aopAttribute)
		{
			this.aopAttribute = aopAttribute;
		}

		#endregion

		#region field

		/// <summary>
		///   AOP属性
		/// </summary>
		public IAOPAttribute aopAttribute;

		/// <summary>
		///   AOP属性对被切面的方法的每个AOPMethodType类型对应的处理函数
		///   bool 参数中是否加了self作为参数
		/// </summary>
		public Dictionary<AOPMethodType, MethodInfoProxy>
		  aopMethodInfoDict = new Dictionary<AOPMethodType, MethodInfoProxy>();

		#endregion
	}
}