using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
	/// <summary>
	///   所有类的指定bindingFlags指定属性访问器
	/// </summary>
	public partial class MemberAccessorPool : ISingleton
	{
		/// <summary>
		///   所有类的指定bindingFlags指定属性访问器
		/// </summary>
		protected Dictionary<MemberAccessorClassType, MemberAccessorDict> classTypeAccessorDict =
			new Dictionary<MemberAccessorClassType, MemberAccessorDict>();


		public static MemberAccessorPool instance => SingletonFactory.instance.Get<MemberAccessorPool>();

		public void SingleInit()
		{
		}

		/// <summary>
		///   获得指定type，指定bindingFlags的属性访问器
		///   没有的话，就创建
		/// </summary>
		/// <param name="classType"></param>
		/// <param name="bindingFlags"></param>
		/// <returns></returns>
		public Dictionary<string, MemberAccessor> GetAccessors(Type classType, BindingFlags bindingFlags)
		{
			var value = GetAssessorInfo(classType, bindingFlags);
			if (value == null) //没有的话，就创建
			{
				value = new MemberAccessorDict(classType.GetFields(bindingFlags));
				classTypeAccessorDict.Add(new MemberAccessorClassType(classType, bindingFlags), value);
			}

			return value.memberAccessorDict;
		}


		/// <summary>
		///   【保护方法】
		///   查找typeAccessorDict是否存在有指定classType中指定bindingFlags的属性访问器
		/// </summary>
		/// <param name="classType"></param>
		/// <param name="bindingFlags"></param>
		/// <returns></returns>
		protected MemberAccessorDict GetAssessorInfo(Type classType, BindingFlags bindingFlags)
		{
			var tempMemberAccessorType = new MemberAccessorClassType(typeof(Type), BindingFlags.Default)
			{
				classType = classType,
				bindingFlags = bindingFlags
			};
			classTypeAccessorDict.TryGetValue(tempMemberAccessorType, out var result);
			return result;
		}
	}
}