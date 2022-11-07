using UnityEngine;

namespace CsCat
{
	public class TransformInfoProxy
	{
		private Transform _transform;
		private GameObject _gameObject;
		private RectTransform _rectTransform;
		private bool _isSetRectTransform;
		private readonly TransformInfo _transformInfo = new TransformInfo();

		public Transform GetTransform()
		{
			return this._transform;
		}

		public GameObject GetGameObject()
		{
			return this._gameObject;
		}

		public RectTransform GetRectTransform()
		{
			if (!_isSetRectTransform)
			{
				_isSetRectTransform = true;
				_rectTransform = this._transform == null ? null : this._transform.GetComponent<RectTransform>();
			}
			return _rectTransform;
		}

		public void ApplyToTransform(Transform transform)
		{
			this._transform = transform;
			this._gameObject = transform != null ? transform.gameObject:null;
			if(transform != null)
				this._transformInfo.ApplyToTransform(transform);
			_isSetRectTransform = false;
		}

		public virtual bool SetIsShow(bool isShow)
		{
			if (this._transformInfo.IsShow() == isShow)
				return false;
			this._transformInfo.SetIsShow(isShow);
			if(_transform!=null)
				_transform.gameObject.SetActive(isShow);
			return true;
		}

		public bool IsShow()
		{
			return this._transformInfo.IsShow();
		}

		public void SetLocalPosition(Vector3 localPosition)
		{
			if (this.GetLocalPosition() == localPosition)
				return;
			this._transformInfo.SetLocalPosition(localPosition);
			if(this._transform!=null)
				_transform.localPosition = localPosition;
		}

		public Vector3 GetLocalPosition()
		{
			return this._transformInfo.GetLocalPosition();
		}


		public void SetLocalEulerAngles(Vector3 localEulerAngles)
		{
			if (this.GetLocalEulerAngles() == localEulerAngles)
				return;
			this._transformInfo.SetLocalEulerAngles(localEulerAngles);
			if (this._transform != null)
				_transform.localEulerAngles = localEulerAngles;
		}

		public Vector3 GetLocalEulerAngles()
		{
			return this._transformInfo.GetLocalEulerAngles();
		}


		public void SetLocalRotation(Quaternion localRotation)
		{
			if (this.GetLocalRotation() == localRotation)
				return;
			this._transformInfo.SetLocalRotation(localRotation);
			if (this._transform != null)
				_transform.localRotation = localRotation;
		}

		public Quaternion GetLocalRotation()
		{
			return this._transformInfo.GetLocalRotation();
		}


		public void SetLocalScale(Vector3 localScale)
		{
			if (this.GetLocalScale() == localScale)
				return;
			this._transformInfo.SetLocalScale(localScale);
			if (this._transform != null)
				_transform.localScale = localScale;
		}

		public Vector3 GetLocalScale()
		{
			return this._transformInfo.GetLocalScale();
		}

		public void SetPosition(Vector3 position)
		{
			this.SetLocalPosition(position);
		}

		public Vector3 GetPosition()
		{
			return this.GetLocalPosition();
		}

		public void SetEulerAngles(Vector3 eulerAngles)
		{
			this.SetLocalEulerAngles(eulerAngles);
		}


		public Vector3 GetEulerAngles()
		{
			return this.GetLocalEulerAngles();
		}


		public void SetRotation(Quaternion rotation)
		{
			this.SetLocalRotation(rotation);
		}


		public Quaternion GetRotation()
		{
			return this.GetLocalRotation();
		}

		public void SetScale(Vector3 scale)
		{
			this.SetLocalScale(scale);
		}


		public Vector3 GetScale()
		{
			return this.GetLocalScale();
		}

		public void Reset()
		{
			this._transform = null;
			this._gameObject = null;
			this._rectTransform = null;
			this._isSetRectTransform = false;
			this._transformInfo.Reset();
		}
	}
}