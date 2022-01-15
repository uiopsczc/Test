using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		private Dictionary<string, Color> changeColorDict = new Dictionary<string, Color>();

		//改变一个物体上MeshRenderer、SkinMeshRenderer材质的颜色
		public void ChangeColor(string tag, Color? color)
		{
			if (this.unitMaterialInfoList.IsNullOrEmpty())
				return;
			if (color != null)
				this.changeColorDict[tag] = color.Value;
			else
				this.changeColorDict.Remove(tag);
			this.__UpdateColor();
		}

		private void __UpdateColor()
		{
			var currentColor = new Color(1, 1, 1, 1);
			foreach (var keyValue in this.changeColorDict)
			{
				var changeColor = keyValue.Value;
				currentColor = currentColor * changeColor;
			}

			for (var i = 0; i < this.unitMaterialInfoList.Count; i++)
			{
				var unitMaterialInfo = this.unitMaterialInfoList[i];
				unitMaterialInfo.material.color = unitMaterialInfo.color * currentColor;
			}
		}
	}
}