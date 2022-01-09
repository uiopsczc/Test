using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public static class UIRockerTest
	{
		private static GameObject unitGameObject;
		private static RectTransform unitRectTransform;
		private static Text unitText;
		private static UIRockerInput rockerInput;
		private static UIRocker uiRocker;

		public static void Test()
		{
			UIRockerTest.unitGameObject = GameObject.Find("UITestPanel").NewChildWithImage("unit").gameObject;
			UIRockerTest.unitText =
				UIRockerTest.unitGameObject.NewChildWithText("state", null, 40, Color.black, TextAnchor.MiddleCenter,
					null);
			UIRockerTest.unitRectTransform = UIRockerTest.unitGameObject.GetComponent<RectTransform>();
			UIRockerTest.rockerInput = Client.instance.AddChild<UIRockerInput>(null);
			UIRockerTest.uiRocker = Client.instance.AddChild<UIRocker>(null, null,
				GameObject.Find("UITestPanel").transform,
				UIRockerTest.rockerInput);

			Client.instance.AddListener<float, float>(null, UIRockerTest.rockerInput.eventNameMovePCT,
				UIRockerTest.MovePct);
			Client.instance.AddListener(null, UIRockerTest.rockerInput.eventNameMoveStop, UIRockerTest.MoveStop);
		}

		public static void MovePct(float pctX, float pctY)
		{
			Vector3 localPosition = UIRockerTest.unitRectTransform.localPosition;
			UIRockerTest.unitRectTransform.localPosition =
				new Vector3(localPosition.x + pctX, localPosition.y + pctY, localPosition.z);
			UIRockerTest.unitText.text = "Move";
		}

		public static void MoveStop()
		{
			UIRockerTest.unitText.text = "Stop";
		}
	}
}