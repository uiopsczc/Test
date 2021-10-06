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
        protected Dictionary<MemberAccessorClasssType, MemberAccessorDict> classTypeAccessor_dict =
            new Dictionary<MemberAccessorClasssType, MemberAccessorDict>();


        public static MemberAccessorPool instance => SingletonFactory.instance.Get<MemberAccessorPool>();

        public void SingleInit()
        {
        }

        /// <summary>
        ///   获得指定type，指定bindingFlags的属性访问器
        ///   没有的话，就创建
        /// </summary>
        /// <param name="class_type"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public Dictionary<string, MemberAccessor> GetAccessors(Type class_type, BindingFlags bindingFlags)
        {
            var value = GetAssessorInfo(class_type, bindingFlags);
            if (value == null) //没有的话，就创建
            {
                value = new MemberAccessorDict(class_type.GetFields(bindingFlags));
                classTypeAccessor_dict.Add(new MemberAccessorClasssType(class_type, bindingFlags), value);
            }

            return value.memberAccessor_dict;
        }


        /// <summary>
        ///   【保护方法】
        ///   查找typeAccessorDict是否存在有指定classType中指定bindingFlags的属性访问器
        /// </summary>
        /// <param name="class_type"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        protected MemberAccessorDict GetAssessorInfo(Type class_type, BindingFlags bindingFlags)
        {
            var tempMemberAccessorType = new MemberAccessorClasssType(typeof(Type), BindingFlags.Default);
            tempMemberAccessorType.class_type = class_type;
            tempMemberAccessorType.bindingFlags = bindingFlags;
            MemberAccessorDict result;
            classTypeAccessor_dict.TryGetValue(tempMemberAccessorType, out result);
            return result;
        }


        
    }
}