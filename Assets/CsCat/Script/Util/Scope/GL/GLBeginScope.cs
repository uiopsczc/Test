using UnityEngine;
using System;

namespace CsCat
{
    public class GLBeginScope : IDisposable
    {
        public GLBeginScope(int mode)
        {
            GL.Begin(mode);
        }

        public void Dispose()
        {
            GL.End();
        }
    }
}