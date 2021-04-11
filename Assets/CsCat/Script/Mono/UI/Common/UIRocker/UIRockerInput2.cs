using UnityEngine;

namespace CsCat
{
  // 左右 键盘的AD键,[-1，1]
  // 下上 键盘的SW键,[-1，1]
  public class UIRockerInput2 : UIRockerInput
  {
    public override void __GetAxisKeyInput(out float axis_x, out float axis_y)
    {
      var axis_x_left = Input.GetKey(KeyCode.A) ? -1 : 0; //键盘的左
      var axis_x_right = Input.GetKey(KeyCode.D) ? 1 : 0; //键盘的右
      var axis_y_down = Input.GetKey(KeyCode.S) ? -1 : 0; //键盘的下
      var axis_y_up = Input.GetKey(KeyCode.W) ? 1 : 0; //键盘的上
      axis_x = axis_x_left + axis_x_right;
      axis_y = axis_y_down + axis_y_up;
    }
  }
}