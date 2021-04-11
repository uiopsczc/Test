using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CsCat
{
  /// <summary>
  ///  property访问器
  ///  通过IL创建getter方法
  ///  通过IL创建setter方法
  /// </summary>
  public sealed class PropertyMemberAccessor : MemberAccessor
  {
    #region field

    /// <summary>
    ///   该属性信息
    /// </summary>
    private readonly PropertyInfo propertyInfo;

    #endregion

    #region ctor

    public PropertyMemberAccessor(PropertyInfo propertyInfo)
    {
      this.propertyInfo = propertyInfo;
      InitializeGetter(propertyInfo); //设置getter方法
      InitializeSetter(propertyInfo); //设置setter方法
    }

    #endregion

    #region property

    /// <summary>
    ///   该属性类型
    /// </summary>
    public override Type member_type => propertyInfo.PropertyType;

    /// <summary>
    ///   该属性的信息
    /// </summary>
    public override MemberInfo memberInfo => propertyInfo;

    #endregion

    #region private method

    /// <summary>
    ///   创建propertyInfo的内部Getter方法
    ///   创建方法为   object 该属性所在的类.get_property_该属性的名称(object this )  返回的就是fieldInfo
    ///   创建的方法通过MemberAccessor的getter访问
    /// </summary>
    /// <param name="propertyInfo"></param>
    private void InitializeGetter(PropertyInfo propertyInfo)
    {
      //ReflectedType  如果filedInfo的类是内部定义类，则ReflectedType返回的是定义该内部内所在的类（即包含该内部类的类）
      var dynamicMethod = new DynamicMethod(propertyInfo.ReflectedType.FullName + ".get_property_" + propertyInfo.Name,
        typeof(object), new[]
          {typeof(object)}, propertyInfo.Module, true);
      var ilGenerator = dynamicMethod.GetILGenerator(); //创建动态方法
      ilGenerator.DeclareLocal(typeof(object)); //声明内部变量
      ilGenerator.Emit(OpCodes.Ldarg_0); //参数0：相当于在类中调用this关键字
      EmitTypeConversion(ilGenerator, propertyInfo.DeclaringType);
      ilGenerator.EmitCall(OpCodes.Callvirt, propertyInfo.GetGetMethod(), null); //Callvirt  call virtual method  调用虚方法
      if (propertyInfo.PropertyType.IsValueType) //PropertyType propertyInfo的类型
        ilGenerator.Emit(OpCodes.Box, propertyInfo.PropertyType);
      ilGenerator.Emit(OpCodes.Ret); //Ret: 方法结束  return propertyInfo
      getter = (Func<object, object>) dynamicMethod.CreateDelegate(typeof(Func<object, object>));
    }

    /// <summary>
    ///   创建fieldInfo的内部setter方法
    ///   创建方法为   void 该属性所在的类set_property_该属性的名称(object this， object 属性的值)  设置的就是fieldInfo
    ///  创建的方法通过MemberAccessor的setter访问
    /// </summary>
    /// <param name="propertyInfo"></param>
    private void InitializeSetter(PropertyInfo propertyInfo)
    {
      //ReflectedType  如果filedInfo的类是内部定义类，则ReflectedType返回的是定义该内部内所在的类（即包含该内部类的类）
      var dynamicMethod = new DynamicMethod(propertyInfo.ReflectedType.FullName + ".set_property_" + propertyInfo.Name,
        typeof(void), new[]
        {
          typeof(object),
          typeof(object)
        }, propertyInfo.Module, true); //创建动态方法
      var ilGenerator = dynamicMethod.GetILGenerator(); //创建动态方法
      ilGenerator.Emit(OpCodes.Ldarg_0); //OpCodes.Ldfld或Stfld之前必须OpCodes.Ldarg_0  参数0：相当于在类中调用this关键字
      EmitTypeConversion(ilGenerator,
        propertyInfo.DeclaringType); ////DeclaringType声明该fieldInfo所在的类（声明的地方，可能是父类声明，子类调用，但声明的地方是父类，所以指向的是父类）
      ilGenerator.Emit(OpCodes.Ldarg_1); //Ldarg_1  local Define arg_1 参数1：属性的值
      EmitTypeConversion(ilGenerator, propertyInfo.PropertyType);
      ilGenerator.EmitCall(OpCodes.Callvirt, propertyInfo.GetSetMethod(), null); //Callvirt  call virtual method
      ilGenerator.Emit(OpCodes.Ret); //Ret:方法结束
      setter = (Action<object, object>) dynamicMethod.CreateDelegate(typeof(Action<object, object>));
    }

    #endregion
  }
}