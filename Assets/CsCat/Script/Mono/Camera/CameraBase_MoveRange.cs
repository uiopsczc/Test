namespace CsCat
{
	public partial class CameraBase : TickObject
	{
		//检测摄像机的移动范围TODO 未实现
		//  private Vector3 min_position = Vector3Const.Default_Min;
		//  private Vector3 max_position = Vector3Const.Default_Max;

		public void SetMoveRange(object move_range)
		{
			//    this.minPosition = minPosition;
			//    this.maxPosition = maxPosition;
		}

		public void ApplyMoveRange(float delta_time)
		{
			//this.transform.position = maxPosition;
		}

	}
}



