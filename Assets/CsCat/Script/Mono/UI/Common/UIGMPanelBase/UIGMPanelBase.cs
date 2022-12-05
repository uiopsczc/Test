using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UIGMPanelBase : UIPopUpPanel
	{

		protected List<Dictionary<string, object>> _configList = new List<Dictionary<string, object>>();
		protected GameObject _Nego_SwitchItem;
		protected GameObject _Nego_InputItem1;
		protected GameObject _Nego_InputItem2;
		protected GameObject _BtnClose;
		protected Transform _Nego_ScrollViewContent;

		protected override void _Init()
		{
			base._Init();
			this.SetPrefabPath("Assets/PatchResources/UI/UIGMPanelBase/Prefab/UIGMPanelBase.prefab");
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			_Nego_ScrollViewContent = this._frameTransform.Find("Nego_Content/Scroll View/Viewport/Nego_ScrollViewContent");
			_Nego_SwitchItem = _Nego_ScrollViewContent.Find("Nego_SwitchItem").gameObject;
			_Nego_InputItem1 = _Nego_ScrollViewContent.Find("Nego_InputItem1").gameObject;
			_Nego_InputItem2 = _Nego_ScrollViewContent.Find("Nego_InputItem2").gameObject;
			_BtnClose = this._frameTransform.Find("Nego_Content/BtnClose").gameObject;
		}

		protected override void _AddUnityListeners()
		{
			base._AddUnityListeners();
			this.RegisterOnClick(_BtnClose, this.Close);
		}


		protected override void _PostSetGameObject()
		{
			base._PostSetGameObject();
			InitConfigList();
			InitItems();
		}


		public void InitItems()
		{
			for (var i = 0; i < _configList.Count; i++)
			{
				Dictionary<string, object> config = _configList[i];
				switch (config["type"])
				{
					case "SwitchItem":
						InitSwitchItem(config);
						break;
					case "inputItem1":
						InitInputItem(config);
						break;
					case "inputItem2":
						InitInputItem2(config);
						break;
				}
			}
		}

		private GameObject CreateClone(GameObject prefab)
		{
			GameObject clone = Object.Instantiate(prefab, _Nego_ScrollViewContent);
			clone.SetActive(true);
			return clone;
		}

		public void InitSwitchItem(Dictionary<string, object> config)
		{
			GameObject clone = CreateClone(_Nego_SwitchItem);
			this.AddChild<SwitchItem>(null, clone, config["desc"], config["yesCallback"],
			  config.ContainsKey("noCallback") ? config["noCallback"] : null);
		}

		public void InitInputItem(Dictionary<string, object> config)
		{
			GameObject clone = CreateClone(_Nego_InputItem1);
			this.AddChild<InputItem>(null, clone, config["desc"], config["yesCallbak"]);
		}

		public void InitInputItem2(Dictionary<string, object> config)
		{
			GameObject clone = CreateClone(_Nego_InputItem2);
			this.AddChild<InputItem2>(null, clone, config["desc"], config["yesCallback"]);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////
		public virtual void InitConfigList()
		{
		}

		protected override void _Destroy()
		{
			_configList.Clear();
			base._Destroy();
		}
	}
}