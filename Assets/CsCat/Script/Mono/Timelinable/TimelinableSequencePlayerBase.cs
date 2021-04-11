using UnityEngine;

namespace CsCat
{
  public class TimelinableSequencePlayerBase
  {
    public Transform transform;
    public TimelinableSequenceBase sequence;
    protected float cur_time;

    protected bool is_playing;
    protected bool is_paused;

    public TimelinableSequencePlayerBase(Transform transform)
    {
      this.transform = transform;
    }


    public virtual void Play()
    {
      Reset();
      if (sequence != null)
        sequence.tracks.Foreach(track => { track.cur_time_itemInfo_index = -1; });
      is_playing = true;
    }

    public virtual void Stop()
    {
      Reset();
      sequence = null;
    }

    public virtual void Pause()
    {
      is_paused = true;
    }

    public virtual void UnPause()
    {
      is_paused = false;
    }

    public void SetTime(float time)
    {
      UpdateTime(time);
    }

    public virtual void UpdateTime(float time)
    {
      cur_time = time;
      if (is_playing)
        sequence.Tick(time);
      else
        sequence.Retime(time);
    }


    public virtual void Reset()
    {
      if (sequence != null)
        sequence.tracks.Foreach(track => { track.cur_time_itemInfo_index = -1; });
      is_playing = false;
      is_paused = false;
    }

    public virtual void Dispose()
    {
      Stop();
    }
  }
}