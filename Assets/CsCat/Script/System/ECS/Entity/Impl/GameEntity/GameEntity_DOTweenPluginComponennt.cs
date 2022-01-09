using DG.Tweening;

namespace CsCat
{
	public partial class GameEntity
	{
		public Sequence AddDOTweenSequence(string key)
		{
			return this.dotweenPluginComponent.AddDOTweenSequence(key);
		}

		public Tween AddDOTween(string key, Tween tween)
		{
			return this.dotweenPluginComponent.AddDOTween(key, tween);
		}

		public void RemoveDOTween(string key)
		{
			this.dotweenPluginComponent.RemoveDOTween(key);
		}

		public void RemoveDOTween(Tween tween)
		{
			this.dotweenPluginComponent.RemoveDOTween(tween);
		}

		public void RemoveAllDOTweens()
		{
			this.dotweenPluginComponent.RemoveDOTweens();
		}
	}
}