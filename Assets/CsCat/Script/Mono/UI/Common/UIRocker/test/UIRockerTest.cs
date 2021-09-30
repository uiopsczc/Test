using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public static class UIRockerTest
  {
    private static GameObject unit_gameObject;
    private static RectTransform unit_rectTransform;
    private static Text unit_text;
    private static UIRockerInput rockerInput;
    private static UIRocker uiRocker;

    public static void Test()
    {
      UIRockerTest.unit_gameObject = GameObject.Find("UITestPanel").NewChildWithImage("unit").gameObject;
      UIRockerTest.unit_text =
        UIRockerTest.unit_gameObject.NewChildWithText("state", null, 40, Color.black, TextAnchor.MiddleCenter, null);
      UIRockerTest.unit_rectTransform = UIRockerTest.unit_gameObject.GetComponent<RectTransform>();
      UIRockerTest.rockerInput = Client.instance.AddChild<UIRockerInput>(null);
      UIRockerTest.uiRocker = Client.instance.AddChild<UIRocker>(null, null,
        GameObject.Find("UITestPanel").transform,
        UIRockerTest.rockerInput);

      Client.instance.AddListener<float, float>(null, UIRockerTest.rockerInput.event_name_move_pct, UIRockerTest.MovePct);
      Client.instance.AddListener(null, UIRockerTest.rockerInput.event_name_move_stop, UIRockerTest.MoveStop);
    }

    public static void MovePct(float pct_x, float pct_y)
    {
      Vector3 localPosition = UIRockerTest.unit_rectTransform.localPosition;
      UIRockerTest.unit_rectTransform.localPosition =
        new Vector3(localPosition.x + pct_x, localPosition.y + pct_y, localPosition.z);
      UIRockerTest.unit_text.text = "Move";
    }

    public static void MoveStop()
    {
      UIRockerTest.unit_text.text = "Stop";
    }
  }
}