using UnityEngine;

namespace CsCat
{
  public class ConstUtil
  {
    public const string DOTweenId_Use_GameTime = "DoTweenIdUseGameTime";

    public static string root_path
    {
      get
      {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
          return Application.persistentDataPath + "/";
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor ||
                 Application.platform == RuntimePlatform.OSXEditor)
        {
          ///*如果是电脑的编辑模式，先放在项目外面*/
          return Application.dataPath.Replace("Assets", "");
          //return Application.dataPath + "/Resources/";
        }
        else
        {
          return Application.dataPath + "/";
        }
      }
    }





  }
}

