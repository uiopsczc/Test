using UnityEngine;

namespace CsCat
{
    public class ConstUtil
    {
        public static string GetRootPath()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    return Application.persistentDataPath + "/";
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                    ///*如果是电脑的编辑模式，先放在项目外面*/
                    return Application.dataPath.Replace("Assets", "");
                //return Application.dataPath + "/Resources/";
                default:
                    return Application.dataPath + "/";
            }
        }
    }
}