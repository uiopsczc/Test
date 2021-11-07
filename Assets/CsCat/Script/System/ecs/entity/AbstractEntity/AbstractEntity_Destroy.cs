using System;
using System.Collections.Generic;

namespace CsCat
{
    public partial class AbstractEntity
    {
        private bool _isDestroyed;
        public Action destroyCallback;

        public bool IsDestroyed()
        {
            return _isDestroyed;
        }

        public void Destroy()
        {
            if (IsDestroyed())
                return;
            RemoveAllChildren();
            SetIsEnabled(false, false);
            SetIsPaused(false, false);
            RemoveAllComponents();
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

        public virtual void OnDespawn()
        {
            _OnDespawn_();
            _OnDespawn_Child();
            _OnDespawn_Component();
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