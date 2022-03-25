using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class GraphicComponent
	{
		private Transform _parentTransform;
		private GameObject _gameObject;
		private bool _isHide = false;
		private bool _isNotDestroyGameObject;

		public Transform _transform;

		public RectTransform _rectTransform;




		public void SetParentTransform(Transform parentTransform)
		{
			this._parentTransform = parentTransform;
			if (this._gameObject != null)
				this._transform.SetParent(this._parentTransform,
					!LayerMask.LayerToName(this._gameObject.layer).Equals("UI"));
		}


		public virtual GameObject InstantiateGameObject(GameObject prefab)
		{
			return Object.Instantiate(prefab);
		}

		public virtual void SetIsShow(bool isShow)
		{
			this._isHide = !isShow;
			if (this._gameObject != null)
				this._gameObject.SetActive(!this._isHide);
		}

		protected virtual void InitGameObjectChildren()
		{
			GetGameEntity().InitGameObjectChildren();
		}


		public virtual void SetGameObject(GameObject gameObject, bool? isNotDestroyGameObject)
		{
			this.cache.Remove2(typeof(Transform).ToString());
			this.cache.Remove2(typeof(RectTransform).ToString());
			this._gameObject = gameObject;
			if (gameObject == null)
				return;
			if (isNotDestroyGameObject != null)
				this._isNotDestroyGameObject = isNotDestroyGameObject.Value;
			InitGameObjectChildren();
			SetIsShow(!_isHide);
		}

		public bool IsShow()
		{
			return !this._isHide;
		}

		public Transform GetTransform()
		{
			_transform = cache.GetOrAddDefault(() => this._gameObject.transform);
			return _transform;
		}

		public RectTransform GetRectTransform()
		{
			_rectTransform = cache.GetOrAddDefault(() => this._gameObject.GetComponent<RectTransform>());
			return _rectTransform;
		}

		public Transform GetParentTransform()
		{
			return this._parentTransform;
		}

		public GameObject GetGameObject()
		{
			return this._gameObject;
		}

		public void SetPosition(Vector3 pos)
		{
			if (this._transform != null)
				this._transform.position = pos;
		}

		public void SetEulerAngles(Vector3 eulerAngles)
		{
			if (this._transform != null)
				this._transform.eulerAngles = eulerAngles;
		}

		public void SetRotation(Quaternion rotation)
		{
			if (this._transform != null)
				this._transform.rotation = rotation;
		}


		public void SetScale(Vector3 scale)
		{
			if (this._transform != null)
				this._transform.SetLossyScale(scale);
		}

		public void SetLocalPosition(Vector3 localPosition)
		{
			if (this._transform != null)
				this._transform.localPosition = localPosition;
		}

		public void SetLocalEulerAngles(Vector3 localEulerAngles)
		{
			if (this._transform != null)
				this._transform.localEulerAngles = localEulerAngles;
		}

		public void SetLocalRotation(Quaternion localRotation)
		{
			if (this._transform != null)
				this._transform.localRotation = localRotation;
		}

		public void SetLocalScale(Vector3 localScale)
		{
			if (this._transform != null)
				this._transform.localScale = localScale;
		}

		public virtual void DestroyGameObject()
		{
			if (this._gameObject != null && !_isNotDestroyGameObject)
				_gameObject.Destroy();
		}
	}
}