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

			var children = gameObject.GetComponentsInChildren<Animator>();
			for (var i = 0; i < children.Length; i++)
			{
				var animator = children[i];
				if (animator.enabled)
					animator.SetPause(cause);
			}

			var systems = gameObject.GetComponentsInChildren<ParticleSystem>();
			for (var i = 0; i < systems.Length; i++)
			{
				var particleSystem = systems[i];
				particleSystem.SetPause(cause);
			}
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