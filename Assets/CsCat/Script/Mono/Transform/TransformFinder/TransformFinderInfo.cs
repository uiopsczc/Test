using System;
using UnityEngine;

namespace CsCat
{
	public class TransformFinderInfo
	{
		public Type transformFinderType;
		public string name;
		private Func<object> _createCallback;

		public TransformFinderInfo(Type transformFinderType)
		{
			this.name = transformFinderType.GetLastName();
			this.transformFinderType = transformFinderType;
			this._createCallback = () => Activator.CreateInstance(transformFinderType);
		}

		public TransformFinderBase CreateInstance()
		{
			return Activator.CreateInstance(transformFinderType) as TransformFinderBase;
		}

		public T CreateInstance<T>() where T : TransformFinderBase
		{
			return CreateInstance() as T;
		}
	}
}