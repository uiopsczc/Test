namespace CsCat
{
  public partial class GraphicComponent : GameComponent
  {
    private ResLoadComponentPlugin resLoadComponentPlugin;
    public void Init(ResLoadComponent resLoadComponent)
    {
      base.Init();
      resLoadComponentPlugin = new ResLoadComponentPlugin(resLoadComponent);
      this.AddListener(ECSEventNameConst.OnAllAssetsLoadDone.ToEventName(this.entity), OnAllAssetsLoadDone);
    }

    protected override void __Reset()
    {
      base.__Reset();
      resLoadComponentPlugin.Destroy();
      DestroyGameObject();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      resLoadComponentPlugin.Destroy();
      DestroyGameObject();



      resLoadComponentPlugin = null;

      parent_transform = null;
      gameObject = null;
      is_not_destroy_gameObject = false;
      prefab = null;
      _prefab_path = null;
      prefab_assetCat = null;
      is_load_done = false;
      is_hide = false;

    }
    
  }
}