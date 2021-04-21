using UnityEngine;

namespace CsCat
{
  public partial class GameEntity : AbstractEntity
  {
    public GameEntity parent { get { return GetParent<GameEntity>(); } }

    public CoroutinePluginComponent coroutinePluginComponent;
    public PausableCoroutinePluginComponent pausableCoroutinePluginComponent;
    public DOTweenPluginComponent dotweenPluginComponent;
    public TimerManagerPluginComponent timerManagerPluginComponent;
    public ResLoadComponent resLoadComponent;
    public GraphicComponent graphicComponent;

    public override void Init()
    {
      base.Init();
      resLoadComponent = this.AddComponent<ResLoadComponent>(null, new ResLoad());
      coroutinePluginComponent = this.AddComponent<CoroutinePluginComponent>(null, new CoroutinePlugin(Main.instance));
      pausableCoroutinePluginComponent = this.AddComponent<PausableCoroutinePluginComponent>(null, new PausableCoroutinePlugin(Main.instance));
      dotweenPluginComponent = this.AddComponent<DOTweenPluginComponent>(null, new DOTweenPlugin());
      timerManagerPluginComponent = this.AddComponent<TimerManagerPluginComponent>(null, new TimerManagerPlugin(timerManager));
      graphicComponent = CreateGraphicComponent();
    }

    protected virtual GraphicComponent CreateGraphicComponent()
    {
      return this.AddComponent<GraphicComponent>(null, this.resLoadComponent);
    }


    public override void PostInit()
    {
      base.PostInit();
      graphicComponent.LoadPrefabPath();
      PreLoadAssets();
      CheckIsAllAssetsLoadDone();
    }



    public string GetGuid()
    {
      return this.key;
    }



    public new GameEntity GetChild(string child_key)
    {
      return base.GetChild<GameEntity>(child_key);
    }

    protected override void __Reset()
    {
      base.__Reset();
      RemoveAllListeners();
      this.all_assets_load_done_callback = null;
    }



    protected override void __Destroy()
    {
      base.__Destroy();
      RemoveAllListeners();

      resLoadComponent = null;
      coroutinePluginComponent = null;
      pausableCoroutinePluginComponent = null;
      timerManagerPluginComponent = null;
      graphicComponent = null;

      is_all_assets_load_done = false;
      all_assets_load_done_callback = null;
    }
  }
}