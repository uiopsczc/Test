using UnityEngine;
using System;

namespace CsCat
{
  public class GLPushMatrixScope : IDisposable
  {
    public GLPushMatrixScope()
    {
      GL.PushMatrix();
    }

    public void Dispose()
    {
      GL.PopMatrix();
    }
  }
}
