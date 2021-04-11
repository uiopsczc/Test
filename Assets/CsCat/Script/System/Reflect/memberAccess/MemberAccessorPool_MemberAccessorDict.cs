using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
  public partial class MemberAccessorPool
  {
    /// <summary>
    ///   所有fieldInfos的属性访问器，存放在memberAccessorDict中
    /// </summary>
    public class MemberAccessorDict
    {
      #region field

      /// <summary>
      ///   fieldInfos属性访问器
      ///   key为fieldInfo的name
      /// </summary>
      public Dictionary<string, MemberAccessor> memberAccessor_dict;

      #endregion

      #region ctor

      /// <summary>
      ///   创建所有fieldInfos的属性访问器，存放在memberAccessorDict中
      /// </summary>
      /// <param name="fieldInfos"></param>
      public MemberAccessorDict(FieldInfo[] fieldInfos)
      {
        memberAccessor_dict = new Dictionary<string, MemberAccessor>();
        for (var i = 0; i < fieldInfos.Length; i++)
        {
          var memberAccessor = MemberAccessor.Create(fieldInfos[i]);
          memberAccessor_dict.Add(memberAccessor.memberInfo.Name, memberAccessor);
        }
      }

      #endregion

      #region public method

      /// <summary>
      ///   获取属性访问器
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public MemberAccessor GetMemberAccessor(string name)
      {
        MemberAccessor result;
        memberAccessor_dict.TryGetValue(name, out result);
        return result;
      }

      #endregion
    }
  }
}