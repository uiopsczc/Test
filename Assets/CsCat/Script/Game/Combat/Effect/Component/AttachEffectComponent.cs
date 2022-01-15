using UnityEngine;

namespace CsCat
{
	public class AttachEffectComponent : EffectComponent
	{
		private Transform attachEntityTransform;
		private Vector3? forceEulerAngles;
		private float sectorAngle;
		public bool isAttach;

		public void Init(IPosition attachEntityIPosition,
		  Vector3? forceEulerAngles = null, float sectorAngle = 0)
		{
			base.Init();
			attachEntityIPosition.SetSocketName(this.effectEntity.cfgEffectData.socket_name_1);
			this.attachEntityTransform = attachEntityIPosition.GetTransform();
			this.forceEulerAngles = forceEulerAngles;
			this.sectorAngle = sectorAngle;

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
				var socketTransform = this.attachEntityTransform;
				this.effectEntity.graphicComponent.SetParentTransform(socketTransform);
				this.effectEntity.graphicComponent.transform.localPosition = new Vector3(0, 0, 0);
				this.effectEntity.graphicComponent.transform.localRotation = Quaternion.identity;
				if (this.forceEulerAngles != null)
					this.effectEntity.graphicComponent.transform.eulerAngles = this.forceEulerAngles.Value;
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
			if (this.sectorAngle != 0)
			{
				Transform sectorSideLeft = this.effectEntity.graphicComponent.transform.Find("sector/side_left");
				Transform sectorSideRight = this.effectEntity.graphicComponent.transform.Find("sector/side_right");
				Transform sectorCenter = this.effectEntity.graphicComponent.transform.Find("sector/center");
				Material sectorCenterMat = sectorCenter.GetComponent<MeshRenderer>().material;
				sectorCenterMat.SetFloat("_AngleCos", Mathf.Cos(Mathf.Deg2Rad * (sectorAngle / 2))); //扇形的角度大小
				sectorSideLeft.localRotation = Quaternion.Euler(0, (this.sectorAngle + 2) / 2, 0); // 左边界的位置
				sectorSideRight.localRotation = Quaternion.Euler(0, -(this.sectorAngle + 2) / 2, 0); // 右边界的位置
			}
		}
	}
}



