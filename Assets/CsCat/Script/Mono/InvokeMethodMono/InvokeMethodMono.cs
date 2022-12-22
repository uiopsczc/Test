using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class InvokeMethodMono : MonoBehaviour
	{
		private MonoBehaviourCache _monoBehaviourCache;
		public bool isFoundChild; //当前transform中找不到的时候，【true：查找子节点】 【false：查找父节点】
		public string targetGameObjectName; //目标节点的名称，不写则没限制
		[HideInInspector] public string targetMethodArgsJsonString = "";
		[HideInInspector] public string targetMethodInfoName;
		[HideInInspector] public string targetTypeName;


		public MonoBehaviourCache monoBehaviourCache =>
			_monoBehaviourCache ?? (_monoBehaviourCache = new MonoBehaviourCache(this));

		public UnityEngine.Component targetComponent
		{
			get
			{
				return monoBehaviourCache.GetOrAddDefault("targetComponent",
					() =>
					{
						var type = GetType().Assembly.GetType(targetTypeName);
						var result = transform.GetComponent(type);
						if (result != null &&
						    (targetGameObjectName.IsNullOrWhiteSpace() || targetGameObjectName.Equals(gameObject.name)))
							return result;
						if (targetGameObjectName.IsNullOrWhiteSpace())
							return isFoundChild
								? transform.GetComponentInChildren(type)
								: transform.GetComponentInParent(type);

						return isFoundChild
							? transform.FindComponentInChildren<Transform>(name, true, false).GetComponent(type)
							: transform.FindComponentInParent<Transform>(name, false).GetComponent(type);
					});
			}
		}

		public void Invoke()
		{
			var args = (List<object>) JsonSerializer.Deserialize(targetMethodArgsJsonString);
			args = args ?? new List<object>();
			targetComponent.InvokeMethod(targetMethodInfoName, false, args.ToArray());
		}
	}
}