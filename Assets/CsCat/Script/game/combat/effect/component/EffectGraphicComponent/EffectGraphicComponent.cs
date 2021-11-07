using UnityEngine;

namespace CsCat
{
  public partial class EffectGraphicComponent : GraphicComponent
  {
    public EffectEntity effectEntity => this.GetEntity<EffectEntity>();

    public override void Init()
    {
      base.Init(Client.instance.combat.effectManager.resLoadComponent);
    }


    public override void OnAllAssetsLoadDone()
    {
      base.OnAllAssetsLoadDone();
      ApplyToTransform(this.effectEntity.transformComponent.position, this.effectEntity.transformComponent.eulerAngles);
    }

    public void ApplyToTransform(Vector3? position, Vector3? eulerAngles)
    {
      if (this.transform == null)
        return;
      if (position != null)
        transform.position = position.Value;
      if (eulerAngles != null)
        transform.eulerAngles = eulerAngles.Value;
    }

    public override void DestroyGameObject()
    {
      if (this.gameObject != null)
        this.gameObject.Despawn();
    }

    public GameObjectPoolCat GetEffectGameObjectPool(GameObject prefab)
    {
      return PoolCatManagerUtil.GetOrAddGameObjectPool(GetEffectGameObjectPoolName(), prefab,
        EffectManagerConst.Pool_Name + "/" + effectEntity.effect_id);
    }

    public string GetEffectGameObjectPoolName()
    {
      return EffectManagerConst.Pool_Name + "_" + effectEntity.effect_id;
    }

    public override GameObject InstantiateGameObject(GameObject prefab)
    {
      return GetEffectGameObjectPool(prefab).SpawnGameObject();
    }

    public override bool IsCanUpdate()
    {
      return this.gameObject != null && base.IsCanUpdate();
    }

    protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base._Update(deltaTime, unscaledDeltaTime);
      ApplyToTransform(this.effectEntity.transformComponent.position, this.effectEntity.transformComponent.eulerAngles);
    }
  }
}