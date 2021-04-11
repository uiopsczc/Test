using UnityEngine.Timeline;

namespace CsCat
{
  public static class TimelineClipExtension
  {
    public static bool IsInRange(this TimelineClip self, double global_time)
    {
      float percent = self.GetPercent(global_time);
      if (percent >= 0 && percent <= 1)
        return true;
      return false;
    }

    public static float GetLocalTime(this TimelineClip self, double global_time)
    {
      double timeOffset = global_time - self.start;
      return (float) timeOffset;
    }


    public static float GetPercent(this TimelineClip self, double global_time)
    {
      return (float) (self.GetLocalTime(global_time) / self.duration);
    }


  }
}