using UnityEngine;

namespace CsCat
{
	public class CollisionCallback : MonoBehaviour
	{
		public static bool stop_call_back = false;

		///////////////////////////////////////////////////2D/////////////////////////////////////////////////
		void OnCollisionEnter2D(Collision2D other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnCollisionEnter2D", this.gameObject, other);
		}

		void OnCollisionExit2D(Collision2D other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnCollisionE.xit2D", this.gameObject, other);
		}

		void OnCollisionStay2D(Collision2D other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnCollisionStay2D", this.gameObject, other);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnTriggerEnter2D", this.gameObject, other);
		}

		void OnTriggerStay2D(Collider2D other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnTriggerStay2D", this.gameObject, other);
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnTriggerExit2D", this.gameObject, other);
		}

		///////////////////////////////////////////////////3D/////////////////////////////////////////////////
		void OnCollisionEnter(Collision other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnCollisionEnter", this.gameObject, other);
		}

		void OnCollisionExit(Collision other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnCollisionExit", this.gameObject, other);
		}

		void OnCollisionStay(Collision other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnCollisionStay", this.gameObject, other);
		}

		void OnTriggerEnter(Collider other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnTriggerEnter", this.gameObject, other);
		}

		void OnTriggerStay(Collider other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnTriggerStay", this.gameObject, other);
		}

		void OnTriggerExit(Collider other)
		{
			if (stop_call_back)
				return;
			XLuaManager.instance.CallLuaFunction("global.client.physicsManager.OnTriggerExit", this.gameObject, other);
		}
	}
}