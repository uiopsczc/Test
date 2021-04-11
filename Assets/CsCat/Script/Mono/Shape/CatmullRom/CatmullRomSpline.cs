using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class CatmullRomSpline
  {
    public float length;
    public List<CatmullRomSegment> segment_list = new List<CatmullRomSegment>();
    private readonly CatmullRomPoint[] org_points;
    private int segment_num;

    public CatmullRomSpline(CatmullRomPoint[] org_points, int sub_segment_num)
    {
      segment_num = org_points.Length - 1;
      //添加头尾顺延曲线方向的两个点
      var list = new List<CatmullRomPoint>(org_points);
      var first = new CatmullRomPoint(2 * org_points[0].position - org_points[1].position, org_points[0].created_time);
      var last = new CatmullRomPoint(
        2 * org_points[org_points.Length - 1].position - org_points[org_points.Length - 2].position,
        org_points[org_points.Length - 1].created_time);
      list.Insert(0, first);
      list.Add(last);
      this.org_points = list.ToArray();

      length = 0;
      for (var i = 1; i <= this.org_points.Length - 3; i++)
      {
        segment_list.Add(new CatmullRomSegment(this.org_points[i - 1], this.org_points[i], this.org_points[i + 1],
          this.org_points[i + 2], sub_segment_num));
        length += segment_list[segment_list.Count - 1].length;
      }
    }


    public CatmullRomPoint GetPointAtDistance(float distance)
    {
      distance = Mathf.Clamp(distance, 0, length);
      float cur_length = 0;
      float last_length = 0;
      foreach (var segment in segment_list)
      {
        cur_length += segment.length;
        if (cur_length < distance)
        {
          last_length = cur_length;
          continue;
        }

        var subDistance = distance - last_length;
        return segment.GetPointAtDistance(subDistance);
      }

      var last = segment_list[segment_list.Count - 1];
      return last.GetPointAtDistance(last.length);
    }

    public CatmullRomPoint GetTangentAtDistance(float distance)
    {
      distance = Mathf.Clamp(distance, 0, length);
      float cur_length = 0;
      float last_length = 0;
      foreach (var segment in segment_list)
      {
        cur_length += segment.length;
        if (cur_length < distance)
        {
          last_length = cur_length;
          continue;
        }

        var sub_distance = distance - last_length;
        return segment.GetTangentAtDistance(sub_distance);
      }

      var last = segment_list[segment_list.Count - 1];
      return last.GetTangentAtDistance(last.length);
    }

    public static Vector3 ComputeBinormal(Vector3 tangent, Vector3 normal)
    {
      return Vector3.Cross(tangent, normal).normalized;
    }
  }
}