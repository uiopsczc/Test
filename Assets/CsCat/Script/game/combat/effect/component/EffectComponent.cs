namespace CsCat
{
  public class EffectComponent : GameComponent
  {
    public EffectEntity effectEntity => this.GetEntity<EffectEntity>();


    protected override void _Destroy()
    {
      base._Destroy();
    }
  }
}


