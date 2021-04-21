using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public interface ITimelinableSequencePlayer
  {
    void Play();
    void Stop();
    void Pause();
    void UnPause();

    void UpdateTime(float time);
    void SetTime(float time);

    void Reset();
    void Dispose();

  }
}



