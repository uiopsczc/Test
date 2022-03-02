using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		private readonly Dictionary<string, Color> _changeColorDict = new Dictionary<string, Color>();

		//改变一个物体上MeshRenderer、SkinMeshRenderer材质的颜色
		public void ChangeColor(string tag, Color? color)
		{
			if (this._unitMaterialInfoList.IsNullOrEmpty())
				return;
			if (color != null)
				this._changeColorDict[tag] = color.Value;
			else
				this._changeColorDict.Remove(tag);
			this._UpdateColor();
		}

		private void _UpdateColor()
		{
			var currentColor = new Color(1, 1, 1, 1);
			foreach (var keyValue in this._changeColorDict)
			{
				var changeColor = keyValue.Value;
				currentColor = currentColor * changeColor;
			}

			for (var i = 0; i < this._unitMaterialInfoList.Count; i++)
			{
				var unitMaterialInfo = this._unitMaterialInfoList[i];
				unitMaterialInfo.material.color = unitMaterialInfo.color * currentColor;
			}
		}
	}
}