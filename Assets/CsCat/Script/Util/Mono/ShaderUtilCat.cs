using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ShaderUtilCat
	{
		//Shader.Find是一个非常消耗的函数，因此尽量缓存起来
		private static readonly Dictionary<string, Shader> _cacheShaderDict = new Dictionary<string, Shader>();

		public static Shader FindShader(string shaderName)
		{
			if (_cacheShaderDict.TryGetValue(shaderName, out var shader)) return shader;
			shader = Shader.Find(shaderName);
			_cacheShaderDict[shaderName] = shader;
			if (shader == null)
				LogCat.LogErrorFormat("缺少Shader：{0}", shaderName);
			return shader;
		}
	}
}