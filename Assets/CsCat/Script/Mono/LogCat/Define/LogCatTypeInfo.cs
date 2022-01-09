using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class LogCatTypeInfo
	{
		public LogCatType logType;
		public Color color;

		public LogCatTypeInfo(LogCatType logType, Color color)
		{
			this.logType = logType;
			this.color = color;
		}
	}
}