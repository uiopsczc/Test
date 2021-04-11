using UnityEngine;

namespace CsCat
{
  public class CameraUtil
  {

    public static Vector2 GetRectSizeByDistance(Camera camera, float distance)
    {
      float tan_of_fov = Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);

      float hight_half = tan_of_fov * distance;
      float width_half = hight_half * ResolutionUtil.GetResolution().x / ResolutionUtil.GetResolution().y;

      return new Vector2(width_half * 2, hight_half * 2);
    }

    public static Rectangle3d GetRectOfLocalByDistance(Camera camera, float distance, Vector2 off_percent)
    {
      Vector2 rect_size = GetRectSizeByDistance(camera, distance);
      rect_size *= (Vector2.one - off_percent);
      Vector3 center = Vector3.forward * distance;

      Rectangle3d rect = new Rectangle3d(center, rect_size, Matrix4x4Const.xz_to_xy_matrix);
      return rect;
    }

    public static Rectangle3d GetRectOfLocalByDistance(Camera camera, float distance)
    {
      return GetRectOfLocalByDistance(camera, distance, Vector2.zero);
    }


    public static Rectangle3d GetRectOfWorldByDistance(Camera camera, float distance)
    {
      return GetRectOfWorldByDistance(camera, distance, Vector2.zero);
    }

    public static Rectangle3d GetRectOfWorldByDistance(Camera camera, float distance, Vector2 off_percent)
    {
      //有严格的顺序，这个worldRect，先转为世界坐标系，然后再平移center，最后翻转
      Rectangle3d rect = GetRectOfLocalByDistance(camera, distance, off_percent);
      rect.PreMultiplyMatrix(camera.transform.localToWorldMatrix);
      return rect;
    }

    ////////////////////////////////////////////////ToUIPos///////////////////////////////////////////
    //世界坐标转UI坐标
    public static Vector2 WorldToUIPos(RectTransform canvas_rectTransform, Camera world_camera, Vector3 worldPosition,
      Vector2? uiPosPivot = null, Vector2 offset = default(Vector2))
    {
      canvas_rectTransform = canvas_rectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
      //世界坐标-》ViewPort坐标
      Vector2 viewport_pos = world_camera.WorldToViewportPoint(worldPosition);
      return ViewPortToUIPos(canvas_rectTransform, viewport_pos, uiPosPivot, offset);
    }

    //屏幕坐标转UI坐标
    public static Vector2 ScreenToUIPos(RectTransform canvas_rectTransform, Camera screen_camera, Vector3 screenPoint,
      Vector2? uiPosPivot = null, Vector2 offset = default(Vector2))
    {
      canvas_rectTransform = canvas_rectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
      screen_camera = screen_camera ?? Client.instance.uiManager.uiCamera;
      //屏幕坐标 -》ViewPort坐标
      Vector2 viewport_pos = screen_camera.ScreenToViewportPoint(screenPoint);
      return ViewPortToUIPos(canvas_rectTransform, viewport_pos, uiPosPivot, offset);
    }

    //ViewPort坐标转UI坐标
    public static Vector2 ViewPortToUIPos(RectTransform canvas_rectTransform, Vector3 viewport_pos,
      Vector2? uiPosPivot = null, Vector2 offset = default(Vector2))
    {
      canvas_rectTransform = canvas_rectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
      // uiPosPivot_x =0.5,uiPosPivot_y = 0.5 MiddleCenter
      // uiPosPivot_x =0.5,uiPosPivot_y = 0 MiddleBottom
      // uiPosPivot_x =0.5,uiPosPivot_y = 1 MiddleTop

      // uiPosPivot_x =0,uiPosPivot_y = 0.5 LeftCenter
      // uiPosPivot_x =0,uiPosPivot_y = 0 LeftBottom
      // uiPosPivot_x =0,uiPosPivot_y = 1 LeftTop

      // uiPosPivot_x =1,uiPosPivot_y = 0.5 RightCenter
      // uiPosPivot_x =1,uiPosPivot_y = 0 RightBottom
      // uiPosPivot_x =1,uiPosPivot_y = 1 RightTop
      Vector2 _uiPosPivot = uiPosPivot.GetValueOrDefault(new Vector2(0.5f, 0.5f)); // middle-center
      viewport_pos = viewport_pos.ToVector2() - _uiPosPivot;
      viewport_pos = viewport_pos.ToVector2() + offset;

      //ViewPort坐标-〉UGUI坐标
      return new Vector2(canvas_rectTransform.rect.width * viewport_pos.x,
        canvas_rectTransform.rect.height * viewport_pos.y);
    }

    ////////////////////////////////////////////////ToWorldPos///////////////////////////////////////////
    //UI坐标转世界坐标
    public static Vector2 UIPosToWorldPos(RectTransform canvas_rectTransform, Camera world_camera, Vector2 ui_pos,
      Vector2? uiPosPivot = null, float viewprot_z = 0, Vector2 viewprot_offset = default(Vector2))
    {
      canvas_rectTransform = canvas_rectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
      Vector3 viewport_pos = UIPosToViewPortPos(canvas_rectTransform, ui_pos, uiPosPivot, 0, viewprot_offset);
      viewport_pos = new Vector3(viewport_pos.x, viewport_pos.y, viewprot_z);
      //ViewPort坐标 -》 世界坐标
      return world_camera.ViewportToWorldPoint(viewport_pos);
    }

    ////////////////////////////////////////////////ToScreenPos///////////////////////////////////////////
    //UI坐标转屏幕坐标
    public static Vector2 UIPosToScreenPos(RectTransform canvas_rectTransform, Camera screen_camera, Vector2 ui_pos,
      Vector2? uiPosPivot = null, Vector2 viewprot_offset = default(Vector2))
    {
      canvas_rectTransform = canvas_rectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
      screen_camera = screen_camera ?? Client.instance.uiManager.uiCamera;
      Vector3 viewport_pos = UIPosToViewPortPos(canvas_rectTransform, ui_pos, uiPosPivot, 0, viewprot_offset);
      //ViewPort坐标-》屏幕坐标
      Vector2 screen_pos = screen_camera.ViewportToScreenPoint(viewport_pos);
      return screen_pos;
    }

    ////////////////////////////////////////////////ToViewPortPos///////////////////////////////////////////
    //UI坐标转ViewPort坐标
    public static Vector3 UIPosToViewPortPos(RectTransform canvas_rectTransform, Vector2 ui_pos,
      Vector2? uiPosPivot = null, float viewprot_z = 0, Vector2 viewprot_offset = default(Vector2))
    {
      canvas_rectTransform = canvas_rectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
      // uiPosPivot_x =0.5,uiPosPivot_y = 0.5 MiddleCenter
      // uiPosPivot_x =0.5,uiPosPivot_y = 0 MiddleBottom
      // uiPosPivot_x =0.5,uiPosPivot_y = 1 MiddleTop

      // uiPosPivot_x =0,uiPosPivot_y = 0.5 LeftCenter
      // uiPosPivot_x =0,uiPosPivot_y = 0 LeftBottom
      // uiPosPivot_x =0,uiPosPivot_y = 1 LeftTop

      // uiPosPivot_x =1,uiPosPivot_y = 0.5 RightCenter
      // uiPosPivot_x =1,uiPosPivot_y = 0 RightBottom
      // uiPosPivot_x =1,uiPosPivot_y = 1 RightTop
      Vector2 _uiPosPivot = uiPosPivot.GetValueOrDefault(new Vector2(0.5f, 0.5f)); // middle-center
      //UGUI坐标 -〉ViewPort
      Vector2 viewport_pos = new Vector2(ui_pos.x / canvas_rectTransform.rect.width,
        ui_pos.y / canvas_rectTransform.rect.height);
      viewport_pos = viewport_pos + _uiPosPivot;
      viewport_pos = viewport_pos + viewprot_offset;
      return new Vector3(viewport_pos.x, viewport_pos.y, viewprot_z);
    }

    //public Polygon[] GetScreenPolygons(Bounds bounds, Camera camera)
    //{
    //    Polygon[] polygons = new Polygon[6];
    //    List<Vector2> pts = new List<Vector2>();

    //    //topPlane
    //    pts.Clear();
    //    pts.Add(GetScreenPoint(bounds.frontTopLeft(camera.transform.rotation), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.frontTopRight(camera.transform.rotation), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backTopRight(camera.transform.rotation), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backTopLeft(camera.transform.rotation), camera).ToVector2()); 
    //    polygons[0] = new Polygon(pts);

    //    //bottomPlane
    //    pts.Clear();
    //    pts.Add(GetScreenPoint(bounds.frontBottomLeft(camera.transform.rotation), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.frontBottomRight(camera.transform.rotation), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backBottomRight(camera.transform.rotation), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backBottomLeft(camera.transform.rotation), camera).ToVector2());
    //    polygons[1] = new Polygon(pts);


    //    //frontPlane
    //    pts.Clear();
    //    pts.Add(GetScreenPoint(bounds.frontTopLeft(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.frontTopRight(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.frontBottomRight(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.frontBottomLeft(), camera).ToVector2());
    //    polygons[2] = new Polygon(pts);

    //    //backPlane
    //    pts.Clear();
    //    pts.Add(GetScreenPoint(bounds.backTopLeft(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backTopRight(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backBottomRight(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backBottomLeft(), camera).ToVector2());
    //    polygons[3] = new Polygon(pts);

    //    //leftPlane
    //    pts.Clear();
    //    pts.Add(GetScreenPoint(bounds.frontTopLeft(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backTopLeft(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backBottomLeft(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.frontBottomLeft(), camera).ToVector2());
    //    polygons[4] = new Polygon(pts);

    //    //rightPlane
    //    pts.Clear();
    //    pts.Add(GetScreenPoint(bounds.frontTopRight(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backTopRight(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.backBottomRight(), camera).ToVector2());
    //    pts.Add(GetScreenPoint(bounds.frontBottomRight(), camera).ToVector2());
    //    polygons[5] = new Polygon(pts);

    //    return polygons;

    //}
  }
}