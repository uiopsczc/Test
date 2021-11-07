using System;

namespace CsCat
{
    public partial class AbstractComponent
    {
        public Action resetCallback;


        public void Reset()
        {
            _Reset();
            _PostReset();
        }

        protected virtual void _Reset()
        {
        }

        protected virtual void _PostReset()
        {
            resetCallback?.Invoke();
            this.resetCallback = null;
        }


        void _OnDespawn_Reset()
        {
            resetCallback = null;
        }
    }
}