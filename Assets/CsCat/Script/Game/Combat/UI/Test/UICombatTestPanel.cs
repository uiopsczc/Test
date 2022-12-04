using UnityEngine.UI;

namespace CsCat
{
	public class UICombatTestPanel : UIPanel
	{
		private Button gmBtn;
		private Button testBtn;
		public override EUILayerName layerName => EUILayerName.BackgroundUILayer;

		public override void Init()
		{
			base.Init();
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UICombatTestPanel.prefab");
		}

		public override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			gmBtn = this._frameTransform.FindComponentInChildren<Button>("gm_btn");
			testBtn = this._frameTransform.FindComponentInChildren<Button>("test_btn");

		}

		protected override void _AddUnityListeners()
		{
			base._AddUnityListeners();
			this.RegisterOnClick(testBtn, Test);
		}

		void Test()
		{
			EffectTest.Test();
		}
	}
}