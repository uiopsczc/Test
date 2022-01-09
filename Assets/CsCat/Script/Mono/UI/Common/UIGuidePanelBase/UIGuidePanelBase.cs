using UnityEngine;

namespace CsCat
{
	public partial class UIGuidePanelBase : UIPopUpPanel
	{

		private GameObject bgPrefab;
		private GameObject dialogRightPrefab;
		private GameObject dialogLeftPrefab;
		private GameObject fingerPrefab;
		private GameObject arrowPrefab;
		private GameObject descPrefab;

		public UIGuidePanelBase.BgItem bgItem;

		public override bool isHideBlackMaskBehind => true;

		public override void Init()
		{
			base.Init();
			graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIGuidePanelBase.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			bgPrefab = this.frameTransform.Find("bg").gameObject;
			dialogRightPrefab = this.frameTransform.Find("dialog_right").gameObject;
			dialogLeftPrefab = this.frameTransform.Find("dialog_left").gameObject;
			fingerPrefab = this.frameTransform.Find("finger").gameObject;
			arrowPrefab = this.frameTransform.Find("arrow").gameObject;
			descPrefab = this.frameTransform.Find("desc").gameObject;

			bgItem = this.AddChild<UIGuidePanelBase.BgItem>(null, bgPrefab);
		}

		public UIGuidePanelBase.DialogItem CreateDialogLeftItem()
		{
			GameObject clone = GameObject.Instantiate(dialogLeftPrefab, graphicComponent.transform);
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.DialogItem>(null, clone);
		}

		public UIGuidePanelBase.DialogItem CreateDialogRightItem()
		{
			GameObject clone = GameObject.Instantiate(dialogRightPrefab, graphicComponent.transform);
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.DialogItem>(null, clone);
		}

		public UIGuidePanelBase.FingerItem CreateFingerItem()
		{
			GameObject clone = GameObject.Instantiate(fingerPrefab, graphicComponent.transform);
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.FingerItem>(null, clone);
		}

		public UIGuidePanelBase.ArrowItem CreateArrowItem()
		{
			GameObject clone = GameObject.Instantiate(arrowPrefab, graphicComponent.transform);
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.ArrowItem>(null, clone);
		}

		public UIGuidePanelBase.DescItem CreateDescItem()
		{
			GameObject clone = GameObject.Instantiate(descPrefab, graphicComponent.transform);
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.DescItem>(null, clone);
		}

	}
}