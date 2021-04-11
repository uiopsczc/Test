using System.Diagnostics;
using System.Reflection;

namespace CsCat
{
  public class StackTraceUtil
  {
    //利用StackTrace基于offsetIndex+index获取所调用的函数
    public static MethodBase GetMethodOfFrame(int index = 0)
    {
      StackTrace stackTrace = new StackTrace();

      int offset_index = 1; //当前是第一层
      int target_index = offset_index + index;
      //UnityEngine.LogCat.LogWarning(targetIndex + " "+stackTrace.GetFrame(targetIndex).GetMethod().Name+" "+ stackTrace.GetFrame(targetIndex).GetMethod().DeclaringType);
      if (stackTrace.GetFrame(target_index) == null)
        return null;
      return stackTrace.GetFrame(target_index).GetMethod();
    }
  }
}