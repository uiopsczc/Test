using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class AABBBox : BoxBase
	{
		private float _minX;
		private float _minY;
		private float _minZ;

		private float _maxX;
		private float _maxY;
		private float _maxZ;

		public float minX
		{
			get => this._minX;
			set
			{
				if (this._minX == value)
					return;
				this._minX = value;
				this.UpdateBox();
			}
		}

		public float minY
		{
			get => this._minY;
			set
			{
				if (this._minY == value)
					return;
				this._minY = value;
				this.UpdateBox();
			}
		}

		public float minZ
		{
			get => this._minZ;
			set
			{
				if (this._minZ == value)
					return;
				this._minZ = value;
				this.UpdateBox();
			}
		}

		public float maxX
		{
			get => this._maxX;
			set
			{
				if (this._maxX == value)
					return;
				this._maxX = value;
				this.UpdateBox();
			}
		}


		public float maxY
		{
			get => this._maxY;
			set
			{
				if (this._maxY == value)
					return;
				this._maxY = value;
				this.UpdateBox();
			}
		}


		public float maxZ
		{
			get => this._maxZ;
			set
			{
				if (this._maxZ == value)
					return;
				this._maxZ = value;
				this.UpdateBox();
			}
		}

		private Vector3 _min;

		public Vector3 min => _min;

		private Vector3 _max;

		public Vector3 max => _max;

		private Vector3 _center;

		public Vector3 center => _center;

		private Vector3 _size;

		public Vector3 size => _size;


		private Vector3 _extents;

		public Vector3 extents => _extents;


		public AABBBox()
		{
		}

		public AABBBox(AABBBox aabbBox)
		{
			this._minX = aabbBox._minX;
			this._minY = aabbBox._minY;
			this._minZ = aabbBox._minZ;

			this._maxX = aabbBox._maxX;
			this._maxY = aabbBox._maxY;
			this._maxZ = aabbBox._maxZ;

			UpdateBox();
		}

		public AABBBox(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
		{
			this._minX = minX;
			this._minY = minY;
			this._minZ = minZ;

			this._maxX = maxX;
			this._maxY = maxY;
			this._maxZ = maxZ;

			UpdateBox();
		}

		public AABBBox(Vector3 min, Vector3 max)
		{
			this._minX = min.x;
			this._minY = min.y;
			this._minZ = min.z;

			this._maxX = max.x;
			this._maxY = max.y;
			this._maxZ = max.z;

			UpdateBox();
		}

		public void SetMin(Vector3 min)
		{
			this._minX = min.x;
			this._minY = min.y;
			this._minZ = min.z;

			UpdateBox();
		}

		public void SetMax(Vector3 max)
		{
			this._maxX = max.x;
			this._maxY = max.y;
			this._maxZ = max.z;

			UpdateBox();
		}

		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this._minX = min.x;
			this._minY = min.y;
			this._minZ = min.z;

			this._maxX = max.x;
			this._maxY = max.y;
			this._maxZ = max.z;

			UpdateBox();
		}


		public void SetCenter(Vector3 center)
		{
			Vector3 extents = this.extents;
			this._minX = center.x - extents.x;
			this._minY = center.y - extents.y;
			this._minZ = center.z - extents.z;

			this._maxX = center.x + extents.x;
			this._maxY = center.y + extents.y;
			this._maxZ = center.z + extents.z;

			UpdateBox();
		}

		public void SetExtents(Vector3 extents)
		{
			Vector3 center = this.center;

			this._minX = center.x - extents.x;
			this._minY = center.y - extents.y;
			this._minZ = center.z - extents.z;

			this._maxX = center.x + extents.x;
			this._maxY = center.y + extents.y;
			this._maxZ = center.z + extents.z;

			UpdateBox();
		}

		public void SetSize(Vector3 size)
		{
			Vector3 extents = size / 2;
			SetExtents(extents);
		}

		private void UpdateBox()
		{
			this._min = new Vector3(_min.x, _min.y, _min.z);
			this._max = new Vector3(_max.x, _max.y, _max.z);
			this._center = _min + _max / 2;
			this._size = new Vector3(Math.Abs(_maxX - _minX), Math.Abs(_maxY - _minY), Math.Abs(_maxZ - _minZ));
			this._extents = _size / 2;
		}

		public override bool IsIntersect(BoxBase other, float tolerence = float.Epsilon)
		{
			var otherAABBBox = other as AABBBox;
			return IsIntersectWithAABBBox(otherAABBBox);
		}

		public bool IsIntersectWithAABBBox(AABBBox otherAABBBox, float tolerence = float.Epsilon)
		{
			if (maxX + tolerence < otherAABBBox.minX || minX > otherAABBBox.maxX + tolerence)
				return false;
			if (maxY + tolerence < otherAABBBox.minY || minY > otherAABBBox.maxY + tolerence)
				return false;
			if (maxZ + tolerence < otherAABBBox.minZ || minZ > otherAABBBox.maxZ + tolerence)
				return false;
			return true;
		}


		public void DoSave(Hashtable dict)
		{
			dict["min"] = min.ToString();
			dict["max"] = max.ToString();
		}

		public void DoRestore(Hashtable dict)
		{
			var min = dict["min"].ToString().ToVector3();
			var max = dict["max"].ToString().ToVector3();
			SetMinMax(min, max);
		}

		public override void DebugDraw(Vector3 offset, Color color)
		{
			Vector3 min = this.min + offset;
			Vector3 max = this.max + offset;
			DrawUtil.DebugCube(min, max, color);
		}

		public override string ToString()
		{
			return string.Format("[min:({0},{1},{2}),max:({3},{4},{5})]", minX, minY, minZ, maxX, maxY, maxZ);
		}
	}
}