
using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public class TestScriptableObjectAA
	{
		public string street;
		public int age;

		public TestScriptableObjectAA(string street, int age)
		{
			this.street = street;
			this.age = age;
		}

		public override string ToString()
		{
			return this.street + " " + this.age;
		}
	}
}



