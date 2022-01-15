using UnityEngine;

namespace CsCat
{
	public class CoroutineSequenceTest : MonoBehaviour
	{
		private void Start()
		{
			var async = CoroutineSequence.Start(this);
			async.Then(next =>
			{
				Hello();
				next();
			});
			async.WaitForSeconds(5);
			async.Then(next =>
				{
					World();
					next();
				}
			);
		}

		void Hello()
		{
			Debug.LogError("hello");
		}

		void World()
		{
			Debug.LogError("world");
		}
	}
}