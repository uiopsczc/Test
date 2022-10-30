using System;
using System.Collections.Generic;

namespace CsCat
{
	public static class ComponentTypes
	{
		public static Type[] Game => new[] {typeof(GraphicComponent),};

		private static Type[] _Transform;
		public static Type[] Transform
		{
			get
			{
				if (_Transform != null) return _Transform;
				_Transform = new Type[]{};
				_Transform = _Transform._AddComponentTypes(ComponentTypes.Game);
				return _Transform;
			}
		}

		private static Type[] _AddComponentTypes(this Type[] sourceTypes, params Type[] toAddTypes)
		{
			sourceTypes = sourceTypes.AddRange(toAddTypes);
			sourceTypes.Unique();
			return sourceTypes;
		}
	}
}