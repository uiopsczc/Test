using UnityEngine;

namespace CsCat
{
  public class UIWaitingPanel : UIPanel
  {
    public override bool is_resident
    {
      get { return true; }
    }

    public override EUILayerName layerName
    {
      get { return EUILayerName.WaitingUILayer; }
    }

    private int waiting_count = 0;
    private Animation waiting_ainimation;
    private GameObject waiting_gameObject;

    public void Init(GameObject gameObject)
    {
      base.Init();
      graphicComponent.SetGameObject(gameObject, true);
      graphicComponent.SetIsShow(false);
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      waiting_gameObject = frame_transform.Find("waiting").gameObject;
      waiting_ainimation = waiting_gameObject.GetComponent<Animation>();
    }

    public void StartWaiting()
    {
      this.waiting_count += 1;
      graphicComponent.SetIsShow(true);
    }

    public void EndWaiting()
    {
      this.waiting_count -= 1;
      if (this.waiting_count <= 0)
      {
        this.waiting_count = 0;
        graphicComponent.SetIsShow(false);
      }
    }


    protected override void _Reset()
    {
      base._Reset();
      this.waiting_count = 0;
    }

    public void HideWaiting()
    {
      Reset();
      graphicComponent.SetIsShow(false);
    }




  }
}