using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class Unit
  {
    private Dictionary<string, Color> change_color_dict = new Dictionary<string, Color>();

    //改变一个物体上MeshRenderer、SkinMeshRenderer材质的颜色
    public void ChangeColor(string tag, Color? color)
    {
      if (this.unitMaterialInfo_list.IsNullOrEmpty())
        return;
      if (color != null)
        this.change_color_dict[tag] = color.Value;
      else
        this.change_color_dict.Remove(tag);
      this.__UpdateColor();
    }

    private void __UpdateColor()
    {
      var current_color = new Color(1, 1, 1, 1);
      foreach (var change_color in this.change_color_dict.Values)
        current_color = current_color * change_color;
      foreach (var unitMaterialInfo in this.unitMaterialInfo_list)
        unitMaterialInfo.material.color = unitMaterialInfo.color * current_color;
    }
  }
}