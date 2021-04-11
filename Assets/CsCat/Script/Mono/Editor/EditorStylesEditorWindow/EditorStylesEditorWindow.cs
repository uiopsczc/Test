using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class EditorStylesEditorWindow : EditorWindow
  {
    private Vector2 scroll_position;
    private string display_value;
    void OnGUI()
    {
      using (new GUILayoutBeginScrollViewScope(ref scroll_position))
      {
        GUILayout.Label("EditorStyles.boldLabel", EditorStyles.boldLabel);
        GUILayout.Label("EditorStyles.centeredGreyMiniLabel", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Label("EditorStyles.colorField", EditorStyles.colorField);
        GUILayout.Label("EditorStyles.foldout", EditorStyles.foldout);
        GUILayout.Label("EditorStyles.foldoutPreDrop" ,EditorStyles.foldoutPreDrop);
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
        display_value = "GUIStyleConst.Box_Header_Style";
        EditorGUILayout.TextField("", display_value, GUIStyleConst.Box_Header_Style, GUILayout.ExpandWidth(false),
          GUILayout.ExpandHeight(false));

        display_value = "GUIStyleConst.Graph_Gizmo_Button_Style";
        if (GUILayout.Button(display_value, GUIStyleConst.Graph_Gizmo_Button_Style))
          this.ShowNotificationAndLog(display_value);

        display_value = "GUIStyleConst.Graph_Info_Button_Style";
        if (GUILayout.Toggle(false, display_value,
          GUIStyleConst.Graph_Info_Button_Style))
          this.ShowNotificationAndLog(display_value);

        display_value = "GUIStyleConst.Graph_Delete_Button_Style";
        if (GUILayout.Button(display_value,
          GUIStyleConst.Graph_Delete_Button_Style))
          this.ShowNotificationAndLog(display_value);

        display_value = "GUIStyleConst.Down_Arrow_Style";
        if (GUILayout.Button(display_value, GUIStyleConst.Down_Arrow_Style))
          this.ShowNotificationAndLog(display_value);

        display_value = "GUIStyleConst.Up_Arrow_Style";
        if (GUILayout.Button(display_value, GUIStyleConst.Up_Arrow_Style))
          this.ShowNotificationAndLog(display_value);

        display_value = "GUIStyleConst.Button_Style";
        if (GUILayout.Button(display_value, GUIStyleConst.Button_Style))
          this.ShowNotificationAndLog(display_value);

        display_value = "GUIStyleConst.Toggle_Style";
        if (GUILayout.Toggle(false, display_value, GUIStyleConst.Toggle_Style))
          this.ShowNotificationAndLog(display_value);

        display_value = "GUIStyleConst.Label_Style";
        GUILayout.Label(display_value, GUIStyleConst.Label_Style);

        display_value = "GUIStyleConst.Textfield_Style";
        GUILayout.TextField(display_value, GUIStyleConst.Textfield_Style);

        display_value = "GUIStyleConst.Textarea_Style";
        GUILayout.TextArea(display_value, GUIStyleConst.Textarea_Style);

        display_value = "GUIStyleConst.Close_Button_Style";
        if (GUILayout.Button(display_value, GUIStyleConst.Close_Button_Style))
          this.ShowNotificationAndLog(GUIStyleConst.Close_Button_Style.ToString());

        display_value = "GUIStyleConst.Small_Reset_Style";
        if (GUILayout.Button(display_value, GUIStyleConst.Small_Reset_Style))
          this.ShowNotificationAndLog(GUIStyleConst.Small_Reset_Style.ToString());

        display_value = "GUIStyleConst.Gizmo_Button_Style";
        if (GUILayout.Button(GUIStyleConst.Gizmo_Button_Style.ToString(), GUIStyleConst.Gizmo_Button_Style))
          this.ShowNotificationAndLog(GUIStyleConst.Gizmo_Button_Style.ToString());

        display_value = "GUIStyleConst.Thin_Help_Box";
        GUILayout.Label(display_value, GUIStyleConst.Thin_Help_Box, GUILayout.Height(15));

        
      }
    }
  }
}