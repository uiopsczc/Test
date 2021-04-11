


using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class ShaderUtilCat
  {
    //Shader.Find是一个非常消耗的函数，因此尽量缓存起来
    private static readonly Dictionary<string, Shader> cache_shader_dict = new Dictionary<string, Shader>();

    public static Shader FindShader(string shader_name)
    {
      Shader shader;
      if (!cache_shader_dict.TryGetValue(shader_name, out shader))
      {
        shader = Shader.Find(shader_name);
        cache_shader_dict[shader_name] = shader;
        if (shader == null)
          LogCat.LogErrorFormat("缺少Shader：{0}", shader_name);
      }

      return shader;
    }
  }
}