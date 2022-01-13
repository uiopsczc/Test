#if UNITY_EDITOR
namespace CsCat
{
	public partial class CameraTimelinableTrack
	{
		public void SyncAnimationWindow()
		{
			for (var i = 0; i < playingItemInfoList.Count; i++)
			{
				var playingItemInfo = playingItemInfoList[i];
				(playingItemInfo as CameraTimelinableItemInfo).SyncAnimationWindow(curTime);
			}
		}
	}
}
#endif