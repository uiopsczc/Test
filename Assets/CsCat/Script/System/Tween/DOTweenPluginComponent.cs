
using System.Collections.Generic;
using DG.Tweening;
namespace CsCat
{
  public class DOTweenPluginComponent:AbstractComponent
  {
    private DOTweenPlugin dotweenPlugin;
    public void Init(DOTweenPlugin dotweenPlugin)
    {
      base.Init();
      this.dotweenPlugin = dotweenPlugin;

    }
    public Sequence AddDOTweenSequence(string key)
    {
      return dotweenPlugin.AddDOTweenSequence(key);
    }

    public Tween AddDOTween(string key, Tween tween)
    {
      return dotweenPlugin.AddDOTween(key, tween);
    }

    public void RemoveDOTween(string key)
    {
      dotweenPlugin.RemoveDOTween(key);
    }

    public void RemoveDOTween(Tween tween)
    {
      dotweenPlugin.RemoveDOTween(tween);
    }
    

    protected override void __SetIsPaused(bool is_paused)
    {
      base.__SetIsPaused(is_paused);
      dotweenPlugin.SetIsPaused(is_paused);
    }

    public void RemoveAllDOTweens()
    {
      this.dotweenPlugin.RemoveAllDOTweens();
    }

    protected override void __Reset()
    {
      base.__Reset();
      RemoveAllDOTweens();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      RemoveAllDOTweens();
    }
  }
}
