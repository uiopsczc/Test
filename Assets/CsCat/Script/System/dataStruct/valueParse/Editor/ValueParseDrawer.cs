using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  [CustomPropertyDrawer(typeof(ValueParse))]
  public class ValueParseDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      //重载BeginProperty
      using (new EditorGUIBeginPropertyScope(position, label, property))
      {
        //Unity默认的每个属性字段都会占用一行，我们这里希望一条自定义Property占一行
        //要是实现这个要求我们分三步： 1. 取消缩进  2. 设置PropertyField 3.还原缩进

        //不要缩进子字段，只有取消了缩进，Rect挤才一行才不会混乱
        using (new EditorGUIIndentLevelScope(-EditorGUI.indentLevel))
        {
          //计算要用到的属性显示rect   Rect(x,y,width,height)x,y是左顶点
          float splitWidth = 5;

          float typeNameLableRectWidth = 30;
          float typeNameRectWidth = 100;
          float valueLableRectWidth = 40;
          float valueRectWidth = 100;



          var typeLableRect = new Rect(position.x, position.y, typeNameLableRectWidth, position.height);
          var typeRect = new Rect(typeLableRect.xMax + splitWidth, position.y, typeNameRectWidth, position.height);
          var valueLableRect = new Rect(typeRect.xMax + splitWidth, position.y, valueLableRectWidth, position.height);
          var valueRect = new Rect(valueLableRect.xMax + splitWidth, position.y, valueRectWidth, position.height);


          var valueParseTypeList = ValueParseUtil.GetValueParseList().ConvertAll(e => ((Type) e["type"])).ToList();
          var valueParseTypeFullNames = valueParseTypeList.ConvertAll(e => e.FullName).ToArray();
          var valueParseTypeDisplayNames = valueParseTypeList.ConvertAll(e => e.Name.ToGUIContent()).ToArray();


          //绘制字段 - 将GUIContent.none传递给每个字段，以便绘制它们而不是用标签
          //属性绘制器不支持布局来创建GUI;
          //因此，您必须使用的类是EditorGUI而不是EditorGUILayout。这就是为什么要给每个属性指定Rect
          EditorGUI.PrefixLabel(typeLableRect, "type:".ToGUIContent());
          int selectedIndex = EditorGUI.Popup(typeRect, GUIContent.none,
            property.FindPropertyRelative("typeName") == null
              ? -1
              : valueParseTypeFullNames.IndexOf(property.FindPropertyRelative("typeName").stringValue),
            valueParseTypeDisplayNames);
          if (selectedIndex != -1)
          {
            property.FindPropertyRelative("typeName").stringValue = valueParseTypeFullNames[selectedIndex];
            property.FindPropertyRelative("assembleName").stringValue =
              valueParseTypeList[selectedIndex].Assembly.FullName;
          }

          EditorGUI.PrefixLabel(valueLableRect, "value:".ToGUIContent());
          EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);
        }
      }
    }

  }
}