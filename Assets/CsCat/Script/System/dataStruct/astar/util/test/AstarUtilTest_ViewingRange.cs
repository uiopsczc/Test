using UnityEngine;

namespace CsCat
{
  public partial class AStarUtilTest
  {
    public static void Test_GetViewingRange()
    {
      LogCat.log(AStarUtil.GetViewingRange(new Vector2Int(2, 2)));
    }

    public static void Test_IsInViewing()
    {
      LogCat.log(AStarUtil.IsInViewingRange(new Vector2Int(2, 2), new Vector2Int(2, 4)));
    }

  }
}