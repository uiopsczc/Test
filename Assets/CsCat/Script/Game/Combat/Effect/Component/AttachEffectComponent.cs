using UnityEngine;

namespace CsCat
{
	public class AttachEffectComponent : EffectComponent
	{
		private Transform _attachEntityTransform;
		private Vector3? _forceEulerAngles;
		private float _sectorAngle;

		public bool isAttach;

		public void Init(IPosition attachEntityIPosition,
		  Vector3? forceEulerAngles = null, float sectorAngle = 0)
		{
			base.Init();
			attachEntityIPosition.SetSocketName(this.effectEntity.cfgEffectData.socketName1);
			this._attachEntityTransform = attachEntityIPosition.GetTransform();
			this._forceEulerAngles = forceEulerAngles;
			this._sectorAngle = sectorAngle;

			this.AddListener(this.GetGameEntity().eventDispatchers, ECSEventNameConst.OnAllAssetsLoadDone, OnAllAssetsLoadDone);

		}

		void OnAllAssetsLoadDone()
		{
			Attach();
		}

		void Attach()
		{
			this.ChangeAttach(true);
		}

		void DeAttach()
		{
			this.ChangeAttach(false);
		}

		void ChangeAttach(bool isAttach)
		{
			if (this.effectEntity.graphicComponent.gameObject == null)
				return;
			if (this.isAttach == isAttach)
				return;
			if (isAttach)
			{
				var socketTransform = this._attachEntityTransform;
				this.effectEntity.graphicComponent.SetParentTransform(socketTransform);
				this.effectEntity.graphicComponent.transform.localPosition = new Vector3(0, 0, 0);
				this.effectEntity.graphicComponent.transform.localRotation = Quaternion.identity;
				if (this._forceEulerAngles != null)
					this.effectEntity.graphicComponent.transform.eulerAngles = this._forceEulerAngles.Value;
				this.SetSector();
				this.isAttach = true;
			}
			else
			{
				this.effectEntity.graphicComponent.SetParentTransform(Client.instance.combat.effectManager.graphicComponent.transform);
				this.isAttach = false;
				this.effectEntity.graphicComponent.SetIsShow(false);
			}
		}

		void SetSector()
		{
			if (this._sectorAngle != 0)
			{
				Transform sectorSideLeft = this.effectEntity.graphicComponent.transform.Find("Sector/SideLeft");
				Transform sectorSideRight = this.effectEntity.graphicComponent.transform.Find("Sector/SideRight");
				Transform sectorCenter = this.effectEntity.graphicComponent.transform.Find("Sector/Center");
				Material sectorCenterMat = sectorCenter.GetComponent<MeshRenderer>().material;
				sectorCenterMat.SetFloat("_AngleCos", Mathf.Cos(Mathf.Deg2Rad * (_sectorAngle / 2))); //扇形的角度大小
				sectorSideLeft.localRotation = Quaternion.Euler(0, (this._sectorAngle + 2) / 2, 0); // 左边界的位置
				sectorSideRight.localRotation = Quaternion.Euler(0, -(this._sectorAngle + 2) / 2, 0); // 右边界的位置
			}
		}
	}
}



