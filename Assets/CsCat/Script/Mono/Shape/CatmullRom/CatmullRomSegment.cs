using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class Knot
  {
    public Vector3 position;
    public Vector3 tangent;
    public float created_time;


    public Knot(Vector3 position, Vector3 tangent, float created_time)
    {
      this.position = position;
      this.tangent = tangent;
      this.created_time = created_time;
    }
  }

  public class Segment
  {
    public Knot end;
    public float length;
    public Knot start;

    public Segment(Knot start, Knot end)
    {
      this.start = start;
      this.end = end;
      length = (this.end.position - this.start.position).magnitude;
    }

    public Knot GetKnot(float lerp)
    {
      var position = Vector3.Lerp(start.position, end.position, lerp);
      var tangent = Vector3.Lerp(start.tangent, end.tangent, lerp).normalized;
      var create_time = Mathf.Lerp(start.created_time, end.created_time, lerp);
      return new Knot(position, tangent, create_time);
    }
  }

  public class CatmullRomSegment
  {
    public float length;
    private readonly CatmullRomPoint p0;
    private readonly CatmullRomPoint p1;
    private readonly CatmullRomPoint p2;
    private readonly CatmullRomPoint p3;
    public int subSegmentNum;

    public List<Segment> sub_segment_list = new List<Segment>();

    public CatmullRomSegment(CatmullRomPoint p0, CatmullRomPoint p1, CatmullRomPoint p2, CatmullRomPoint p3,
      int sub_segment_num)
    {
      this.p0 = p0;
      this.p1 = p1;
      this.p2 = p2;
      this.p3 = p3;
      this.subSegmentNum = sub_segment_num;

      length = 0;
      for (float i = 0; i < sub_segment_num; i++)
      {
        var point0 = GetPoint(i / sub_segment_num);
        var tangent0 = GetTangent(i / sub_segment_num);
        var create_time0 = Mathf.Lerp(p2.created_time, p3.created_time, i / sub_segment_num);
        var point1 = GetPoint((i + 1) / sub_segment_num);
        var tangent1 = GetTangent((i + 1) / sub_segment_num);
        var create_time1 = Mathf.Lerp(p2.created_time, p3.created_time, (i + 1) / sub_segment_num);
        var segment = new Segment(new Knot(point0, tangent0, create_time0), new Knot(point1, tangent1, create_time1));
        sub_segment_list.Add(segment);
        length += segment.length;
      }
    }


    public Vector3 GetPoint(float t)
    {
      var ret = new Vector3();

      var t2 = t * t;
      var t3 = t2 * t;

      ret = 0.5f * (2.0f * p1.position +
                    (-p0.position + p2.position) * t +
                    (2.0f * p0.position - 5.0f * p1.position + 4 * p2.position - p3.position) * t2 +
                    (-p0.position + 3.0f * p1.position - 3.0f * p2.position + p3.position) * t3);
      return ret;
    }

    public Vector3 GetTangent(float t)
    {
      var ret = new Vector3();

      var t2 = t * t;

      ret = 0.5f * (-p0.position + p2.position) +
            (2.0f * p0.position - 5.0f * p1.position + 4 * p2.position - p3.position) * t +
            (-p0.position + 3.0f * p1.position - 3.0f * p2.position + p3.position) * t2 * 1.5f;
      ret.Normalize();
      return ret;
      ;
    }

    public Knot GetKnotAtDistance(float distance)
    {
      float cur_distance = 0;
      Segment target = null;
      float lerp = 0;

      foreach (var sub_segment in sub_segment_list)
      {
        cur_distance += sub_segment.length;
        if (cur_distance >= distance)
        {
          lerp = (sub_segment.length - (cur_distance - distance)) / sub_segment.length;
          target = sub_segment;
          break;
        }
      }

      if (distance + 0.001f >= length)
      {
        target = sub_segment_list[sub_segment_list.Count - 1];
        lerp = 1;
      }
      else if (distance <= 0)
      {
        target = sub_segment_list[0];
        lerp = 0;
      }

      //LogCat.LogWarning(subSegments.Count);
      //if (target == null)
      //{
      //    LogCat.LogWarning(subSegments.Count);
      //    LogCat.LogWarning(curDistance);
      //    LogCat.LogWarning(distance);
      //}
      return target.GetKnot(lerp);
      ;
    }

    public CatmullRomPoint GetPointAtDistance(float distance)
    {
      var knot = GetKnotAtDistance(distance);
      return new CatmullRomPoint(knot.position, knot.created_time);
    }


    public CatmullRomPoint GetTangentAtDistance(float distance)
    {
      var knot = GetKnotAtDistance(distance);
      return new CatmullRomPoint(knot.tangent, knot.created_time);
    }
  }
}