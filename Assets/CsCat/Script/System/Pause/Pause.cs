using System;

namespace CsCat
{
    public class Pause : ISingleton
    {
        /// <summary>
        ///   每次pause会加一，每次unPause会减一，当为pauseCount的时候会Resume
        /// </summary>
        private int pauseCount;

        public void SingleInit()
        {
        }


        private void AddPauseCount(int cnt)
        {
            var org = isPaused;
            pauseCount += cnt;
            var cur = isPaused;
            if (org != cur)
            {
                if (isPaused)
                    onPause?.Invoke();
                else
                    onUnPause?.Invoke();
            }
        }


        public static Pause instance = SingletonFactory.instance.Get<Pause>();
        public bool isPaused => pauseCount > 0;


        public Action onPause;
        public Action onUnPause;


        public void SetPause()
        {
            AddPauseCount(1);
        }

        public void SetUnPause()
        {
            AddPauseCount(-1);
        }


        public void Reset()
        {
            var org = isPaused;
            pauseCount = 0;
            var cur = isPaused;

            if (org != cur)
                onUnPause?.Invoke();
        }
    }
}