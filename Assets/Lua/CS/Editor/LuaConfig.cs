using System;
using System.Collections.Generic;
using System.IO;
using CsCat;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XLua;
using Object = UnityEngine.Object;

public static class LuaConfig
{
  [LuaCallCSharp]
  public static List<Type> static_lua_call_cs_list = new List<Type>
  {
    typeof(LogCat),
    typeof(Client),
    typeof(PoolCat),
    typeof(UnityObjectPoolCat),
    typeof(GameObjectPoolCat),
    typeof(AssetBundleManager),
    typeof(AssetCat),
    typeof(AutoAssetDestory),
    typeof(ActSequenceMono),
    typeof(ActSequence),
    typeof(Act),
    typeof(AudioManager),
    typeof(SingletonMaster),
    typeof(AudioMixerGroupInfo),
    typeof(UGUIEventListener),
    typeof(EUILayerName),
    typeof(UILayerConfig),
    typeof(UILayer),
    typeof(UINotifyManager),
    typeof(UILoadingPanel),
    typeof(UILanternNotifyPanel),
    typeof(UIShowLogoPanel),
    typeof(UIFadePanel),
    typeof(UIWaitingPanel),
    typeof(XLineRenderer),
    typeof(AutoAssetSetImageSprite),
    typeof(MoveManager),
    typeof(UILayerManager),
    typeof(UIManager),
    //////////////////////////////Const////////////////////////////////////////
    typeof(FilePathConst),
    typeof(SerializeDataConst),
    typeof(AudioMixerConst),
    typeof(UIConst),
    typeof(PoolCatConst),
    //////////////////////////////Util////////////////////////////////////////
    typeof(StdioUtil),
    typeof(FileUtilCat),
    typeof(StringUtilCat),
    typeof(CameraUtil),
    typeof(TimeUtil),
    typeof(UIUtil),
    typeof(GameObjectUtil),
    //////////////////////////////Extension////////////////////////////////////////
    typeof(Vector3Extension),
    typeof(UnityEngineObjectExtension2),
    typeof(TransformExtension),
    typeof(GameObjectExtension),
    typeof(ComponentExtensions),
    typeof(ObjectExtension_xlua),
    typeof(AudioSourceExtension),
    typeof(TypeExtension),
    //////////////////////////////Interface////////////////////////////////////////
    typeof(IDespawn),
    //////////////////////////////Stand API////////////////////////////////////////
    typeof(File),
    typeof(Type),
    //////////////////////////////Unity API///////////////////////////////////////
    typeof(GUI),
    typeof(Rect),
    typeof(GUIStyle),
    typeof(TextAnchor),
    typeof(Screen),
    typeof(Material),
    typeof(MeshRenderer),
    typeof(TrailRenderer),
    typeof(AsyncOperation),
    typeof(SceneManager),
    typeof(LoadSceneMode),
    typeof(Animation),
    typeof(AnimationState),
    typeof(AnimationCullingType),
    typeof(Animator),
    typeof(AnimatorControllerParameter),
    typeof(WrapMode),
    typeof(AnimatorControllerParameterType),
    typeof(RuntimePlatform),
    typeof(KeyCode),
    typeof(Input),
    typeof(RectTransformUtility),
    typeof(Application),
    typeof(Component),
    typeof(Canvas),
    typeof(GraphicRaycaster),
    typeof(GameObject),
    typeof(Camera),
    typeof(Mathf),
    typeof(Vector3),
    typeof(Quaternion),
    typeof(Object),
    typeof(CanvasScaler),
    typeof(RenderMode),
    typeof(CanvasScaler.ScaleMode),
    typeof(CanvasScaler.ScreenMatchMode),
    typeof(Text),
    typeof(Image),
    typeof(Button),
    typeof(InputField),
    typeof(Slider),
    typeof(ScrollRect),
    typeof(Transform),
    typeof(RectTransform),
    typeof(Time),
    typeof(RaycastHit),
    typeof(Ray),
    typeof(Bounds),
    typeof(Touch),
    typeof(TouchPhase),
    typeof(Vector4),
    typeof(LayerMask),
    typeof(Color),
    typeof(Plane),
    typeof(Vector2),
    typeof(Vector2Int),
    typeof(Graphic),
    typeof(AudioListener),
    typeof(AudioSource),
    typeof(AudioClip),
    typeof(AudioMixer),
    typeof(Sprite),
    typeof(RawImage),
    typeof(Texture),
    typeof(Canvas),
    //////////////////////////////////Plugin//////////////////////////////////////////
    typeof(AutoPlay),
    typeof(AxisConstraint),
    typeof(Ease),
    typeof(LogBehaviour),
    typeof(LoopType),
    typeof(PathMode),
    typeof(PathType),
    typeof(RotateMode),
    typeof(ScrambleMode),
    typeof(TweenType),
    typeof(UpdateType),

    typeof(DOTween),
    typeof(DOVirtual),
    typeof(EaseFactory),
    typeof(Tweener),
    typeof(Tween),
    typeof(Sequence),
    typeof(TweenParams),
    typeof(ABSSequentiable),
    typeof(DOTweenModuleUI),

    typeof(TweenerCore<Vector3, Vector3, VectorOptions>),

    typeof(TweenCallback),
    typeof(TweenExtensions),
    typeof(TweenSettingsExtensions),
    typeof(ShortcutExtensions)
  };

  [LuaCallCSharp]
  //    public static List<Type> dynamic_lua_call_cs_list
  //    {
  //        get
  //        {
  //            List<Type> result = new List<Type>();
  //            result.AddRange(from type in  Assembly.GetAssembly(typeof(GameObject)).GetTypes()
  //                select type);
  //            result.AddRange(from type in Assembly.GetAssembly(typeof(Image)).GetTypes()
  //                    select type);
  //            result.Remove(typeof(Unity.Collections.LowLevel.Unsafe.UnsafeUtility));
  //            return result;
  //        }
  //    }
  [CSharpCallLua]
  public static List<Type> static_cs_call_lua_list = new List<Type>
  {
    //Delegate
    typeof(Action),
    typeof(Action<int>),
    typeof(Action<float>),
    typeof(Action<float, float>),
    typeof(Action<AssetCat>),
    typeof(Action<Act>),
    typeof(Action<Action>),
    typeof(Func<float>),
    typeof(Func<Vector3>)
  };

  [BlackList]
  public static List<List<string>> BlackList = new List<List<string>>
  {
    new List<string> {"UnityEngine.UI.Text", "OnRebuildRequested"},
    new List<string> {"UnityEngine.UI.Graphic", "OnRebuildRequested"},
    new List<string> {"UnityEngine.Input", "IsJoystickPreconfigured", "System.String"},
    new List<string> {"UnityEngine.Light", "SetLightDirty"},
    new List<string> {"UnityEngine.Light", "shadowRadius"},
    new List<string> {"UnityEngine.Light", "shadowAngle"},
    new List<string> {"UnityEngine.Texture", "imageContentsHash"},
    new List<string> { "UnityEngine.MeshRenderer", "receiveGI"},
    new List<string> { "UnityEngine.MeshRenderer", "stitchLightmapSeams"},
    new List<string> { "UnityEngine.MeshRenderer", "scaleInLightmap"},
    new List<string> { "UnityEngine.AnimatorControllerParameter", "name"},
    new List<string>{"System.Type", "IsSZArray" },
    new List<string>{"System.Type", "GetMiniTypeThumbnail" },
  };
}