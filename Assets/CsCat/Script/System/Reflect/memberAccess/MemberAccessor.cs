using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CsCat
{
    /// <summary>
    ///   属性访问器
    ///   可以通过getter获得该属性的值
    ///   可以通过setter设置该属性的值
    /// </summary>
    public abstract class MemberAccessor
    {
        #region field

        private static int Accessor_Number;

        #endregion

        #region property

        /// <summary>
        ///   该属性类型
        /// </summary>
        public abstract Type memberType { get; }

        /// <summary>
        ///   该属性的信息
        /// </summary>
        public abstract MemberInfo memberInfo { get; }

        #endregion

        #region delegate

        /// <summary>
        ///   获得该属性的值
        ///   参数0:该属性的实例
        ///   返回值：该属性的值
        /// </summary>
        protected Func<object, object> getter;

        /// <summary>
        ///   设置该属性的值
        ///   参数0：该属性的实例
        ///   参数1：属性新的值
        /// </summary>
        protected Action<object, object> setter;

        #endregion

        #region static method

        /// <summary>
        ///  只处理成员（_Xxx）或属性（属性是指set get )
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static MemberAccessor Create(MemberInfo memberInfo)
        {
            MemberAccessor result;
            try
            {
                if (memberInfo.MemberType == MemberTypes.Field) //处理filedInfo
                    result = new FieldMemberAccessor((FieldInfo) memberInfo);
                else
                {
                    //处理PropertyInfo
                    if (memberInfo.MemberType != MemberTypes.Property)
                        throw new NotSupportedException(memberInfo.MemberType.ToString());
                    result = new PropertyMemberAccessor((PropertyInfo) memberInfo);
                }
            }
            catch (Exception)
            {
                if (memberInfo.MemberType == MemberTypes.Field) //处理filedInfo
                    result = new FieldMember((FieldInfo) memberInfo);
                else
                    result = memberInfo.MemberType == MemberTypes.Property
                        ? new PropertyMember((PropertyInfo) memberInfo)
                        : null;
            }

            return result;
        }

        /// <summary>
        ///   转化类型（unbox）
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="declaring_type"></param>
        public static void EmitTypeConversion(ILGenerator generator, Type declaring_type)
        {
            generator.Emit(declaring_type.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, declaring_type);
        }

        #endregion

        #region public method

        /// <summary>
        ///   获得owner在该属性的值
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public object GetValue(object owner)
        {
            return getter(owner);
        }

        /// <summary>
        ///   设置owner在该属性的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public void SetValue(object obj, object value)
        {
            setter(obj, value);
        }

        #endregion
    }
}