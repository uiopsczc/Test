#if UNITY_EDITOR
namespace CsCat
{
  public partial class CameraTimelinableTrack
  {
    public void SyncAnimationWindow()
    {
      foreach (var playing_itemInfo in playing_itemInfo_list)
        (playing_itemInfo as CameraTimelinableItemInfo).SyncAnimationWindow(cur_time);
    }
  }
}
#endif