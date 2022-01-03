using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class SerializableDictionary_Test : ScriptableObject
	{
		[SerializeField]
		private SerializableDictionary_GameObject_Float _dict =
		  SerializableDictionary_GameObject_Float.New<SerializableDictionary_GameObject_Float>();

		private Dictionary<GameObject, float> dict
		{
			get { return _dict.dict; }
		}
	}
}