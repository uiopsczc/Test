using System;
using System.Diagnostics;
using System.Text;

namespace CsCat
{
    public class StringBuilderScope : IDisposable
    {
        public StringBuilder stringBuilder;


        public StringBuilderScope()
        {
            stringBuilder = PoolCatManagerUtil.Spawn<StringBuilder>();
        }


        public void Dispose()
        {
            stringBuilder.Clear();
            PoolCatManagerUtil.Despawn(stringBuilder);
            stringBuilder = null;
        }
    }
}