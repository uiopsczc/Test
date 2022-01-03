using System;
using System.Reflection;

namespace CsCat
{
	public partial class MemberAccessorPool
	{
		/// <summary>
		///   calssType 和 bindingFlags  组成的唯一键值
		/// </summary>
		public class MemberAccessorClassType
		{
			#region ctor

			public MemberAccessorClassType(Type classType, BindingFlags bindingFlags)
			{
				this.classType = classType;
				this.bindingFlags = bindingFlags;
			}

			#endregion

			#region field

			/// <summary>
			///   类型
			/// </summary>
			public Type classType;

			/// <summary>
			///   BindingFlags
			/// </summary>
			public BindingFlags bindingFlags;

			#endregion

			#region override method

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType()) return false;
				var other = (MemberAccessorClassType)obj;
				return classType == other.classType && bindingFlags.Equals(other.bindingFlags);
			}

			public override int GetHashCode()
			{
				return ObjectUtil.GetHashCode(classType, bindingFlags);
			}

			#endregion
		}
	}
}