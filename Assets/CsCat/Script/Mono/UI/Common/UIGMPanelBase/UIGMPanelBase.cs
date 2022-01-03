using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UIGMPanelBase : UIPopUpPanel
	{

		protected List<Dictionary<string, object>> config_list = new List<Dictionary<string, object>>();
		protected GameObject switch_item_prefab;
		protected GameObject input_item_prefab;
		protected GameObject input_item2_prefab;
		protected GameObject close_btn_gameObject;
		protected Transform inner_content_transform;

		public override void Init()
		{
			base.Init();
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIGMPanelBase.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			inner_content_transform = this.frame_transform.Find("content/Scroll View/Viewport/Content");
			switch_item_prefab = inner_content_transform.Find("switch_item").gameObject;
			input_item_prefab = inner_content_transform.Find("input_item").gameObject;
			input_item2_prefab = inner_content_transform.Find("input_item_2").gameObject;
			close_btn_gameObject = this.frame_transform.Find("content/close_btn").gameObject;

			this.RegisterOnClick(close_btn_gameObject, () => this.Close());

			InitConfigList();
			InitItems();
		}


		public void InitItems()
		{
			foreach (Dictionary<string, object> config in config_list)
			{
				switch (config["type"])
				{
					case "switch_item":
						InitSwitchItem(config);
						break;
					case "input_item":
						InitInputItem(config);
						break;
					case "input_item_2":
						InitInputItem2(config);
						break;
				}
			}
		}

		private GameObject CreateClone(GameObject prefab)
		{
			GameObject clone = GameObject.Instantiate(prefab, inner_content_transform);
			clone.SetActive(true);
			return clone;
		}

		public void InitSwitchItem(Dictionary<string, object> config)
		{
			GameObject clone = CreateClone(switch_item_prefab);
			this.AddChild<SwitchItem>(null, clone, config["desc"], config["yes_callback"],
			  config.ContainsKey("no_callback") ? config["no_callback"] : null);
		}

		public void InitInputItem(Dictionary<string, object> config)
		{
			GameObject clone = CreateClone(input_item_prefab);
			this.AddChild<InputItem>(null, clone, config["desc"], config["yes_callbak"]);
		}

		public void InitInputItem2(Dictionary<string, object> config)
		{
			GameObject clone = CreateClone(input_item2_prefab);
			this.AddChild<InputItem2>(null, clone, config["desc"], config["yes_callback"]);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////
		public virtual void InitConfigList()
		{
		}

		protected override void _Destroy()
		{
			base._Destroy();
			config_list.Clear();
		}
	}
}