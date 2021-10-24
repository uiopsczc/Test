using UnityEngine;

namespace CsCat
{
    public class CameraUtil
    {
        public static Vector2 GetRectSizeByDistance(Camera camera, float distance)
        {
            float tanOfFov = Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);

            float heightHalf = tanOfFov * distance;
            float widthHalf = heightHalf * ResolutionUtil.GetResolution().x / ResolutionUtil.GetResolution().y;

            return new Vector2(widthHalf * 2, heightHalf * 2);
        }

        public static Rectangle3D GetRectOfLocalByDistance(Camera camera, float distance, Vector2 offPercent)
        {
            Vector2 rectSize = GetRectSizeByDistance(camera, distance);
            rectSize *= (Vector2.one - offPercent);
            Vector3 center = Vector3.forward * distance;

            Rectangle3D rect = new Rectangle3D(center, rectSize, Matrix4x4Const.XZ_To_XY_Matrix);
            return rect;
        }

        public static Rectangle3D GetRectOfLocalByDistance(Camera camera, float distance)
        {
            return GetRectOfLocalByDistance(camera, distance, Vector2.zero);
        }


        public static Rectangle3D GetRectOfWorldByDistance(Camera camera, float distance)
        {
            return GetRectOfWorldByDistance(camera, distance, Vector2.zero);
        }

        public static Rectangle3D GetRectOfWorldByDistance(Camera camera, float distance, Vector2 offPercent)
        {
            //有严格的顺序，这个worldRect，先转为世界坐标系，然后再平移center，最后翻转
            Rectangle3D rect = GetRectOfLocalByDistance(camera, distance, offPercent);
            rect.PreMultiplyMatrix(camera.transform.localToWorldMatrix);
            return rect;
        }

        ////////////////////////////////////////////////ToUIPos///////////////////////////////////////////
        //世界坐标转UI坐标
        public static Vector2 WorldToUIPos(RectTransform canvasRectTransform, Camera worldCamera, Vector3 worldPosition,
            Vector2? uiPosPivot = null, Vector2 offset = default)
        {
            canvasRectTransform = canvasRectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
            //世界坐标-》ViewPort坐标
            Vector2 viewportPoint = worldCamera.WorldToViewportPoint(worldPosition);
            return ViewPortToUIPos(canvasRectTransform, viewportPoint, uiPosPivot, offset);
        }

        //屏幕坐标转UI坐标
        public static Vector2 ScreenToUIPos(RectTransform canvasRectTransform, Camera screenCamera, Vector3 screenPoint,
            Vector2? uiPosPivot = null, Vector2 offset = default)
        {
            canvasRectTransform = canvasRectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
            screenCamera = screenCamera ?? Client.instance.uiManager.uiCamera;
            //屏幕坐标 -》ViewPort坐标
            Vector2 viewportPos = screenCamera.ScreenToViewportPoint(screenPoint);
            return ViewPortToUIPos(canvasRectTransform, viewportPos, uiPosPivot, offset);
        }

        //ViewPort坐标转UI坐标
        public static Vector2 ViewPortToUIPos(RectTransform canvasRectTransform, Vector3 viewportPos,
            Vector2? uiPosPivot = null, Vector2 offset = default)
        {
            canvasRectTransform = canvasRectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
            // uiPosPivot_x =0.5,uiPosPivot_y = 0.5 MiddleCenter
            // uiPosPivot_x =0.5,uiPosPivot_y = 0 MiddleBottom
            // uiPosPivot_x =0.5,uiPosPivot_y = 1 MiddleTop

            // uiPosPivot_x =0,uiPosPivot_y = 0.5 LeftCenter
            // uiPosPivot_x =0,uiPosPivot_y = 0 LeftBottom
            // uiPosPivot_x =0,uiPosPivot_y = 1 LeftTop

            // uiPosPivot_x =1,uiPosPivot_y = 0.5 RightCenter
            // uiPosPivot_x =1,uiPosPivot_y = 0 RightBottom
            // uiPosPivot_x =1,uiPosPivot_y = 1 RightTop
            Vector2 uiPosPivotValue = uiPosPivot.GetValueOrDefault(Vector2Const.Half); // middle-center
            viewportPos = viewportPos.ToVector2() - uiPosPivotValue;
            viewportPos = viewportPos.ToVector2() + offset;

            //ViewPort坐标-〉UGUI坐标
            return new Vector2(canvasRectTransform.rect.width * viewportPos.x,
                canvasRectTransform.rect.height * viewportPos.y);
        }

        ////////////////////////////////////////////////ToWorldPos///////////////////////////////////////////
        //UI坐标转世界坐标
        public static Vector2 UIPosToWorldPos(RectTransform canvasRectTransform, Camera worldCamera, Vector2 uiPos,
            Vector2? uiPosPivot = null, float viewportZ = 0, Vector2 viewportOffset = default)
        {
            canvasRectTransform = canvasRectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
            Vector3 viewportPos = UIPosToViewPortPos(canvasRectTransform, uiPos, uiPosPivot, 0, viewportOffset);
            viewportPos = new Vector3(viewportPos.x, viewportPos.y, viewportZ);
            //ViewPort坐标 -》 世界坐标
            return worldCamera.ViewportToWorldPoint(viewportPos);
        }

        ////////////////////////////////////////////////ToScreenPos///////////////////////////////////////////
        //UI坐标转屏幕坐标
        public static Vector2 UIPosToScreenPos(RectTransform canvasRectTransform, Camera screenCamera, Vector2 uiPos,
            Vector2? uiPosPivot = null, Vector2 viewportOffset = default)
        {
            canvasRectTransform = canvasRectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
            screenCamera = screenCamera ?? Client.instance.uiManager.uiCamera;
            Vector3 viewportPos = UIPosToViewPortPos(canvasRectTransform, uiPos, uiPosPivot, 0, viewportOffset);
            //ViewPort坐标-》屏幕坐标
            Vector2 screenPos = screenCamera.ViewportToScreenPoint(viewportPos);
            return screenPos;
        }

        ////////////////////////////////////////////////ToViewPortPos///////////////////////////////////////////
        //UI坐标转ViewPort坐标
        public static Vector3 UIPosToViewPortPos(RectTransform canvasRectTransform, Vector2 uiPos,
            Vector2? uiPosPivot = null, float viewportZ = 0, Vector2 viewportOffset = default)
        {
            canvasRectTransform = canvasRectTransform ?? Client.instance.uiManager.uiCanvas_rectTransform;
            // uiPosPivot_x =0.5,uiPosPivot_y = 0.5 MiddleCenter
            // uiPosPivot_x =0.5,uiPosPivot_y = 0 MiddleBottom
            // uiPosPivot_x =0.5,uiPosPivot_y = 1 MiddleTop

            // uiPosPivot_x =0,uiPosPivot_y = 0.5 LeftCenter
            // uiPosPivot_x =0,uiPosPivot_y = 0 LeftBottom
            // uiPosPivot_x =0,uiPosPivot_y = 1 LeftTop

            // uiPosPivot_x =1,uiPosPivot_y = 0.5 RightCenter
            // uiPosPivot_x =1,uiPosPivot_y = 0 RightBottom
            // uiPosPivot_x =1,uiPosPivot_y = 1 RightTop
            Vector2 uiPosPivotValue = uiPosPivot.GetValueOrDefault(Vector2Const.Half); // middle-center
            //UGUI坐标 -〉ViewPort
            Vector2 viewportPos = new Vector2(uiPos.x / canvasRectTransform.rect.width,
                uiPos.y / canvasRectTransform.rect.height);
            viewportPos = viewportPos + uiPosPivotValue;
            viewportPos = viewportPos + viewportOffset;
            return new Vector3(viewportPos.x, viewportPos.y, viewportZ);
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