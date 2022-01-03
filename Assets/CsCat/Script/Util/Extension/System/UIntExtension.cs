using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class UIntExtension
	{
		public static bool IsContains(this uint self, uint beContainedValue)
		{
			return UintUtil.IsContains(self, beContainedValue);
		}
	}
}