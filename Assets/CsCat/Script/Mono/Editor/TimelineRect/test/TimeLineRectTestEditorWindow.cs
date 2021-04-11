using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class TimelineRectTestEditorWindow : EditorWindow
  {
    private TimelineRect timelineRect;


    void Awake()
    {
      
      this.timelineRect = new TimelineRect(() => new Rect(0,0,this.position.width,this.position.height));
    }
    
    public void OnEnable()
    {
      this.timelineRect.OnEnable();
    }
    
    public void OnDisable()
    {
      this.timelineRect.OnDisable();
    }
    
    
    void OnGUI()
    {
      this.timelineRect.OnGUI();
      Repaint();
    }
  }
}