using System.Collections.Generic;

namespace CsCat
{
    public class GameObjectEntity : TickObject
    {
        public TransformComponent transformComponent;

        public override void Init()
        {
            base.Init();
            transformComponent = this.AddComponent<TransformComponent>(null);
        }
    }
}