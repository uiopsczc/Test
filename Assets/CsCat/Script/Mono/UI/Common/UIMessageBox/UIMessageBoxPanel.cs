using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIMessageBoxPanel : UIPopUpPanel
	{

		private Text titleText;
		private Text subtitleText;
		private Text descText;
		private ScrollRect descScrollRect;
		private Transform itemsTransform;
		private ScrollRect itemsScrollRect;
		private Transform itemParentTransform;
		private Button closeBtn;
		private Button button1;
		private Text button1Text;
		private Button button2;
		private Text button2Text;
		private Action closeCallback;

		public override void Init()
		{
			base.Init();
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIMessageBoxPanel.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			titleText = this.contentTransform.FindComponentInChildren<Text>("title");
			subtitleText = this.contentTransform.FindComponentInChildren<Text>("subtitle");
			descText =
			  this.contentTransform.FindComponentInChildren<Text>("content/desc/Scroll View/Viewport/Content/desc");
			descScrollRect = this.contentTransform.FindComponentInChildren<ScrollRect>("content/desc/Scroll View");
			itemParentTransform = this.contentTransform.Find("content/items/Scroll View/Viewport/Content");
			itemsTransform = this.contentTransform.Find("content/items");
			itemsScrollRect = this.contentTransform.FindComponentInChildren<ScrollRect>("content/items/Scroll View");
			closeBtn = this.contentTransform.FindComponentInChildren<Button>("close");
			button1 = this.contentTransform.FindComponentInChildren<Button>("buttons/button1");
			button1Text = this.contentTransform.FindComponentInChildren<Text>("buttons/button1/text");
			button2 = this.contentTransform.FindComponentInChildren<Button>("buttons/button2");
			button2Text = this.contentTransform.FindComponentInChildren<Text>("buttons/button2/text");
		}

		public void Show(string title, string subTitle, string desc,
		  List<Dictionary<string, int>> itemInfoDictList = null, string button1Desc = null,
		  Action button1Callback = null, string button2Desc = null, Action button2Callback = null, Action closeCallback = null)
		{
			graphicComponent.SetIsShow(true);
			this.titleText.text = title;
			this.subtitleText.text = subTitle;
			this.descText.text = desc;
			this.descScrollRect.verticalNormalizedPosition = 1;
			itemsTransform.gameObject.SetActive(!itemInfoDictList.IsNullOrEmpty());
			if (!itemInfoDictList.IsNullOrEmpty())
			{
				for (var i = 0; i < itemInfoDictList.Count; i++)
				{
					var itemInfoDict = itemInfoDictList[i];
					int id = itemInfoDict["id"];
					int count = itemInfoDict["count"];
					UIItemBase item = this.AddChild<UIItemBase>(null, this.itemParentTransform);
					item.InvokeAfterAllAssetsLoadDone(() =>
					{
						item.Show(id.ToString(), count);
						StartCoroutine(IEScrollRectSetVerticalPosition(this.itemsScrollRect, 1));
					});
				}
			}

			if (button1Callback != null)
			{
				button1.gameObject.SetActive(true);
				button1Text.text = button1Desc;
				this.RegisterOnClick(button1, button1Callback);
			}
			else
				button1.gameObject.SetActive(false);

			if (button2Callback != null)
			{
				button2.gameObject.SetActive(true);
				button2Text.text = button2Desc;
				this.RegisterOnClick(button2, button2Callback);
			}
			else
				button2.gameObject.SetActive(false);

			this.closeCallback = closeCallback;
			this.RegisterOnClick(this.closeBtn, Close);
		}

		IEnumerator IEScrollRectSetVerticalPosition(ScrollRect scrollRect, float value)
		{
			yield return null;
			scrollRect.verticalNormalizedPosition = value;
		}

		public override void Close()
		{
			base.Close();
			this.closeCallback?.Invoke();
		}
	}
}