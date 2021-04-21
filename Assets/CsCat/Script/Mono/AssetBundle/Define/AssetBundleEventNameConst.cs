#if UNITY_EDITOR
#endif

namespace CsCat
{
  public static class AssetBundleEventNameConst
  {
    public const string BasePath = "AssetBundle";

    public const string On_ResourceWebRequester_Fail = BasePath + "On_ResourceWebRequester_Fail";
    public const string On_ResourceWebRequester_Success = BasePath + "On_ResourceWebRequester_Success";
    public const string On_ResourceWebRequester_Done = BasePath + "On_ResourceWebRequester_Done";


    public const string On_AssetBundleAsyncLoader_Fail = BasePath + "On_AssetBundleAsyncLoader_Fail";
    public const string On_AssetBundleAsyncLoader_Success = BasePath + "On_AssetBundleAsyncLoader_Success";
    public const string On_AssetBundleAsyncLoader_Done = BasePath + "On_AssetBundleAsyncLoader_Done";

    public const string On_AssetAsyncLoader_Fail = BasePath + "On_AssetAsyncLoader_Fail";
    public const string On_AssetAsyncLoader_Success = BasePath + "On_AssetAsyncLoader_Success";
    public const string On_AssetAsyncLoader_Done = BasePath + "On_AssetAsyncLoader_Done";

  }
}