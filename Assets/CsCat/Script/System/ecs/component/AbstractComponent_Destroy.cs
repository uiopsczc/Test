using System;

namespace CsCat
{
    public partial class AbstractComponent
    {
        private bool _isDestroyed;
        public Action destroyCallback;

        public bool IsDestroyed()
        {
            return this._isDestroyed;
        }


        public void Destroy()
        {
            if (IsDestroyed())
                return;
            SetIsEnabled(false);
            SetIsPaused(false);
            _Destroy();
            _isDestroyed = true;
            _PostDestroy();
            cache.Clear();
        }

        protected virtual void _Destroy()
        {
        }

        protected virtual void _PostDestroy()
        {
            destroyCallback?.Invoke();
            destroyCallback = null;
        }


        public void OnDespawn()
        {
            _OnDespawn_();
            _OnDespawn_Destroy();
            _OnDespawn_Enable();
            _OnDespawn_Pause();
            _OnDespawn_Reset();
        }

        void _OnDespawn_Destroy()
        {
            _isDestroyed = false;
            destroyCallback = null;
        }
    }
}