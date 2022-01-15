using System;
using UnityEngine;

namespace CsCat
{
	public class AStarRange
	{
		public int leftBottomX;
		public int leftBottomY;
		public int rightTopX;
		public int rightTopY;

		public AStarRange(int left_bottom_x, int left_bottom_y, int right_top_x, int right_top_y)
		{
			SetRange(left_bottom_x, left_bottom_y, right_top_x, right_top_y);
		}

		public AStarRange(AStarRange range)
		{
			SetRange(range.leftBottomX, range.leftBottomY, range.rightTopX, range.rightTopY);
		}

		public AStarRange(Vector2Int left_bottom, Vector2Int right_top)
		{
			SetRange(left_bottom.x, left_bottom.y, right_top.x, right_top.y);
		}

		public AStarRange(Vector2Int left_bottom, int dx, int dy) : this(left_bottom, left_bottom + new Vector2Int(dx, dy))
		{
		}

		public void SetRange(int left_bottom_x, int left_bottom_y, int right_top_x, int right_top_y)
		{
			this.leftBottomX = Math.Min(left_bottom_x, right_top_x);
			this.leftBottomY = Math.Min(left_bottom_y, right_top_y);
			this.rightTopX = Math.Max(left_bottom_x, right_top_x);
			this.rightTopY = Math.Max(left_bottom_y, right_top_y);
		}

		public Vector2Int GetCenter()
		{
			return new Vector2Int((leftBottomX + rightTopX) / 2, (leftBottomY + rightTopY) / 2);
		}

		public Vector2Int GetLeftBottom()
		{
			return new Vector2Int(leftBottomX, leftBottomY);
		}

		public Vector2Int GetRightTop()
		{
			return new Vector2Int(rightTopX, rightTopY);
		}

		public Vector2Int GetRandomPos(RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			int dx = rightTopX - leftBottomX;
			int dy = rightTopY - leftBottomY;
			int x, y;
			if (dx > 0)
				x = leftBottomX + randomManager.RandomInt(0, Math.Abs(dx) + 1);
			else if (dx < 0)
				x = leftBottomX - randomManager.RandomInt(0, Math.Abs(dx) + 1);
			else
				x = leftBottomX;
			if (dy > 0)
				y = leftBottomY + randomManager.RandomInt(0, Math.Abs(dy) + 1);
			else if (dy < 0)
				y = leftBottomY - randomManager.RandomInt(0, Math.Abs(dy) + 1);
			else
				y = leftBottomY;
			return new Vector2Int(x, y);
		}

		public int GetWidth()
		{
			return rightTopX - leftBottomX;
		}

		public int GetHeight()
		{
			return rightTopY - leftBottomY;
		}

		public bool IsInRange(Vector2Int pos)
		{
			return IsInRangeX(pos.x) && IsInRangeY(pos.y);
		}

		public bool IsInRangeX(int x)
		{
			RangeCat range_x = new RangeCat(leftBottomX, rightTopX);
			return range_x.IsContains(x);
		}

		public bool IsInRangeY(int y)
		{
			RangeCat range_y = new RangeCat(leftBottomY, rightTopY);
			return range_y.IsContains(y);
		}

		public override string ToString()
		{
			return string.Format("[{0},{1}]-[{2},{3}]", leftBottomX, leftBottomY, rightTopX, rightTopY);
		}

	}
}