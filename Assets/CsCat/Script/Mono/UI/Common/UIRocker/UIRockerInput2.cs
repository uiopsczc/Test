using UnityEngine;

namespace CsCat
{
	// 左右 键盘的AD键,[-1，1]
	// 下上 键盘的SW键,[-1，1]
	public class UIRockerInput2 : UIRockerInput
	{
		public override void __GetAxisKeyInput(out float axisX, out float axisY)
		{
			var axisXLeft = Input.GetKey(KeyCode.A) ? -1 : 0; //键盘的左
			var axisXRight = Input.GetKey(KeyCode.D) ? 1 : 0; //键盘的右
			var axisYDown = Input.GetKey(KeyCode.S) ? -1 : 0; //键盘的下
			var axisYUp = Input.GetKey(KeyCode.W) ? 1 : 0; //键盘的上
			axisX = axisXLeft + axisXRight;
			axisY = axisYDown + axisYUp;
		}
	}
}