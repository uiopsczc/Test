using System;
using System.Diagnostics;
using System.Text;

namespace CsCat
{
    public class StringBuilderScope : IDisposable
    {
        public StringBuilder stringBuilder;

        #region ctor

        public StringBuilderScope()
        {
            stringBuilder = PoolCatManagerUtil.Spawn<StringBuilder>();
        }

        #endregion

        #region public method

        public void Dispose()
        {
            stringBuilder.Clear();
            PoolCatManagerUtil.Despawn(stringBuilder);
            stringBuilder = null;
        }

        #endregion
    }
}