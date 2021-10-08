using System;
using System.Diagnostics;
using System.Text;

namespace CsCat
{
    public class StringBuilderScope : PoolScope<StringBuilder>
    {
        public StringBuilder stringBuilder => this.spawn;


        public StringBuilderScope(int? capacity = null) : base()
        {
            if (capacity != null)
                stringBuilder.Capacity = capacity.Value;
        }


        public override void Dispose()
        {
            stringBuilder.Clear();
            base.Dispose();
        }
    }
}