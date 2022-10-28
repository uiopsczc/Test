using System;
using System.Collections.Generic;

namespace CsCat
{
	public interface IPoolIndex
	{
		int GetIndex();

		object GetValue();
	}
}