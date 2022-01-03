using UnityEngine;

namespace CsCat
{
	public class EffectTest
	{
		public static void Test()
		{
			//    Client.instance.effectManager.CreateEffect<GroundEffect>("1").InitGroundEffect(Vector3.back, Vector3.zero, 2);
			//    Client.instance.effectManager.CreateEffect<AttachEffect>("1").InitAttachEffect(GameObject.Find("Main Camera").transform);
			//    Client.instance.effectManager.CreateEffect<MissileEffect>("1").InitMissileEffect(Vector3.back.ToTransformVector3(), (Vector3.forward * 10).ToTransformVector3(), 1, 0.2f);
			//    Client.instance.effectManager.CreateEffect<MortarMissileEffect>("1").InitMortarMissileEffect(Vector3.back.ToTransformVector3(), (Vector3.forward * 10).ToTransformVector3(), new Vector3(0,-9,0), 30f);
			Client.instance.combat.effectManager.CreateLineEffectEntity("1", null, Vector3.back.ToVector3Position(),
			  (Vector3.forward * 10).ToVector3Position(), 1, 1);
			//    Client.instance.effectManager.CreateEffect<SpinLineEffect>("1").InitSpinLineEffect(Vector3.zero,Vector3.right,30, 360, 5,Vector3.zero,0);
		}
	}
}



