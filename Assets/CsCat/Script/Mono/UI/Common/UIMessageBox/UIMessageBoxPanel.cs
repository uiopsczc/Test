using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIMessageBoxPanel : UIPopUpPanel
	{

		private Text title_text;
		private Text subtitle_text;
		private Text desc_text;
		private ScrollRect desc_scrollRect;
		private Transform items_transform;
		private ScrollRect items_scrollRect;
		private Transform item_parent_transform;
		private Button close_btn;
		private Button button1;
		private Text button1_text;
		private Button button2;
		private Text button2_text;
		private Action close_callback;

		public override void Init()
		{
			base.Init();
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIMessageBoxPanel.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			title_text = this.content_transform.FindComponentInChildren<Text>("title");
			subtitle_text = this.content_transform.FindComponentInChildren<Text>("subtitle");
			desc_text =
			  this.content_transform.FindComponentInChildren<Text>("content/desc/Scroll View/Viewport/Content/desc");
			desc_scrollRect = this.content_transform.FindComponentInChildren<ScrollRect>("content/desc/Scroll View");
			item_parent_transform = this.content_transform.Find("content/items/Scroll View/Viewport/Content");
			items_transform = this.content_transform.Find("content/items");
			items_scrollRect = this.content_transform.FindComponentInChildren<ScrollRect>("content/items/Scroll View");
			close_btn = this.content_transform.FindComponentInChildren<Button>("close");
			button1 = this.content_transform.FindComponentInChildren<Button>("buttons/button1");
			button1_text = this.content_transform.FindComponentInChildren<Text>("buttons/button1/text");
			button2 = this.content_transform.FindComponentInChildren<Button>("buttons/button2");
			button2_text = this.content_transform.FindComponentInChildren<Text>("buttons/button2/text");
		}

		public void Show(string title, string sub_title, string desc,
		  List<Dictionary<string, int>> itemInfo_dict_list = null, string button1_desc = null,
		  Action button1_callback = null, string button2_desc = null, Action button2_callback = null, Action close_callback = null)
		{
			graphicComponent.SetIsShow(true);
			this.title_text.text = title;
			this.subtitle_text.text = sub_title;
			this.desc_text.text = desc;
			this.desc_scrollRect.verticalNormalizedPosition = 1;
			items_transform.gameObject.SetActive(!itemInfo_dict_list.IsNullOrEmpty());
			if (!itemInfo_dict_list.IsNullOrEmpty())
			{
				foreach (var itemInfo_dict in itemInfo_dict_list)
				{
					int id = itemInfo_dict["id"];
					int count = itemInfo_dict["count"];
					UIItemBase item = this.AddChild<UIItemBase>(null, this.item_parent_transform);
					item.InvokeAfterAllAssetsLoadDone(() =>
					{
						item.Show(id.ToString(), count);
						StartCoroutine(IEScrollRectSetVerticalPosition(this.items_scrollRect, 1));
					});
				}
			}

			if (button1_callback != null)
			{
				button1.gameObject.SetActive(true);
				button1_text.text = button1_desc;
				this.RegisterOnClick(button1, button1_callback);
			}
			else
				button1.gameObject.SetActive(false);

			if (button2_callback != null)
			{
				button2.gameObject.SetActive(true);
				button2_text.text = button2_desc;
				this.RegisterOnClick(button2, button2_callback);
			}
			else
				button2.gameObject.SetActive(false);

			this.close_callback = close_callback;
			this.RegisterOnClick(this.close_btn, Close);
		}

		IEnumerator IEScrollRectSetVerticalPosition(ScrollRect scrollRect, float value)
		{
			yield return null;
			scrollRect.verticalNormalizedPosition = value;
		}

		public override void Close()
		{
			base.Close();
			this.close_callback?.Invoke();
		}
	}
}