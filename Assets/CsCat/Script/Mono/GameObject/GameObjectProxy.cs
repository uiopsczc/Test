using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class GameObjectProxy
	{
		private GameObject _gameObject;
		private bool _isActive;
		private Transform _transform;
		private RectTransform _rectTransform;
		private string _name;

		public Transform transform
		{
			get
			{
				if (_transform == null)
					_transform = this._gameObject.transform;
				return _transform;
			}
		}

		public RectTransform rectTransform
		{
			get
			{
				if (_rectTransform == null)
					_rectTransform = this._gameObject.GetComponent<RectTransform>();
				return _rectTransform;
			}
		}

		public string name
		{
			set
			{
				if (this._name.Equals(value))
					return;
				this._name = value;
			}
			get => this._name;
		}

		public GameObjectProxy(GameObject gameObject)
		{
			this._gameObject = gameObject;
			this._name = gameObject.name;
			this._isActive = gameObject.activeSelf;
		}

		public void SetActive(bool isActive)
		{
			if (this._isActive == isActive)
				return;
			this._isActive = isActive;
			this._gameObject.SetActive(this._isActive);
		}

		public bool IsActive()
		{
			return this._isActive;
		}
	}
}