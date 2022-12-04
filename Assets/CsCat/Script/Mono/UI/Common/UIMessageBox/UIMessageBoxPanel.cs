using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIMessageBoxPanel : UIPopUpPanel
	{

		private Text _TxtC_Title;
		private Text _TxtC_Subtitle;
		private Text _TxtC_Desc;
		private ScrollRect _ScrollRect_Desc;
		private Transform _Nego_Items;
		private ScrollRect _ScrollRect_Items;
		private Transform _itemsParentTransform;
		private Button _Btn_Close;
		private Button _Btn1;
		private Text _TxtC_Btn1;
		private Button _Btn2;
		private Text _TxtC_Btn2;
		private Action closeCallback;

		protected override void _Init()
		{
			base._Init();
			this.SetPrefabPath("Assets/PatchResources/UI/UIMessageBox/Prefab/UIMessageBoxPanel.prefab");
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			_TxtC_Title = this._contentTransform.Find("TxtC_Title").GetComponent<Text>();
			_TxtC_Subtitle = this._contentTransform.Find("TxtC_Subtitle").GetComponent<Text>();
			_TxtC_Desc =
				this._contentTransform.Find("Nego_Desc/ScrollView_Desc/Viewport/Content/TxtC_Desc").GetComponent<Text>();
			_ScrollRect_Desc = this._contentTransform.Find("Neog_Content/Nego_Desc/ScrollView_Desc").GetComponent<ScrollRect>();
			_itemsParentTransform = this._contentTransform.Find("Nego_Items/ScrollView_Items/Viewport/Neog_ItemsContent");
			_Nego_Items = this._contentTransform.Find("Nego_Items");
			_ScrollRect_Items = this._contentTransform.Find("Nego_Items/ScrollView_Items").GetComponent<ScrollRect>();
			_Btn_Close = this._contentTransform.Find("Btn_Close").GetComponent<Button>();
			_Btn1 = this._contentTransform.Find("Nego_Buttons/Btn1").GetComponent<Button>();
			_TxtC_Btn1 = this._contentTransform.Find("Nego_Buttons/Btn1/TxtC_Btn1").GetComponent<Text>();
			_Btn2 = this._contentTransform.Find("Nego_Buttons/Btn2").GetComponent<Button>();
			_TxtC_Btn1 = this._contentTransform.Find("Nego_Buttons/Btn2/TxtC_Btn2").GetComponent<Text>();
		}

		public void Show(string title, string subTitle, string desc,
		  List<Dictionary<string, int>> itemInfoDictList = null, string button1Desc = null,
		  Action button1Callback = null, string button2Desc = null, Action button2Callback = null, Action closeCallback = null)
		{
			SetIsShow(true);
			this._TxtC_Title.text = title;
			this._TxtC_Subtitle.text = subTitle;
			this._TxtC_Desc.text = desc;
			this._ScrollRect_Desc.verticalNormalizedPosition = 1;
			_Nego_Items.gameObject.SetActive(!itemInfoDictList.IsNullOrEmpty());
			if (!itemInfoDictList.IsNullOrEmpty())
			{
				for (var i = 0; i < itemInfoDictList.Count; i++)
				{
					var itemInfoDict = itemInfoDictList[i];
					int id = itemInfoDict["id"];
					int count = itemInfoDict["count"];
					UIItemBase item = this.AddChild<UIItemBase>(null, this._itemsParentTransform);
					item.InvokeAfterPrefabLoadDone(() =>
					{
						item.Show(id.ToString(), count);
						this.GetChild<CoroutineDictTreeNode>().StartCoroutine(IEScrollRectSetVerticalPosition(this._ScrollRect_Items, 1));
					});
				}
			}

			if (button1Callback != null)
			{
				_Btn1.gameObject.SetActive(true);
				_TxtC_Btn1.text = button1Desc;
				this.RegisterOnClick(_Btn1, button1Callback);
			}
			else
				_Btn1.gameObject.SetActive(false);

			if (button2Callback != null)
			{
				_Btn2.gameObject.SetActive(true);
				_TxtC_Btn2.text = button2Desc;
				this.RegisterOnClick(_Btn2, button2Callback);
			}
			else
				_Btn2.gameObject.SetActive(false);

			this.closeCallback = closeCallback;
			this.RegisterOnClick(this._Btn_Close, Close);
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