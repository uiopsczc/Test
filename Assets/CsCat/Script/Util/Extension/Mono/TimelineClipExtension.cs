using UnityEngine.Timeline;

namespace CsCat
{
    public static class TimelineClipExtension
    {
        public static bool IsInRange(this TimelineClip self, double globalTime)
        {
            float percent = self.GetPercent(globalTime);
            return percent >= 0 && percent <= 1;
        }

        public static float GetLocalTime(this TimelineClip self, double globalTime)
        {
            double timeOffset = globalTime - self.start;
            return (float) timeOffset;
        }


        public static float GetPercent(this TimelineClip self, double globalTime)
        {
            return (float) (self.GetLocalTime(globalTime) / self.duration);
        }
    }
}