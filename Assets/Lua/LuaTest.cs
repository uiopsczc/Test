using System.Collections;
using CsCat;
using UnityEngine;

public class LuaTest : MonoBehaviour
{
	private void Start()
	{
		if (Application.isEditor)
			EditorModeConst.IsEditorMode = true;
		StartCoroutine(Init());
	}

	private IEnumerator Init()
	{
		yield return InitResource();
		DoLuaLogic();
	}

	private IEnumerator InitResource()
	{
		yield return Client.instance.assetBundleUpdater.CheckUpdate();
		yield return Client.instance.assetBundleManager.IEInit();
		yield return LuaRequireLoader.LoadLuaFiles();
	}

	private void DoLuaLogic()
	{
		XLuaManager.instance.OnInit();
		//        XLuaManager.Instance.LuaEnv.DoString(@"
		//            UIManager.GetInstance():OpenWindow('UITest2');
		//            ");
	}
}