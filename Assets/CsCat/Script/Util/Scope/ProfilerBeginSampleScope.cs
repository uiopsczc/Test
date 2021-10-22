using System;
using UnityEngine.Profiling;

namespace CsCat
{
	public class ProfilerBeginSampleScope : IDisposable
	{
		public ProfilerBeginSampleScope(string name)
		{
			Profiler.BeginSample(name);
		}


		public void Dispose()
		{
			Profiler.EndSample();
		}
	}
}