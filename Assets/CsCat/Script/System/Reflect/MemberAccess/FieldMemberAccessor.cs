using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CsCat
{
	/// <summary>
	///  filed访问器
	///  通过IL创建getter方法
	///  通过IL创建setter方法
	/// </summary>
	public sealed class FieldMemberAccessor : MemberAccessor
	{
		#region field

		/// <summary>
		///  该属性信息
		/// </summary>
		private readonly FieldInfo _fieldInfo;

		#endregion

		#region ctor

		public FieldMemberAccessor(FieldInfo fieldInfo)
		{
			this._fieldInfo = fieldInfo;
			_InitializeGetter(fieldInfo); //初始化该属性的getter方法
			_InitializeSetter(fieldInfo); //初始化该属性的setter方法
		}

		#endregion

		#region property

		/// <summary>
		///   该属性类型
		/// </summary>
		public override Type memberType => _fieldInfo.FieldType;

		/// <summary>
		///   该属性的信息
		/// </summary>
		public override MemberInfo memberInfo => _fieldInfo;

		#endregion


		#region private method

		/// <summary>
		///   创建fieldInfo的内部Getter方法
		///   创建方法为   object 该属性所在的类.get_该属性的名称(object this )  返回的就是fieldInfo
		///   创建的方法通过MemberAccessor的getter访问
		/// </summary>
		/// <param name="fieldInfo"></param>
		private void _InitializeGetter(FieldInfo fieldInfo)
		{
			var dynamicMethod = new DynamicMethod(fieldInfo.ReflectedType.FullName + ".get_" + fieldInfo.Name,
				typeof(object),
				new[]
				{
					typeof(object)
				}, fieldInfo.Module, true); //创建动态方法
			var ilGenerator = dynamicMethod.GetILGenerator(); //创建动态方法里面的内容
			ilGenerator.Emit(OpCodes.Ldarg_0); //OpCodes.Ldfld或Stfld之前必须OpCodes.Ldarg_0  参数0：相当于在类中调用this关键字
			if (fieldInfo.DeclaringType.IsValueType
			) //DeclaringType声明该fieldInfo所在的类（声明的地方，可能是父类声明，子类调用，但声明的地方是父类，所以指向的是父类）
				ilGenerator.Emit(OpCodes.Unbox, fieldInfo.DeclaringType); //拆箱，  
			ilGenerator.Emit(OpCodes.Ldfld, fieldInfo); //Ldfld: local define field,引用fieldInfo  返回的就是fieldInfo
			if (fieldInfo.FieldType.IsValueType) //FieldType  fieldInfo的类型
				ilGenerator.Emit(OpCodes.Box, fieldInfo.FieldType); //封箱 ，对fieldInfo进行封箱操作
			ilGenerator.Emit(OpCodes.Ret); //Ret:方法结束  return fieldInfo
			getter = (Func<object, object>)dynamicMethod.CreateDelegate(typeof(Func<object, object>));
		}

		/// <summary>
		///   创建fieldInfo的内部setter方法
		///   创建方法为   void 该属性所在的类.set_该属性的名称(object this， object 属性的值)  设置的就是fieldInfo
		///   创建的方法通过MemberAccessor的setter访问
		/// </summary>
		/// <param name="fieldInfo"></param>
		private void _InitializeSetter(FieldInfo fieldInfo)
		{
			//ReflectedType  如果filedInfo的类是内部定义类，则ReflectedType返回的是定义该内部内所在的类（即包含该内部类的类）
			var dynamicMethod = new DynamicMethod(fieldInfo.ReflectedType.FullName + ".set_" + fieldInfo.Name,
				typeof(void),
				new[]
				{
					typeof(object),
					typeof(object)
				}, fieldInfo.Module, true); //创建动态方法
			var ilGenerator = dynamicMethod.GetILGenerator(); //创建动态方法里面的内容
			ilGenerator.Emit(OpCodes.Ldarg_0); //OpCodes.Ldfld或Stfld之前必须OpCodes.Ldarg_0  参数0：相当于在类中调用this关键字
			EmitTypeConversion(ilGenerator, fieldInfo.DeclaringType);
			ilGenerator.Emit(OpCodes.Ldarg_1); //Ldarg_1  local Define arg_1  参数1：属性的值
			ilGenerator.Emit(fieldInfo.FieldType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass,
				fieldInfo.FieldType);
			ilGenerator.Emit(OpCodes.Stfld,
				fieldInfo); //Stfld set field  有两个参数   参数0：this  参数1：属性的值 fieldInfo.value=arg_1
			ilGenerator.Emit(OpCodes.Ret); //Ret:方法结束
			setter = (Action<object, object>)dynamicMethod.CreateDelegate(typeof(Action<object, object>));
		}

		#endregion
	}
}