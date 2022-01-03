using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorStylesEditorWindow : EditorWindow
	{
		private Vector2 scrollPosition;
		private string displayValue;

		void OnGUI()
		{
			using (new GUILayoutBeginScrollViewScope(ref scrollPosition))
			{
				GUILayout.Label("EditorStyles.boldLabel", EditorStyles.boldLabel);
				GUILayout.Label("EditorStyles.centeredGreyMiniLabel", EditorStyles.centeredGreyMiniLabel);
				GUILayout.Label("EditorStyles.colorField", EditorStyles.colorField);
				GUILayout.Label("EditorStyles.foldout", EditorStyles.foldout);
				GUILayout.Label("EditorStyles.foldoutPreDrop", EditorStyles.foldoutPreDrop);
				GUILayout.Label("EditorStyles.helpBox", EditorStyles.helpBox);
				GUILayout.Label("EditorStyles.inspectorDefaultMargins", EditorStyles.inspectorDefaultMargins);
				GUILayout.Label("EditorStyles.inspectorFullWidthMargins", EditorStyles.inspectorFullWidthMargins);
				GUILayout.Label("EditorStyles.label", EditorStyles.label);
				GUILayout.Label("EditorStyles.largeLabel", EditorStyles.largeLabel);
				GUILayout.Label("EditorStyles.layerMaskField", EditorStyles.layerMaskField);
				GUILayout.Label("EditorStyles.miniBoldLabel", EditorStyles.miniBoldLabel);
				GUILayout.Label("EditorStyles.miniButton", EditorStyles.miniButton);
				GUILayout.Label("EditorStyles.miniButtonLeft", EditorStyles.miniButtonLeft);
				GUILayout.Label("EditorStyles.miniButtonMid", EditorStyles.miniButtonMid);
				GUILayout.Label("EditorStyles.miniButtonRight", EditorStyles.miniButtonRight);
				GUILayout.Label("EditorStyles.miniLabel", EditorStyles.miniLabel);
				GUILayout.Label("EditorStyles.miniTextField", EditorStyles.miniTextField);
				GUILayout.Label("EditorStyles.numberField", EditorStyles.numberField);
				GUILayout.Label("EditorStyles.objectField", EditorStyles.objectField);
				GUILayout.Label("EditorStyles.objectFieldMiniThumb", EditorStyles.objectFieldMiniThumb);
				GUILayout.Label("EditorStyles.objectFieldThumb", EditorStyles.objectFieldThumb);
				GUILayout.Label("EditorStyles.popup", EditorStyles.popup);

				GUILayout.Space(100);

				GUILayout.Label("EditorStyles.radioButton", EditorStyles.radioButton);
				GUILayout.Label("EditorStyles.textArea", EditorStyles.textArea);
				GUILayout.Label("EditorStyles.textField", EditorStyles.textField);
				GUILayout.Label("EditorStyles.toggle", EditorStyles.toggle);
				GUILayout.Label("EditorStyles.toggleGroup", EditorStyles.toggleGroup);
				GUILayout.Label("EditorStyles.toolbar", EditorStyles.toolbar);
				GUILayout.Label("EditorStyles.toolbarButton", EditorStyles.toolbarButton);
				GUILayout.Label("EditorStyles.toolbarDropDown", EditorStyles.toolbarDropDown);
				GUILayout.Label("EditorStyles.toolbarPopup", EditorStyles.toolbarPopup);
				GUILayout.Label("EditorStyles.toolbarTextField", EditorStyles.toolbarTextField);
				GUILayout.Label("EditorStyles.whiteBoldLabel", EditorStyles.whiteBoldLabel);
				GUILayout.Label("EditorStyles.whiteLabel", EditorStyles.whiteLabel);
				GUILayout.Label("EditorStyles.whiteLargeLabel", EditorStyles.whiteLargeLabel);
				GUILayout.Label("EditorStyles.whiteMiniLabel", EditorStyles.whiteMiniLabel);
				GUILayout.Label("EditorStyles.wordWrappedLabel", EditorStyles.wordWrappedLabel);
				GUILayout.Label("EditorStyles.wordWrappedMiniLabel'", EditorStyles.wordWrappedMiniLabel);
				GUILayout.Label("AnimationEventBackground", "AnimationEventBackground");
				GUILayout.Label("Dopesheetkeyframe", "Dopesheetkeyframe");
				EditorGUILayout.HelpBox("help box", MessageType.Info);

				GUILayout.Space(100);
				displayValue = "GUIStyleConst.Box_Header_Style";
				EditorGUILayout.TextField("", displayValue, GUIStyleConst.BoxHeaderStyle,
					GUILayout.ExpandWidth(false),
					GUILayout.ExpandHeight(false));

				displayValue = "GUIStyleConst.Graph_Gizmo_Button_Style";
				if (GUILayout.Button(displayValue, GUIStyleConst.GraphGizmoButtonStyle))
					this.ShowNotificationAndLog(displayValue);

				displayValue = "GUIStyleConst.Graph_Info_Button_Style";
				if (GUILayout.Toggle(false, displayValue,
					GUIStyleConst.GraphInfoButtonStyle))
					this.ShowNotificationAndLog(displayValue);

				displayValue = "GUIStyleConst.Graph_Delete_Button_Style";
				if (GUILayout.Button(displayValue,
					GUIStyleConst.GraphDeleteButtonStyle))
					this.ShowNotificationAndLog(displayValue);

				displayValue = "GUIStyleConst.Down_Arrow_Style";
				if (GUILayout.Button(displayValue, GUIStyleConst.DownArrowStyle))
					this.ShowNotificationAndLog(displayValue);

				displayValue = "GUIStyleConst.Up_Arrow_Style";
				if (GUILayout.Button(displayValue, GUIStyleConst.UpArrowStyle))
					this.ShowNotificationAndLog(displayValue);

				displayValue = "GUIStyleConst.Button_Style";
				if (GUILayout.Button(displayValue, GUIStyleConst.ButtonStyle))
					this.ShowNotificationAndLog(displayValue);

				displayValue = "GUIStyleConst.Toggle_Style";
				if (GUILayout.Toggle(false, displayValue, GUIStyleConst.ToggleStyle))
					this.ShowNotificationAndLog(displayValue);

				displayValue = "GUIStyleConst.Label_Style";
				GUILayout.Label(displayValue, GUIStyleConst.LabelStyle);

				displayValue = "GUIStyleConst.Textfield_Style";
				GUILayout.TextField(displayValue, GUIStyleConst.TextfieldStyle);

				displayValue = "GUIStyleConst.Textarea_Style";
				GUILayout.TextArea(displayValue, GUIStyleConst.TextareaStyle);

				displayValue = "GUIStyleConst.Close_Button_Style";
				if (GUILayout.Button(displayValue, GUIStyleConst.CloseButtonStyle))
					this.ShowNotificationAndLog(GUIStyleConst.CloseButtonStyle.ToString());

				displayValue = "GUIStyleConst.Small_Reset_Style";
				if (GUILayout.Button(displayValue, GUIStyleConst.SmallResetStyle))
					this.ShowNotificationAndLog(GUIStyleConst.SmallResetStyle.ToString());

				displayValue = "GUIStyleConst.Gizmo_Button_Style";
				if (GUILayout.Button(GUIStyleConst.GizmoButtonStyle.ToString(), GUIStyleConst.GizmoButtonStyle))
					this.ShowNotificationAndLog(GUIStyleConst.GizmoButtonStyle.ToString());

				displayValue = "GUIStyleConst.Thin_Help_Box";
				GUILayout.Label(displayValue, GUIStyleConst.ThinHelpBox, GUILayout.Height(15));
			}
		}
	}
}