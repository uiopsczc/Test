using System;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   GUI.enabled  如果false的话，之后的东西都会变灰，不能使用，直到为true
	/// </summary>
	public class GUIEnabledScope : IDisposable
	{
		[SerializeField] private bool _preIsEnabled { get; }

		public GUIEnabledScope(bool isNewEnabled)
		{
			_preIsEnabled = GUI.enabled;
			GUI.enabled = isNewEnabled;
		}


		public void Dispose()
		{
			GUI.enabled = _preIsEnabled;
		}
	}
}