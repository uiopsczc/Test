using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public partial class RedDotManager : GameEntity
  {
    public Dictionary<string, RedDotManager.ListenInnerDict>
      listen_dict =
        new Dictionary<string, RedDotManager.ListenInnerDict>(); // dict<tag, dict<"listener" = a_listener, "red_dot_info_dict" = dict<gameObject = red_dot_info>>>

    public Dictionary<GameObject, RedDotManager.RedDotInfo> red_dot_info_dict =
      new Dictionary<GameObject, RedDotManager.RedDotInfo>(); // dict<tag = dict<gameObject = red_dot_info>>



    public override void PostInit()
    {
      base.PostInit();
      this.AddTimer((args) =>
      {
        this.CheckCleanRedDotInfoDict();
        return true;
      }, 0, 5);
    }

    private void CheckCleanRedDotInfoDict()
    {
      this.red_dot_info_dict.RemoveByFunc((gameObject, red_dot_info) =>
      {
        if (gameObject == null)
        {
          var tag = red_dot_info.tag;
          if (tag != null)
          {
            var listenInnerDict = this.listen_dict[tag];
            listenInnerDict.red_dot_info_dict.Remove(gameObject);
            if (listenInnerDict.red_dot_info_dict.IsNullOrEmpty())
            {
              this.RemoveListener(listenInnerDict.listener);
              this.listen_dict.Remove(tag);
            }
          }

          return true;
        }

        return false;
      });
    }

    public void CleanListenEvent(string tag)
    {
      RedDotManager.ListenInnerDict listenInnerDict = this.listen_dict.Remove2<RedDotManager.ListenInnerDict>(tag);
      if (listenInnerDict != null && listenInnerDict.listener != null)
      {
        this.RemoveListener(listenInnerDict.listener);
        listenInnerDict.red_dot_info_dict.RemoveByFunc((gameObject, red_dot_info) =>
        {
          if (gameObject != null)
          {
            this.red_dot_info_dict.Remove(gameObject);

            var red_dot_transform = gameObject.transform.Find(RedDotConst.Red_Dot_Name);
            if (red_dot_transform != null)
              red_dot_transform.gameObject.SetActive(false);
          }

          return true;
        });
      }
    }

    public void CleanListenEvent(GameObject gameObject)
    {
      var red_dot_info = this.red_dot_info_dict.Remove2<RedDotManager.RedDotInfo>(gameObject);
      if (red_dot_info != null)
      {
        RedDotManager.ListenInnerDict listenInnerDict = this.listen_dict[red_dot_info.tag];
        listenInnerDict.red_dot_info_dict.Remove(gameObject);
        if (listenInnerDict.red_dot_info_dict.IsNullOrEmpty())
        {
          this.RemoveListener(listenInnerDict.listener);
          this.listen_dict.Remove(red_dot_info.tag);
        }

        var red_dot_transform = gameObject.transform.Find(RedDotConst.Red_Dot_Name);
        if (red_dot_transform != null)
          red_dot_transform.gameObject.SetActive(false);
      }
    }

    public void AddRedDot(GameObject gameObject, string tag, Hashtable image_params = null,
      params object[] check_func_params)
    {
      if (this.red_dot_info_dict.ContainsKey(gameObject))
        return;
      var info = Client.instance.redDotLogic.GetRedDotInfoByTag(tag);
      var red_dot_info = new RedDotManager.RedDotInfo(gameObject, tag, info.check_func, image_params ?? new Hashtable(),
        check_func_params);
      this.__ListenEvent(tag, red_dot_info);
      this.red_dot_info_dict[gameObject] = red_dot_info;
    }

    private void __ListenEvent(string tag, RedDotManager.RedDotInfo red_dot_info)
    {
      if (tag.IsNullOrWhiteSpace())
        return;
      if (!this.listen_dict.ContainsKey(tag))
      {
        var listenInnerDict = this.listen_dict.GetOrAddDefault(tag, () => new RedDotManager.ListenInnerDict());
        listenInnerDict.listener = this.AddListener(tag, () => { this.RefreshRedDot(red_dot_info); });
      }

      this.listen_dict[tag].red_dot_info_dict[red_dot_info.gameObject] = red_dot_info;
    }

    private void RefreshRedDot(RedDotManager.RedDotInfo red_dot_info)
    {
      var gameObject = red_dot_info.gameObject;
      if (gameObject == null)
        return;
      var check_func = red_dot_info.check_func;
      var check_func_params = red_dot_info.check_func_params;
      var is_active = check_func.Invoke<bool>(check_func_params);
      var image_params = red_dot_info.image_params;
      this.AddRedDotImage(gameObject, is_active, image_params);
    }

    private Image AddRedDotImage(GameObject item_gameObject, bool is_active, Hashtable image_params)
    {
      Transform red_dot_transform = item_gameObject.transform.Find(RedDotConst.Red_Dot_Name);
      if (red_dot_transform != null)
      {
        red_dot_transform.gameObject.SetActive(is_active);
        return red_dot_transform.GetComponent<Image>();
      }
      else
      {
        //设置位置
        float x_offset = image_params.ContainsKey("x_offset") ? image_params.Get<float>("x_offset") : 0;
        float y_offset = image_params.ContainsKey("y_offset") ? image_params.Get<float>("y_offset") : 0;
        Image red_dot_image = item_gameObject.NewChildWithImage(RedDotConst.Red_Dot_Name);
        red_dot_image.raycastTarget = false;
        var red_dot_rectTransform = red_dot_image.GetComponent<RectTransform>();
        //设置red_dot_rectTransform的(0, 0)为基于父节点的右上角
        red_dot_rectTransform.anchorMax = Vector2.one;
        red_dot_rectTransform.anchorMin = Vector2.one;
        red_dot_rectTransform.pivot = Vector2.one;
        bool is_setNativeSize = !image_params.ContainsKey("width") || !image_params.ContainsKey("height");
        red_dot_rectTransform.gameObject.SetActive(false);
        AutoAssetSetImageSprite.Set(red_dot_image, RedDotConst.Red_Dot_Image_AssetPath, is_setNativeSize,
          is_setNativeSize
            ? Vector2.zero
            : new Vector2(image_params.Get<float>("width"), image_params.Get<float>("height")),
          (image, sprite) =>
          {
            if (image.gameObject == null)
              return;
            red_dot_rectTransform.gameObject.SetActive(is_active);
            var sizeDelta = red_dot_rectTransform.sizeDelta;
            red_dot_rectTransform.anchoredPosition = new Vector2(x_offset, y_offset);
            //          red_dot_rectTransform.anchoredPosition = new Vector2(sizeDelta.x / 2 + x_offset, sizeDelta.y / 2 + y_offset);
          });
        return red_dot_image;
      }
    }
  }
}