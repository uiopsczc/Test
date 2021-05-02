using System.Collections.Generic;

namespace CsCat
{
  public class TickObject : GameEntity
  {
    protected override bool is_not_delete_child_relationship_immediately => true;

    protected override bool is_not_delete_component_relationShip_immediately => true;

    public TickObject parent_tickObject
    {
      get { return cache.GetOrAddDefault("parent_tickObject", () => parent as TickObject); }
    }

    public bool is_can_not_update = false;

    public override bool IsCanUpdate()
    {
      return !is_can_not_update && base.IsCanUpdate();
    }

    private TickObject child;
    private AbstractComponent component;

    public virtual void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.IsCanUpdate()) return;
      foreach (var child_key in child_key_list)
      {
        child = GetChild(child_key) as TickObject;
        child?.Update(deltaTime, unscaledDeltaTime);
      }
      foreach (var component_key in component_key_list)
      {
        component = GetComponent(component_key);
        component?.Update(deltaTime, unscaledDeltaTime);
      }
      __Update(deltaTime, unscaledDeltaTime);
    }

    public virtual void FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.IsCanUpdate()) return;
      foreach (var child_key in child_key_list)
      {
        child = GetChild(child_key) as TickObject;
        child?.FixedUpdate(deltaTime, unscaledDeltaTime);
      }
      foreach (var component_key in component_key_list)
      {
        component = GetComponent(component_key);
        component?.FixedUpdate(deltaTime, unscaledDeltaTime);
      }
      __FixedUpdate(deltaTime, unscaledDeltaTime);
    }


    public virtual void LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.IsCanUpdate()) return;
      foreach (var child_key in child_key_list)
      {
        child = GetChild(child_key) as TickObject;
        child?.LateUpdate(deltaTime, unscaledDeltaTime);
      }
      foreach (var component_key in component_key_list)
      {
        component = GetComponent(component_key);
        component?.LateUpdate(deltaTime, unscaledDeltaTime);
      }
      __LateUpdate(deltaTime, unscaledDeltaTime);
    }


    protected virtual void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
    }

    protected virtual void __FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
    }

    protected virtual void __LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      is_can_not_update = false;
    }
  }
}