using UnityEngine;

namespace CsCat
{
	public partial class GameEntity : Entity
	{
		public GameEntity parent => GetParent<GameEntity>();
		public EventDispatchers eventDispatchers = new EventDispatchers();
		public CoroutineDictComponent CoroutineDictComponent;
		public PausableCoroutineDictComponent PausableCoroutineDictComponent;
		public DOTweenPluginComponent dotweenPluginComponent;
		public TimerManagerPluginComponent timerManagerPluginComponent;
		public EventDispatchersPluginDictComponent eventDispatchersPluginDictComponent;

		public override void Init()
		{
			base.Init();
			eventDispatchersPluginDictComponent = this.AddComponent<EventDispatchersPluginDictComponent>(null);
			
			CoroutineDictComponent =
				this.AddComponent<CoroutineDictComponent>(null, new CoroutineDict(Main.instance));
			PausableCoroutineDictComponent =
				this.AddComponent<PausableCoroutineDictComponent>(null, new PausableCoroutineDict(Main.instance));
			dotweenPluginComponent = this.AddComponent<DOTweenPluginComponent>(null, new DOTweenPlugin());
			timerManagerPluginComponent =
				this.AddComponent<TimerManagerPluginComponent>(null, new TimerManagerPlugin(timerManager));
		}

		


		


		public string GetGuid()
		{
			return this.key;
		}


		public new GameEntity GetChild(string childKey)
		{
			return base.GetChild<GameEntity>(childKey);
		}

		protected override void _Reset()
		{
			base._Reset();
		}


		protected override void _Destroy()
		{
			base._Destroy();
			
			CoroutineDictComponent = null;
			PausableCoroutineDictComponent = null;
			timerManagerPluginComponent = null;
			eventDispatchersPluginDictComponent = null;

			
		}
	}
}