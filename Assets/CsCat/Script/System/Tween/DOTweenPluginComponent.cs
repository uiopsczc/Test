using System.Collections.Generic;
using DG.Tweening;

namespace CsCat
{
    public class DOTweenPluginComponent : AbstractComponent
    {
        private DOTweenPlugin _dotweenPlugin;

        public void Init(DOTweenPlugin dotweenPlugin)
        {
            base.Init();
            this._dotweenPlugin = dotweenPlugin;
        }

        public Sequence AddDOTweenSequence(string key)
        {
            return _dotweenPlugin.AddDOTweenSequence(key);
        }

        public Tween AddDOTween(string key, Tween tween)
        {
            return _dotweenPlugin.AddDOTween(key, tween);
        }

        public void RemoveDOTween(string key)
        {
            _dotweenPlugin.RemoveDOTween(key);
        }

        public void RemoveDOTween(Tween tween)
        {
            _dotweenPlugin.RemoveDOTween(tween);
        }


        protected override void _SetIsPaused(bool isPaused)
        {
            base._SetIsPaused(isPaused);
            _dotweenPlugin.SetIsPaused(isPaused);
        }

        public void RemoveDOTweens()
        {
            this._dotweenPlugin.RemoveDOTweens();
        }

        protected override void _Reset()
        {
            base._Reset();
            RemoveDOTweens();
        }

        protected override void _Destroy()
        {
            base._Destroy();
            RemoveDOTweens();
        }
    }
}