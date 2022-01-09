using UnityEngine;

namespace CsCat
{
	public static class PhysicsTest
	{
		public static void Call()
		{
			LogCat.log("333333333");
		}

		public static void Test1()
		{
			Client.instance.physicsManager.RegisterOnClick(GameObject.Find("Cube"), Call);
		}

		public static void Test2()
		{
			Client.instance.physicsManager.UnRegisterOnClick(GameObject.Find("Cube"), Call);
		}

		public static void Test3()
		{
			Client.instance.physicsManager.SetRaycastLayer("UI");
		}
	}
}