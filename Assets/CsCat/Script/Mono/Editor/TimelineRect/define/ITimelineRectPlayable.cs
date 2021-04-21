using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public interface ITimelineRectPlayable
  {
    void OnPlay(TimelineRect timelineRect);
    bool OnStop(TimelineRect timelineRect);
    void OnPause(TimelineRect timelineRect);
    void OnUnPause(TimelineRect timelineRect);
  }
}