using System;

namespace CsCat
{
  public class Pause : ISingleton
  {
    #region field

    /// <summary>
    ///   每次pause会加一，每次unPause会减一，当为pauseCount的时候会Resume
    /// </summary>
    private int pause_count;

    #endregion

    #region private method

    private void AddPauseCount(int cnt)
    {
      var org = is_paused;
      pause_count += cnt;
      var cur = is_paused;
      if (org != cur)
      {
        if (is_paused)
          onPuase?.Invoke();
        else
          onUnPause?.Invoke();
      }
    }

    #endregion

    #region property

    public static Pause instance = SingletonFactory.instance.Get<Pause>();
    public bool is_paused => pause_count > 0;

    #endregion

    #region delegate

    public Action onPuase;
    public Action onUnPause;

    #endregion

    #region public method

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
      var org = is_paused;
      pause_count = 0;
      var cur = is_paused;

      if (org != cur)
        onUnPause?.Invoke();
    }

    #endregion
  }
}