using UnityEngine;

namespace CsCat
{
	public static class ParticleSystemExtensions
	{
		/// <summary>
		/// 设置暂停
		/// </summary>
		/// <param name="self"></param>
		/// <param name="cause"></param>
		public static void SetPause(this ParticleSystem self, object cause)
		{
			PauseUtil.SetPause(self, cause);
		}

		public static float GetDuration(this ParticleSystem self, bool isRecursive = true)
		{
			float maxDuration = 0f;
			float duration = 0;
			if (isRecursive)
			{
				ParticleSystem[] particleSystems = self.GetComponentsInChildren<ParticleSystem>();
				foreach (var particleSystem in particleSystems)
				{
					duration = particleSystem.GetDuration(false);
					if (duration == -1f)
						return -1f;
					if (maxDuration < duration)
						maxDuration = duration;
				}

				return duration;
			}

			var main = self.main;
			if (main.loop)
				return -1;
			duration = main.duration + main.startLifetimeMultiplier + main.startDelayMultiplier;
			return duration;
		}
	}
}