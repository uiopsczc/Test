using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CsCat
{
  public class GenericMenuItemInfo
  {
    /// <summary>
    /// 当前名称
    /// </summary>
    public string name;

    /// <summary>
    /// 排序级别
    /// </summary>
    public int priority = int.MaxValue;

    /// <summary>
    /// 是否需要校验
    /// </summary>
    public bool isValidate;

    /// <summary>
    /// 点击时调用的函数
    /// </summary>
    public MethodInfo methodInfo;

    /// <summary>
    /// 校验选项是否可以使用的函数
    /// </summary>
    public MethodInfo methodInfoValidate;


    /// <summary>
    /// 父节点
    /// </summary>
    public GenericMenuItemInfo parent;

    /// <summary>
    /// 孩子节点
    /// </summary>
    public List<GenericMenuItemInfo> children = new List<GenericMenuItemInfo>();

    public string itemName;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="genericMenuItemAttribute"></param>
    /// <param name="methodInfo"></param>
    public GenericMenuItemInfo(GenericMenuItemInfo parent, GenericMenuItemAttribute genericMenuItemAttribute,
      MethodInfo methodInfo)
    {
      name = genericMenuItemAttribute.names[genericMenuItemAttribute.currentNameIndex];
      this.parent = parent;
      this.itemName = genericMenuItemAttribute.itemName;
      genericMenuItemAttribute.currentNameIndex++;
      Update(genericMenuItemAttribute, methodInfo);
    }

    /// <summary>
    /// 更新该节点的信息
    /// </summary>
    /// <param name="genericMenuItemAttribute"></param>
    /// <param name="methodInfo"></param>
    public void Update(GenericMenuItemAttribute genericMenuItemAttribute, MethodInfo methodInfo)
    {
      if (this.priority > genericMenuItemAttribute.priority)
        this.priority = genericMenuItemAttribute.priority;
      if (genericMenuItemAttribute.names.Length - genericMenuItemAttribute.currentNameIndex >= 1)
      {
        SubNamesLengthGreaterThanOrEquals_1_Condition(genericMenuItemAttribute, methodInfo);
      }
      else
      {
        SubNamesLengthEquals_0_Condition(methodInfo, genericMenuItemAttribute);
      }
    }


    /// <summary>
    /// subNamesLenght>=1的情况
    /// </summary>
    /// <param name="genericMenuItemAttribute"></param>
    /// <param name="methodInfo"></param>
    public void SubNamesLengthGreaterThanOrEquals_1_Condition(GenericMenuItemAttribute genericMenuItemAttribute,
      MethodInfo methodInfo)
    {
      string firstSubName = genericMenuItemAttribute.names[genericMenuItemAttribute.currentNameIndex];
      GenericMenuItemInfo target = children.Find(e => e.name.Equals(firstSubName));
      if (target == null)
      {
        children.Add(new GenericMenuItemInfo(this, genericMenuItemAttribute, methodInfo));
      }
      else
      {
        genericMenuItemAttribute.currentNameIndex++;
        target.Update(genericMenuItemAttribute, methodInfo);
      }
    }

    /// <summary>
    /// subNamesLenght==0的情况
    /// </summary>
    /// <param name="methodInfo"></param>
    /// <param name="genericMenuItemAttribute"></param>
    public void SubNamesLengthEquals_0_Condition(MethodInfo methodInfo,
      GenericMenuItemAttribute genericMenuItemAttribute)
    {
      if (methodInfo.ReturnType == typeof(bool))
        this.methodInfoValidate = methodInfo;
      else
        this.methodInfo = methodInfo;

      if (genericMenuItemAttribute.isValidate)
        this.isValidate = true;
    }

    /// <summary>
    /// 进行排序
    /// </summary>
    public void Sort()
    {
      foreach (var child in children)
      {
        child.Sort();
      }

      children.Sort(
        (a, b) =>
        {
          if (a.priority < b.priority)
            return -1;
          if (a.priority > b.priority)
            return 1;
          return 0;
        });
    }

    /// <summary>
    /// 获取该节点下的所有的叶子节点
    /// </summary>
    /// <returns></returns>
    public List<GenericMenuItemInfo> GetLeafList()
    {
      List<GenericMenuItemInfo> result = new List<GenericMenuItemInfo>();
      foreach (var child in children)
      {
        if (child.methodInfo != null)
          result.Add(child);
        else
          result.AddRange(child.GetLeafList(), false);
      }

      return result;
    }

    /// <summary>
    /// 获取NamePath,相对于relativeToNamePath
    /// </summary>
    /// <param name="relativeNamePath"></param>
    /// <returns></returns>
    public string GetNamePath(string relativeNamePath)
    {
      GenericMenuItemInfo iterator = this;
      StringBuilder sb = new StringBuilder(iterator.name);
      while (iterator.parent != null)
      {
        iterator = iterator.parent;
        if (iterator.name.Equals(relativeNamePath))
          break;
        sb.Insert(0, string.Format("{0}/", iterator.name));
      }

      return sb.ToString();
    }
  }
}
