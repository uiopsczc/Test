using UnityEngine;

namespace CsCat
{
	public class PauseUtil
	{
		/// <summary>
		/// 设置暂停
		/// </summary>
		/// <param name="animator"></param>
		/// <param name="cause"></param>
		public static void SetPause(Animator animator, object cause)
		{
			new PropertyToRestore(cause, animator, "speed").AddToList();
			animator.speed = 0;
		}

		/// <summary>
		/// 设置暂停
		/// 暂停animator和particleSystem(包括其孩子节点)
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="cause"></param>
		public static void SetPause(GameObject gameObject, object cause)
		{
			if (gameObject.activeInHierarchy == false)
				return;

			foreach (var animator in gameObject.GetComponentsInChildren<Animator>())
				if (animator.enabled)
					animator.SetPause(cause);

			foreach (var particleSystem in gameObject.GetComponentsInChildren<ParticleSystem>())
				particleSystem.SetPause(cause);
		}

		/// <summary>
		/// 设置暂停
		/// </summary>
		/// <param name="particleSystem"></param>
		/// <param name="cause"></param>
		public static void SetPause(ParticleSystem particleSystem, object cause)
		{
			new MemberToRestoreProxy(cause, particleSystem, "Play", true);
			particleSystem.Pause();
		}
	}
}