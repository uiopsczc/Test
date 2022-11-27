using UnityEngine;

namespace CsCat
{
	public class TransformInfo
	{
		private Vector3 _localPosition;
		private Vector3 _localEulerAngles;
		private Quaternion _localRotation;
		private Vector3 _localScale;
		private bool _isShow;
		private Transform _parentTransform;


		public TransformInfo()
		{
		}

		public void ApplyToTransform(Transform toApplyTransform)
		{
			toApplyTransform.position = this.GetPosition();
			toApplyTransform.eulerAngles = this.GetEulerAngles();
			toApplyTransform.localScale = this.GetLocalScale();
			toApplyTransform.gameObject.SetActive(this._isShow);
			toApplyTransform.SetParent(this._parentTransform,
				toApplyTransform.gameObject.layer != LayerMask.NameToLayer("UI"));
		}

		public void SetParentTransform(Transform parentTransform)
		{
			this._parentTransform = parentTransform;
		}

		public Transform GetParentTransform()
		{
			return this._parentTransform;
		}

		public void SetLocalPosition(Vector3 localPosition)
		{
			this._localPosition = localPosition;
		}

		public Vector3 GetLocalPosition()
		{
			return this._localPosition;
		}


		public void SetLocalEulerAngles(Vector3 localEulerAngles)
		{
			this._localEulerAngles = localEulerAngles;
			this._localRotation = Quaternion.Euler(localEulerAngles.x, localEulerAngles.y, localEulerAngles.z);
		}

		public Vector3 GetLocalEulerAngles()
		{
			return this._localEulerAngles;
		}


		public void SetLocalRotation(Quaternion localRotation)
		{
			this._localRotation = localRotation;
			this._localEulerAngles = localRotation.eulerAngles;
		}

		public Quaternion GetLocalRotation()
		{
			return this._localRotation;
		}


		public void SetLocalScale(Vector3 localScale)
		{
			this._localScale = localScale;
		}

		public Vector3 GetLocalScale()
		{
			return this._localScale;
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

		public void RawSetIsShow(bool isShow)
		{
			this._isShow = isShow;
		}

		public bool SetIsShow(bool isShow)
		{
			if (this._isShow == isShow)
				return false;
			this._isShow = isShow;
			return true;
		}


		public bool IsShow()
		{
			return this._isShow;
		}

		public void Reset()
		{
			this._localPosition = Vector3.zero;
			this._localEulerAngles = Vector3.zero;
			this._localRotation = Quaternion.identity;
			this._localScale = Vector3.one;
			this._isShow = false;
		}
	}
}