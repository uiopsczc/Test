using UnityEngine;

namespace CsCat
{
	public partial class TransformComponent : AbstractComponent
	{
		public Vector3 localPosition;
		private Vector3 _localEulerAngles;
		private Quaternion _localRotation = Quaternion.identity;
		public Vector3 localScale;


		public override void Init()
		{
			base.Init();
			localPosition = Vector3.zero;
			_localEulerAngles = Vector3.zero;
			_localRotation = Quaternion.identity;
			localScale = Vector3.one;
		}

		public Vector3 localEulerAngles
		{
			get => _localEulerAngles;
			set
			{
				this._localEulerAngles = value;
				this._localRotation = Quaternion.Euler(_localEulerAngles);
			}
		}

		public Quaternion localRotation
		{
			get => _localRotation;
			set
			{
				this._localRotation = value;
				this._localEulerAngles = this._localRotation.eulerAngles;
			}
		}

		//////////////////////////////////////////////////////////////////////
		// 
		//////////////////////////////////////////////////////////////////////

		public Vector3 position
		{
			get { return localPosition; }
			set { localPosition = value; }
		}

		public Vector3 eulerAngles
		{
			get { return localEulerAngles; }
			set { localEulerAngles = value; }
		}

		public Quaternion rotation
		{
			get { return localRotation; }
			set { localRotation = value; }
		}


		public Vector3 scale
		{
			get { return localScale; }
			set { localScale = value; }
		}
	}
}