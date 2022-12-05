using UnityEngine;

namespace CsCat
{
	public partial class TransformInfoTreeNode : TreeNode
	{
		readonly TransformInfo _transformInfo = new TransformInfo();

		public void ApplyToTransform(Transform toApplyTransform)
		{
			_transformInfo.ApplyToTransform(toApplyTransform);
		}

		public void SetParentTransform(Transform parentTransform)
		{
			_transformInfo.SetParentTransform(parentTransform);
		}

		public Transform GetParentTransform()
		{
			return this._transformInfo.GetParentTransform();
		}

		public void SetLocalPosition(Vector3 localPosition)
		{
			_transformInfo.SetLocalPosition(localPosition);
		}

		public Vector3 GetLocalPosition()
		{
			return _transformInfo.GetLocalPosition();
		}


		public void SetLocalEulerAngles(Vector3 localEulerAngles)
		{
			_transformInfo.SetLocalEulerAngles(localEulerAngles);
		}

		public Vector3 GetLocalEulerAngles()
		{
			return _transformInfo.GetLocalEulerAngles();
		}


		public void SetLocalRotation(Quaternion localRotation)
		{
			_transformInfo.SetLocalRotation(localRotation);
		}

		public Quaternion GetLocalRotation()
		{
			return _transformInfo.GetLocalRotation();
		}


		public void SetLocalScale(Vector3 localScale)
		{
			_transformInfo.SetLocalScale(localScale);
		}

		public Vector3 GetLocalScale()
		{
			return _transformInfo.GetLocalScale();
		}

		public void SetPosition(Vector3 position)
		{
			_transformInfo.SetPosition(position);
		}

		public Vector3 GetPosition()
		{
			return _transformInfo.GetPosition();
		}

		public void SetEulerAngles(Vector3 eulerAngles)
		{
			_transformInfo.SetEulerAngles(eulerAngles);
		}


		public Vector3 GetEulerAngles()
		{
			return _transformInfo.GetEulerAngles();
		}


		public void SetRotation(Quaternion rotation)
		{
			_transformInfo.SetRotation(rotation);
		}


		public Quaternion GetRotation()
		{
			return _transformInfo.GetRotation();
		}

		public void SetScale(Vector3 scale)
		{
			_transformInfo.SetScale(scale);
		}


		public Vector3 GetScale()
		{
			return _transformInfo.GetScale();
		}


		public bool SetIsShow(bool isShow)
		{
			return _transformInfo.SetIsShow(isShow);
		}


		public bool IsShow()
		{
			return this._transformInfo.IsShow();
		}

		protected override void _Reset()
		{
			this._transformInfo.Reset();
			base._Reset();
		}

		protected override void _Destroy()
		{
			this._transformInfo.Reset();
			base._Destroy();
		}
	}
}