#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public static partial class GUIStyleConst
	{
		private static GUISkin _editor_skin_dark;

		public static GUISkin editor_skin_dark
		{
			get
			{
				if (_editor_skin_dark == null)
					_editor_skin_dark =
						AssetDatabase.LoadAssetAtPath(
							FilePathConst.EditorAssetsPath + "DarkEditorSkin/DarkEditorSkin.guiskin",
							typeof(GUISkin)) as GUISkin;
				return _editor_skin_dark;
			}
		}


		private static GUIStyle _Graph_Delete_Button_Style;

		public static GUIStyle GraphDeleteButtonStyle => _Graph_Delete_Button_Style ??
															(_Graph_Delete_Button_Style =
																editor_skin_dark.FindStyle("PixelButton"));

		private static GUIStyle _Graph_Info_Button_Style;

		public static GUIStyle GraphInfoButtonStyle => _Graph_Info_Button_Style ??
														  (_Graph_Info_Button_Style =
															  editor_skin_dark.FindStyle("InfoButton"));

		private static GUIStyle _Graph_Gizmo_Button_Style;

		public static GUIStyle GraphGizmoButtonStyle => _Graph_Gizmo_Button_Style ??
														   (_Graph_Gizmo_Button_Style =
															   editor_skin_dark.FindStyle("GizmoButton"));

		private static GUIStyle _Box_Style;

		public static GUIStyle Box_Style => _Box_Style ?? (_Box_Style = editor_skin_dark.FindStyle("box"));

		private static GUIStyle _Graph_Box_Style;

		public static GUIStyle Graph_Box_Style =>
			_Graph_Box_Style ?? (_Graph_Box_Style = editor_skin_dark.FindStyle("PixelBox3"));


		private static GUIStyle _Button_Style;

		public static GUIStyle ButtonStyle => _Button_Style ?? (_Button_Style = editor_skin_dark.FindStyle("button"));

		private static GUIStyle _Toggle_Style;

		public static GUIStyle ToggleStyle => _Toggle_Style ?? (_Toggle_Style = editor_skin_dark.FindStyle("toggle"));

		private static GUIStyle _Label_Style_Dark;

		public static GUIStyle Label_Style_Dark =>
			_Label_Style_Dark ?? (_Label_Style_Dark = editor_skin_dark.FindStyle("label"));

		private static GUIStyle _Textfield_Style;

		public static GUIStyle TextfieldStyle =>
			_Textfield_Style ?? (_Textfield_Style = editor_skin_dark.FindStyle("textfield"));

		private static GUIStyle _Textarea_Style;

		public static GUIStyle TextareaStyle =>
			_Textarea_Style ?? (_Textarea_Style = editor_skin_dark.FindStyle("textarea"));

		private static GUIStyle _Horizontal_Slider_Style;

		public static GUIStyle Horizontal_Slider_Style => _Horizontal_Slider_Style ??
														  (_Horizontal_Slider_Style =
															  editor_skin_dark.FindStyle("horizontalslider"));

		private static GUIStyle _Horizontal_Slider_Thumb_Style;

		public static GUIStyle Horizontal_Slider_Thumb_Style =>
			_Horizontal_Slider_Thumb_Style ?? (_Horizontal_Slider_Thumb_Style =
				editor_skin_dark.FindStyle("horizontalsliderthumb"));

		private static GUIStyle _Vertical_Slider_Style;

		public static GUIStyle Vertical_Slider_Style => _Vertical_Slider_Style ??
														(_Vertical_Slider_Style =
															editor_skin_dark.FindStyle("verticalslider"));

		private static GUIStyle _Horizontal_Scrollbar_Style;

		public static GUIStyle Horizontal_Scrollbar_Style => _Horizontal_Scrollbar_Style ??
															 (_Horizontal_Scrollbar_Style =
																 editor_skin_dark.FindStyle("horizontalscrollbar"));

		private static GUIStyle _Vertical_Scrollbar_Style;

		public static GUIStyle Vertical_Scrollbar_Style => _Vertical_Scrollbar_Style ??
														   (_Vertical_Scrollbar_Style =
															   editor_skin_dark.FindStyle("verticalscrollbar"));

		private static GUIStyle _Horizontal_Scrollbar_Thumb_Style;

		public static GUIStyle Horizontal_Scrollbar_Thumb_Style =>
			_Horizontal_Scrollbar_Thumb_Style ?? (_Horizontal_Scrollbar_Thumb_Style =
				editor_skin_dark.FindStyle("horizontalscrollbarthumb"));

		private static GUIStyle _Horizontal_Scrollbar_Left_Button_Style;

		public static GUIStyle Horizontal_Scrollbar_Left_Button_Style =>
			_Horizontal_Scrollbar_Left_Button_Style ?? (_Horizontal_Scrollbar_Left_Button_Style =
				editor_skin_dark.FindStyle("horizontalscrollbarleftbutton"));

		private static GUIStyle _Pixel_Box_Style;

		public static GUIStyle Pixel_Box_Style =>
			_Pixel_Box_Style ?? (_Pixel_Box_Style = editor_skin_dark.FindStyle("PixelBox"));

		private static GUIStyle _Color_Interpolation_Box_Style;

		public static GUIStyle Color_Interpolation_Box_Style =>
			_Color_Interpolation_Box_Style ?? (_Color_Interpolation_Box_Style =
				editor_skin_dark.FindStyle("ColorInterpolationBox"));

		private static GUIStyle _Stretch_Width_Style;

		public static GUIStyle Stretch_Width_Style =>
			_Stretch_Width_Style ?? (_Stretch_Width_Style = editor_skin_dark.FindStyle("StretchWidth"));

		private static GUIStyle _Box_Header_Style;

		public static GUIStyle BoxHeaderStyle =>
			_Box_Header_Style ?? (_Box_Header_Style = editor_skin_dark.FindStyle("BoxHeader"));

		private static GUIStyle _Top_Box_Header_Style;

		public static GUIStyle Top_Box_Header_Style =>
			_Top_Box_Header_Style ?? (_Top_Box_Header_Style = editor_skin_dark.FindStyle("TopBoxHeader"));

		private static GUIStyle _Pixel_Box3_Style;

		public static GUIStyle Pixel_Box3_Style =>
			_Pixel_Box3_Style ?? (_Pixel_Box3_Style = editor_skin_dark.FindStyle("PixelBox3"));

		private static GUIStyle _Pixel_Button_Style;

		public static GUIStyle Pixel_Button_Style =>
			_Pixel_Button_Style ?? (_Pixel_Button_Style = editor_skin_dark.FindStyle("PixelButton"));

		private static GUIStyle _Link_Button_Style;

		public static GUIStyle Link_Button_Style =>
			_Link_Button_Style ?? (_Link_Button_Style = editor_skin_dark.FindStyle("LinkButton"));

		private static GUIStyle _Close_Button_Style;

		public static GUIStyle CloseButtonStyle =>
			_Close_Button_Style ?? (_Close_Button_Style = editor_skin_dark.FindStyle("CloseButton"));

		private static GUIStyle _Grid_Pivot_Select_Button_Style;

		public static GUIStyle Grid_Pivot_Select_Button_Style =>
			_Grid_Pivot_Select_Button_Style ?? (_Grid_Pivot_Select_Button_Style =
				editor_skin_dark.FindStyle("GridPivotSelectButton"));

		private static GUIStyle _Grid_Pivot_Select_Background_Style;

		public static GUIStyle Grid_Pivot_Select_Background_Style =>
			_Grid_Pivot_Select_Background_Style ?? (_Grid_Pivot_Select_Background_Style =
				editor_skin_dark.FindStyle("GridPivotSelectBackground"));

		private static GUIStyle _Collision_Header_Style;

		public static GUIStyle Collision_Header_Style => _Collision_Header_Style ??
														 (_Collision_Header_Style =
															 editor_skin_dark.FindStyle("CollisionHeader"));

		private static GUIStyle _Info_Button_Style;

		public static GUIStyle Info_Button_Style =>
			_Info_Button_Style ?? (_Info_Button_Style = editor_skin_dark.FindStyle("InfoButton"));

		private static GUIStyle _Pixel_Box3_Separator_Style;

		public static GUIStyle Pixel_Box3_Separator_Style => _Pixel_Box3_Separator_Style ??
															 (_Pixel_Box3_Separator_Style =
																 editor_skin_dark.FindStyle("PixelBox3Separator"));

		private static GUIStyle _Grid_Size_Lock_Style;

		public static GUIStyle Grid_Size_Lock_Style =>
			_Grid_Size_Lock_Style ?? (_Grid_Size_Lock_Style = editor_skin_dark.FindStyle("GridSizeLock"));

		private static GUIStyle _Up_Arrow_Style;

		public static GUIStyle UpArrowStyle =>
			_Up_Arrow_Style ?? (_Up_Arrow_Style = editor_skin_dark.FindStyle("UpArrow"));

		private static GUIStyle _Down_Arrow_Style;

		public static GUIStyle DownArrowStyle =>
			_Down_Arrow_Style ?? (_Down_Arrow_Style = editor_skin_dark.FindStyle("DownArrow"));

		private static GUIStyle _Small_Reset_Style;

		public static GUIStyle SmallResetStyle =>
			_Small_Reset_Style ?? (_Small_Reset_Style = editor_skin_dark.FindStyle("SmallReset"));

		private static GUIStyle _Gizmo_Button_Style;

		public static GUIStyle GizmoButtonStyle =>
			_Gizmo_Button_Style ?? (_Gizmo_Button_Style = editor_skin_dark.FindStyle("GizmoButton"));


		private static GUIStyle _Help_Box;

		public static GUIStyle Help_Box => _Help_Box ??
										   (_Help_Box = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector)
											   .FindStyle("HelpBox"));


		private static GUIStyle _Thin_Help_Box;

		public static GUIStyle ThinHelpBox => _Thin_Help_Box ?? (_Thin_Help_Box = new GUIStyle(Help_Box)
		{
			contentOffset = new Vector2(0, -2),
			stretchWidth = false,
			clipping = TextClipping.Overflow,
			overflow = { top = 1 }
		});


		private static GUIStyle _Up_Arrow;

		public static GUIStyle Up_Arrow => _Up_Arrow ?? (_Up_Arrow = editor_skin_dark.FindStyle("UpArrow"));

		private static GUIStyle _Down_Arrow;

		public static GUIStyle Down_Arrow => _Down_Arrow ?? (_Down_Arrow = editor_skin_dark.FindStyle("DownArrow"));
	}
}
#endif