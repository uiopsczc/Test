using UnityEngine;

namespace CsCat
{
	public partial class GameEntity : AbstractEntity
	{
		public GameEntity parent => GetParent<GameEntity>();
		public EventDispatchers eventDispatchers = new EventDispatchers();
		public CoroutinePluginComponent coroutinePluginComponent;
		public PausableCoroutinePluginComponent pausableCoroutinePluginComponent;
		public DOTweenPluginComponent dotweenPluginComponent;
		public TimerManagerPluginComponent timerManagerPluginComponent;
		public EventDispatchersPluginDictComponent eventDispatchersPluginDictComponent;

		public override void Init()
		{
			base.Init();
			eventDispatchersPluginDictComponent = this.AddComponent<EventDispatchersPluginDictComponent>(null);
			
			coroutinePluginComponent =
				this.AddComponent<CoroutinePluginComponent>(null, new CoroutinePlugin(Main.instance));
			pausableCoroutinePluginComponent =
				this.AddComponent<PausableCoroutinePluginComponent>(null, new PausableCoroutinePlugin(Main.instance));
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
			
			coroutinePluginComponent = null;
			pausableCoroutinePluginComponent = null;
			timerManagerPluginComponent = null;
			eventDispatchersPluginDictComponent = null;

			
		}
	}
}