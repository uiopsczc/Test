#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static partial class GUIStyleConst
  {
    private static GUISkin _editor_skin_dark;
    public static GUISkin editor_skin_dack
    {
      get
      {
        if (_editor_skin_dark == null)
          _editor_skin_dark = AssetDatabase.LoadAssetAtPath(FilePathConst.EditorAssetsPath + "DarkEditorSkin/DarkEditorSkin.guiskin", typeof(GUISkin)) as GUISkin;
        return _editor_skin_dark;
      }
    }



    private static GUIStyle _Graph_Delete_Button_Style;
    public static GUIStyle Graph_Delete_Button_Style
    {
      get
      {
        if (_Graph_Delete_Button_Style == null)
          _Graph_Delete_Button_Style = editor_skin_dack.FindStyle("PixelButton");
        return _Graph_Delete_Button_Style;
      }
    }
    private static GUIStyle _Graph_Info_Button_Style;
    public static GUIStyle Graph_Info_Button_Style
    {
      get
      {
        if (_Graph_Info_Button_Style == null)
          _Graph_Info_Button_Style = editor_skin_dack.FindStyle("InfoButton");
        return _Graph_Info_Button_Style;
      }
    }
    private static GUIStyle _Graph_Gizmo_Button_Style;
    public static GUIStyle Graph_Gizmo_Button_Style
    {
      get
      {
        if (_Graph_Gizmo_Button_Style == null)
          _Graph_Gizmo_Button_Style = editor_skin_dack.FindStyle("GizmoButton");
        return _Graph_Gizmo_Button_Style;
      }
    }
    private static GUIStyle _Box_Style;
    public static GUIStyle Box_Style
    {
      get
      {
        if (_Box_Style == null)
          _Box_Style = editor_skin_dack.FindStyle("box");
        return _Box_Style;
      }
    }

    private static GUIStyle _Graph_Box_Style;
    public static GUIStyle Graph_Box_Style
    {
      get
      {
        if (_Graph_Box_Style == null)
          _Graph_Box_Style = editor_skin_dack.FindStyle("PixelBox3");
        return _Graph_Box_Style;
      }
    }


    private static GUIStyle _Button_Style;
    public static GUIStyle Button_Style
    {
      get
      {
        if (_Button_Style == null)
          _Button_Style = editor_skin_dack.FindStyle("button");
        return _Button_Style;
      }
    }
    private static GUIStyle _Toggle_Style;
    public static GUIStyle Toggle_Style
    {
      get
      {
        if (_Toggle_Style == null)
          _Toggle_Style = editor_skin_dack.FindStyle("toggle");
        return _Toggle_Style;
      }
    }
    private static GUIStyle _Label_Style_Dark;
    public static GUIStyle Label_Style_Dark
    {
      get
      {
        if (_Label_Style_Dark == null)
          _Label_Style_Dark = editor_skin_dack.FindStyle("label");
        return _Label_Style_Dark;
      }
    }
    private static GUIStyle _Textfield_Style;
    public static GUIStyle Textfield_Style
    {
      get
      {
        if (_Textfield_Style == null)
          _Textfield_Style = editor_skin_dack.FindStyle("textfield");
        return _Textfield_Style;
      }
    }
    private static GUIStyle _Textarea_Style;
    public static GUIStyle Textarea_Style
    {
      get
      {
        if (_Textarea_Style == null)
          _Textarea_Style = editor_skin_dack.FindStyle("textarea");
        return _Textarea_Style;
      }
    }
    private static GUIStyle _Horizontal_Slider_Style;
    public static GUIStyle Horizontal_Slider_Style
    {
      get
      {
        if (_Horizontal_Slider_Style == null)
          _Horizontal_Slider_Style = editor_skin_dack.FindStyle("horizontalslider");
        return _Horizontal_Slider_Style;
      }
    }
    private static GUIStyle _Horizontal_Slider_Thumb_Style;
    public static GUIStyle Horizontal_Slider_Thumb_Style
    {
      get
      {
        if (_Horizontal_Slider_Thumb_Style == null)
          _Horizontal_Slider_Thumb_Style = editor_skin_dack.FindStyle("horizontalsliderthumb");
        return _Horizontal_Slider_Thumb_Style;
      }
    }
    private static GUIStyle _Vertical_Slider_Style;
    public static GUIStyle Vertical_Slider_Style
    {
      get
      {
        if (_Vertical_Slider_Style == null)
          _Vertical_Slider_Style = editor_skin_dack.FindStyle("verticalslider");
        return _Vertical_Slider_Style;
      }
    }
    private static GUIStyle _Horizontal_Scrollbar_Style;
    public static GUIStyle Horizontal_Scrollbar_Style
    {
      get
      {
        if (_Horizontal_Scrollbar_Style == null)
          _Horizontal_Scrollbar_Style = editor_skin_dack.FindStyle("horizontalscrollbar");
        return _Horizontal_Scrollbar_Style;
      }
    }
    private static GUIStyle _Vertical_Scrollbar_Style;
    public static GUIStyle Vertical_Scrollbar_Style
    {
      get
      {
        if (_Vertical_Scrollbar_Style == null)
          _Vertical_Scrollbar_Style = editor_skin_dack.FindStyle("verticalscrollbar");
        return Vertical_Scrollbar_Style;
      }
    }

    private static GUIStyle _Horizontal_Scrollbar_Thumb_Style;
    public static GUIStyle Horizontal_Scrollbar_Thumb_Style
    {
      get
      {
        if (_Horizontal_Scrollbar_Thumb_Style == null)
          _Horizontal_Scrollbar_Thumb_Style = editor_skin_dack.FindStyle("horizontalscrollbarthumb");
        return _Horizontal_Scrollbar_Thumb_Style;
      }
    }
    private static GUIStyle _Horizontal_Scrollbar_Left_Button_Style;
    public static GUIStyle Horizontal_Scrollbar_Left_Button_Style
    {
      get
      {
        if (_Horizontal_Scrollbar_Left_Button_Style == null)
          _Horizontal_Scrollbar_Left_Button_Style = editor_skin_dack.FindStyle("horizontalscrollbarleftbutton");
        return _Horizontal_Scrollbar_Left_Button_Style;
      }
    }
    private static GUIStyle _Pixel_Box_Style;
    public static GUIStyle Pixel_Box_Style
    {
      get
      {
        if (_Pixel_Box_Style == null)
          _Pixel_Box_Style = editor_skin_dack.FindStyle("PixelBox");
        return _Pixel_Box_Style;
      }
    }
    private static GUIStyle _Color_Interpolation_Box_Style;
    public static GUIStyle Color_Interpolation_Box_Style
    {
      get
      {
        if (_Color_Interpolation_Box_Style == null)
          _Color_Interpolation_Box_Style = editor_skin_dack.FindStyle("ColorInterpolationBox");
        return _Color_Interpolation_Box_Style;
      }
    }
    private static GUIStyle _Stretch_Width_Style;
    public static GUIStyle Stretch_Width_Style
    {
      get
      {
        if (_Stretch_Width_Style == null)
          _Stretch_Width_Style = editor_skin_dack.FindStyle("StretchWidth");
        return _Stretch_Width_Style;
      }
    }
    private static GUIStyle _Box_Header_Style;
    public static GUIStyle Box_Header_Style
    {
      get
      {
        if (_Box_Header_Style == null)
          _Box_Header_Style = editor_skin_dack.FindStyle("BoxHeader");
        return _Box_Header_Style;
      }
    }
    private static GUIStyle _Top_Box_Header_Style;
    public static GUIStyle Top_Box_Header_Style
    {
      get
      {
        if (_Top_Box_Header_Style == null)
          _Top_Box_Header_Style = editor_skin_dack.FindStyle("TopBoxHeader");
        return _Top_Box_Header_Style;
      }
    }
    private static GUIStyle _Pixel_Box3_Style;
    public static GUIStyle Pixel_Box3_Style
    {
      get
      {
        if (_Pixel_Box3_Style == null)
          _Pixel_Box3_Style = editor_skin_dack.FindStyle("PixelBox3");
        return _Pixel_Box3_Style;
      }
    }
    private static GUIStyle _Pixel_Button_Style;
    public static GUIStyle Pixel_Button_Style
    {
      get
      {
        if (_Pixel_Button_Style == null)
          _Pixel_Button_Style = editor_skin_dack.FindStyle("PixelButton");
        return _Pixel_Button_Style;
      }
    }
    private static GUIStyle _Link_Button_Style;
    public static GUIStyle Link_Button_Style
    {
      get
      {
        if (_Link_Button_Style == null)
          _Link_Button_Style = editor_skin_dack.FindStyle("LinkButton");
        return _Link_Button_Style;
      }
    }
    private static GUIStyle _Close_Button_Style;
    public static GUIStyle Close_Button_Style
    {
      get
      {
        if (_Close_Button_Style == null)
          _Close_Button_Style = editor_skin_dack.FindStyle("CloseButton");
        return _Close_Button_Style;
      }
    }
    private static GUIStyle _Grid_Pivot_Select_Button_Style;
    public static GUIStyle Grid_Pivot_Select_Button_Style
    {
      get
      {
        if (_Grid_Pivot_Select_Button_Style == null)
          _Grid_Pivot_Select_Button_Style = editor_skin_dack.FindStyle("GridPivotSelectButton");
        return _Grid_Pivot_Select_Button_Style;
      }
    }
    private static GUIStyle _Grid_Pivot_Select_Background_Style;
    public static GUIStyle Grid_Pivot_Select_Background_Style
    {
      get
      {
        if (_Grid_Pivot_Select_Background_Style == null)
          _Grid_Pivot_Select_Background_Style = editor_skin_dack.FindStyle("GridPivotSelectBackground");
        return _Grid_Pivot_Select_Background_Style;
      }
    }
    private static GUIStyle _Collision_Header_Style;
    public static GUIStyle Collision_Header_Style
    {
      get
      {
        if (_Collision_Header_Style == null)
          _Collision_Header_Style = editor_skin_dack.FindStyle("CollisionHeader");
        return _Collision_Header_Style;
      }
    }
    private static GUIStyle _Info_Button_Style;
    public static GUIStyle Info_Button_Style
    {
      get
      {
        if (_Info_Button_Style == null)
          _Info_Button_Style = editor_skin_dack.FindStyle("InfoButton");
        return _Info_Button_Style;
      }
    }
    private static GUIStyle _Pixel_Box3_Separator_Style;
    public static GUIStyle Pixel_Box3_Separator_Style
    {
      get
      {
        if (_Pixel_Box3_Separator_Style == null)
          _Pixel_Box3_Separator_Style = editor_skin_dack.FindStyle("PixelBox3Separator");
        return _Pixel_Box3_Separator_Style;
      }
    }
    private static GUIStyle _Grid_Size_Lock_Style;
    public static GUIStyle Grid_Size_Lock_Style
    {
      get
      {
        if (_Grid_Size_Lock_Style == null)
          _Grid_Size_Lock_Style = editor_skin_dack.FindStyle("GridSizeLock");
        return _Grid_Size_Lock_Style;
      }
    }
    private static GUIStyle _Up_Arrow_Style;
    public static GUIStyle Up_Arrow_Style
    {
      get
      {
        if (_Up_Arrow_Style == null)
          _Up_Arrow_Style = editor_skin_dack.FindStyle("UpArrow");
        return _Up_Arrow_Style;
      }
    }
    private static GUIStyle _Down_Arrow_Style;
    public static GUIStyle Down_Arrow_Style
    {
      get
      {
        if (_Down_Arrow_Style == null)
          _Down_Arrow_Style = editor_skin_dack.FindStyle("DownArrow");
        return _Down_Arrow_Style;
      }
    }
    private static GUIStyle _Small_Reset_Style;
    public static GUIStyle Small_Reset_Style
    {
      get
      {
        if (_Small_Reset_Style == null)
          _Small_Reset_Style = editor_skin_dack.FindStyle("SmallReset");
        return _Small_Reset_Style;
      }
    }
    private static GUIStyle _Gizmo_Button_Style;
    public static GUIStyle Gizmo_Button_Style
    {
      get
      {
        if (_Gizmo_Button_Style == null)
          _Gizmo_Button_Style = editor_skin_dack.FindStyle("GizmoButton");
        return _Gizmo_Button_Style;
      }
    }


    private static GUIStyle _Help_Box;
    public static GUIStyle Help_Box
    {
      get
      {
        if (_Help_Box == null)
          _Help_Box = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("HelpBox");
        return _Help_Box;
      }
    }


    private static GUIStyle _Thin_Help_Box;
    public static GUIStyle Thin_Help_Box
    {
      get
      {
        if (_Thin_Help_Box == null)
        {
          _Thin_Help_Box = new GUIStyle(Help_Box);
          _Thin_Help_Box.contentOffset = new Vector2(0, -2);
          _Thin_Help_Box.stretchWidth = false;
          _Thin_Help_Box.clipping = TextClipping.Overflow;
          _Thin_Help_Box.overflow.top += 1;
        }
        return _Thin_Help_Box;
      }
    }


    private static GUIStyle _Up_Arrow;
    public static GUIStyle Up_Arrow
    {
      get
      {
        if (_Up_Arrow == null)
          _Up_Arrow = editor_skin_dack.FindStyle("UpArrow");
        return _Up_Arrow;
      }
    }

    private static GUIStyle _Down_Arrow;
    public static GUIStyle Down_Arrow
    {
      get
      {
        if (_Down_Arrow == null)
          _Down_Arrow = editor_skin_dack.FindStyle("DownArrow");
        return _Down_Arrow;
      }
    }

  }
}
#endif