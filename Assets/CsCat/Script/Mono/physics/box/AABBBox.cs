using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class AABBBox:BoxBase
  {
    private float _min_x;
    private float _min_y;
    private float _min_z;

    private float _max_x;
    private float _max_y;
    private float _max_z;

    public float min_x
    {
      get { return this._min_x; }
      set
      {
        if (this._min_x == value)
          return;
        this._min_x = value;
        this.UpdateBox();
      }
    }

    public float min_y
    {
      get { return this._min_y; }
      set
      {
        if (this._min_y == value)
          return;
        this._min_y = value;
        this.UpdateBox();
      }
    }

    public float min_z
    {
      get { return this._min_z; }
      set
      {
        if (this._min_z == value)
          return;
        this._min_z = value;
        this.UpdateBox();
      }
    }

    public float max_x
    {
      get { return this._max_x; }
      set
      {
        if (this._max_x == value)
          return;
        this._max_x = value;
        this.UpdateBox();
      }
    }


    public float max_y
    {
      get { return this._max_y; }
      set
      {
        if (this._max_y == value)
          return;
        this._max_y = value;
        this.UpdateBox();
      }
    }


    public float max_z
    {
      get { return this._max_z; }
      set
      {
        if (this._max_z == value)
          return;
        this._max_z = value;
        this.UpdateBox();
      }
    }

    private Vector3 _min;

    public Vector3 min
    {
      get { return _min; }
    }

    private Vector3 _max;

    public Vector3 max
    {
      get { return _max; }
    }

    private Vector3 _center;

    public Vector3 center
    {
      get { return _center; }
    }

    private Vector3 _size;

    public Vector3 size
    {
      get { return _size; }
    }


    private Vector3 _extents;

    public Vector3 extents
    {
      get { return _extents; }
    }


    public AABBBox()
    {
    }

    public AABBBox(AABBBox aabbBox)
    {
      this._min_x = aabbBox._min_x;
      this._min_y = aabbBox._min_y;
      this._min_z = aabbBox._min_z;

      this._max_x = aabbBox._max_x;
      this._max_y = aabbBox._max_y;
      this._max_z = aabbBox._max_z;

      UpdateBox();
    }

    public AABBBox(float min_x, float min_y, float min_z, float max_x, float max_y, float max_z)
    {
      this._min_x = min_x;
      this._min_y = min_y;
      this._min_z = min_z;

      this._max_x = max_x;
      this._max_y = max_y;
      this._max_z = max_z;

      UpdateBox();
    }

    public AABBBox(Vector3 min, Vector3 max)
    {
      this._min_x = min.x;
      this._min_y = min.y;
      this._min_z = min.z;

      this._max_x = max.x;
      this._max_y = max.y;
      this._max_z = max.z;

      UpdateBox();
    }

    public void SetMin(Vector3 min)
    {
      this._min_x = min.x;
      this._min_y = min.y;
      this._min_z = min.z;

      UpdateBox();
    }

    public void SetMax(Vector3 max)
    {
      this._max_x = max.x;
      this._max_y = max.y;
      this._max_z = max.z;

      UpdateBox();
    }

    public void SetMinMax(Vector3 min, Vector3 max)
    {
      this._min_x = min.x;
      this._min_y = min.y;
      this._min_z = min.z;

      this._max_x = max.x;
      this._max_y = max.y;
      this._max_z = max.z;

      UpdateBox();
    }


    public void SetCenter(Vector3 center)
    {
      Vector3 extents = this.extents;
      this._min_x = center.x - extents.x;
      this._min_y = center.y - extents.y;
      this._min_z = center.z - extents.z;

      this._max_x = center.x + extents.x;
      this._max_y = center.y + extents.y;
      this._max_z = center.z + extents.z;

      UpdateBox();
    }

    public void SetExtents(Vector3 extents)
    {
      Vector3 center = this.center;

      this._min_x = center.x - extents.x;
      this._min_y = center.y - extents.y;
      this._min_z = center.z - extents.z;

      this._max_x = center.x + extents.x;
      this._max_y = center.y + extents.y;
      this._max_z = center.z + extents.z;

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
      this._size = new Vector3(Math.Abs(_max_x - _min_x), Math.Abs(_max_y - _min_y), Math.Abs(_max_z - _min_z));
      this._extents = _size / 2;
    }

    public override bool IsIntersect(BoxBase other, float tolerence = float.Epsilon)
    {
      var other_aabbBox = other as AABBBox;
      return IsIntersect_With_AABBBox(other_aabbBox);
    }

    public bool IsIntersect_With_AABBBox(AABBBox other_aabbBox, float tolerence = float.Epsilon)
    {
      if (max_x + tolerence < other_aabbBox.min_x || min_x > other_aabbBox.max_x + tolerence)
        return false;
      if (max_y + tolerence < other_aabbBox.min_y || min_y > other_aabbBox.max_y + tolerence)
        return false;
      if (max_z + tolerence < other_aabbBox.min_z || min_z > other_aabbBox.max_z + tolerence)
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
      DrawUtil.DebugCube(min,max,color);
    }

    public override string ToString()
    {
      return string.Format("[min:({0},{1},{2}),max:({3},{4},{5})]", min_x, min_y, min_z, max_x, max_y, max_z);
    }

    
  }
}