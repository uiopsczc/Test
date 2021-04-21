namespace CsCat
{
  public static class UIBloodTest
  {
    public static UIBloodTestParent uiBloodTestParent;

    public static void Test1()
    {
      uiBloodTestParent = Client.instance.AddChild<UIBloodTestParent>(null, "aa", 3);
    }

    public static void Test2()
    {
      uiBloodTestParent.SlideTo(40);
    }

    public static void Test3()
    {
      uiBloodTestParent.Reset();
    }

    public static void Test4()
    {
      uiBloodTestParent = Client.instance.AddChild<UIBloodTestParent>(null, "bb", 2);
    }
  }
}