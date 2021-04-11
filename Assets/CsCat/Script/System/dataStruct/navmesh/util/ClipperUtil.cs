using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class ClipperUtil
  {
    #region field

    private static List<PolygonNode> lnSubject;
    private static List<PolygonNode> lnClip;
    private static Polygon pSubject;
    private static Polygon pClip;

    #endregion

    #region static method

    #region 剪裁便利函数

    public static void Cut(Polygon clip, List<Polygon> subject_list)
    {
      clip.SetClockWise(true);
      for (var i = subject_list.Count - 1; i >= 0; i--)
      {
        var subject = subject_list[i];
        subject_list.RemoveAt(i);
        subject.SetClockWise(true);
        var list = Cut(clip, subject);

        if (list != null && list.Count > 0)
          foreach (var k in list)
          {
            k.SetClockWise(true);
            subject_list.Add(k);
          }
      }
    }

    #endregion

    #region 合并便利函数

    public static void Union(List<Polygon> polygon_list)
    {
      for (var n = 0; n < polygon_list.Count; n++)
      {
        var p0 = polygon_list[n];
        for (var m = 0; m < polygon_list.Count; m++)
        {
          var p1 = polygon_list[m];
          if (p0 != p1)
          {
            p0.SetClockWise(true);
            p1.SetClockWise(true);
            var list = Union(p0, p1); //合并
            if (list != null && list.Count > 0)
            {
              polygon_list.Remove(p0);
              polygon_list.Remove(p1);

              foreach (var p in list)
              {
                p.SetClockWise(false);
                polygon_list.Add(p);
              }

              n = 1; //重新开始
              break;
            }

            p0.SetClockWise(false);
            p1.SetClockWise(false);
          }
        }
      }
    }

    #endregion

    #region 剪裁

    public static List<Polygon> Cut(Polygon clip, Polygon subject)
    {
      var ret = new List<Polygon>();
      pClip = clip;
      pSubject = subject;

      //包围盒不相交
      if (pSubject.box_rectangle.IsIntersect(pClip.box_rectangle) == false)
        return null;

      //所有顶点和交点
      lnClip = PolygonNodeUtil.CreateNodes(pClip, false, false); //副多边形
      lnSubject = PolygonNodeUtil.CreateNodes(pSubject, false, true); //主多边形

      //插入交点
      var insCnt = IntersectPoint_Cut();


      //生成多边形
      if (insCnt > 0)
      {
        ret = LinkToPolygon_Cut();
        return ret;
      }

      //0个交点
      //要判断是否主多边形包含副多边形，如果包含，则返回副多边形
      var AContainB = true;
      foreach (var line in pClip.line_list)
        if (!pSubject.Contains(line))
          AContainB = false;
      if (AContainB)
      {
        ret.Add(pClip);
        return ret;
      }

      //要判断是否副多边形包含主多边形，如果包含，则返回主多边形
      var BContainA = true;
      foreach (var line in pSubject.line_list)
        if (!pClip.Contains(line))
          BContainA = false;
      if (BContainA)
      {
        ret.Add(pSubject);
        return ret;
      }

      return null;
    }


    // 生成的多边形
    private static List<Polygon> LinkToPolygon_Cut()
    {
      //保存合并后的多边形数组
      var rtV = new List<Polygon>();

      for (var i = 0; i < lnClip.Count; i++)
      {
        var testNode = lnClip[i];
        //1. 选取任一没有被跟踪过的交点为始点，将其输出到结果多边形顶点表中．
        if (testNode.is_intersect_point && testNode.is_processed == false)
        {
          var rcNodes = new List<Vector2>();
          while (testNode != null)
          {
            testNode.is_processed = true;

            // 如果是交点
            if (testNode.is_intersect_point)
            {
              testNode.other.is_processed = true;
              if (testNode.is_out_point == false) //该交点为进点（跟踪主多边形边界）
              {
                if (testNode.is_subject == false) //当前点在副多边形中
                  testNode = testNode.other; //切换到主多边形中
              }
              else //该交点为出点（跟踪副多边形边界）
              {
                if (testNode.is_subject) //当前点在主多边形中
                  testNode = testNode.other; //切换到副多边形中
              }
            }

            rcNodes.Add(testNode.vertex); ////// 如果是多边形顶点，将其输出到结果多边形顶点表中

            if (testNode.next == null) //末尾点返回到开始点
            {
              if (testNode.is_subject)
                testNode = lnSubject[0];
              else
                testNode = lnClip[0];
            }
            else
            {
              testNode = testNode.next;
            }

            //与首点相同，生成一个多边形
            if (testNode.vertex == rcNodes[0])
              break;
          }

          //提取
          rtV.Add(new Polygon(rcNodes.ToArray()));
        }
      }

      return rtV;
    }


    private static int IntersectPoint_Cut()
    {
      var insCnt = 0; //交点数
      var nSubjectCur = lnSubject[0];
      PolygonNode nClipCur;
      Line lSubject;
      Line lClip;
      bool is_has_intersect_point; //是否有交点
      while (nSubjectCur != null) //主多边形
      {
        lSubject = nSubjectCur.next == null
          ? new Line(nSubjectCur.vertex, lnSubject[0].vertex)
          : new Line(nSubjectCur.vertex, nSubjectCur.next.vertex);

        //奇异情况:与裁剪多边形重合的主多边形的边不参与求交点 http://course.baidu.com/view/58a00497daef5ef7ba0d3c52.html
        if (IsOverLap_Cut(lSubject, pClip))
        {
          nSubjectCur = nSubjectCur.next;
          continue;
        }


        nClipCur = lnClip[0];
        is_has_intersect_point = false;

        while (nClipCur != null)
        {
          lClip = nClipCur.next == null
            ? new Line(nClipCur.vertex, lnClip[0].vertex)
            : new Line(nClipCur.vertex, nClipCur.next.vertex);

          Vector2? intersectPoint;
          var lineRelation = lSubject.Intersection(lClip, out intersectPoint);
          if (lineRelation == Line.LineClassification.SEGMENTS_INTERSECT) //相交
          {
            if (lClip.ClassifyPoint(lSubject.point_A) == PointClassification.ON_SEGMENT &&
                GetNodeIndex(lnClip, lSubject.point_A) == -1)
              if (!IsOutter(lSubject, pClip))
              {
                insCnt++;
                var nSubjectInter = nSubjectCur;
                nSubjectInter.is_intersect_point = true;


                var nClipInter = new PolygonNode(lSubject.point_A, true, false);
                lnClip.Add(nClipInter);

                nClipInter.other = nSubjectInter;
                nSubjectInter.other = nClipInter;

                nClipCur = lnClip[GetNodeIndex(lnClip, lClip.point_A)];
                nClipInter.next = nClipCur.next;
                nClipCur.next = nClipInter;


                nSubjectInter.is_out_point = false;
                nClipInter.is_out_point = false;
              }

            if (lClip.ClassifyPoint(lSubject.point_B) == PointClassification.ON_SEGMENT &&
                GetNodeIndex(lnClip, lSubject.point_B) == -1)
              if (!IsOutter(lSubject, pClip))
              {
                insCnt++;
                var nSubjectInter = lnSubject[GetNodeIndex(lnSubject, lSubject.point_B)];
                nSubjectInter.is_intersect_point = true;

                var nClipInter = new PolygonNode(lSubject.point_B, true, false);
                lnClip.Add(nClipInter);

                nSubjectInter.other = nClipInter;
                nClipInter.other = nSubjectInter;

                nClipInter.next = nClipCur.next;
                nClipCur.next = nClipInter;


                nSubjectInter.is_out_point = true;
                nClipInter.is_out_point = true;
              }

            //忽略交点已在顶点列表中的                        
            if (GetNodeIndex(lnSubject, intersectPoint.Value) == -1)
            {
              insCnt++;
              ///////// 插入交点
              var nSubjectInter = new PolygonNode(intersectPoint.Value, true, true);
              var nClipInter = new PolygonNode(intersectPoint.Value, true, false);

              lnSubject.Add(nSubjectInter);
              lnClip.Add(nClipInter);
              //双向引用
              nSubjectInter.other = nClipInter;
              nClipInter.other = nSubjectInter;

              //插入
              nSubjectInter.next = nSubjectCur.next;
              nSubjectCur.next = nSubjectInter;
              nClipInter.next = nClipCur.next;
              nClipCur.next = nClipInter;


              //出点
              if (lSubject.ClassifyPoint(lClip.point_B) == PointClassification.RIGHT_SIDE)
              {
                nSubjectInter.is_out_point = true;
                nClipInter.is_out_point = true;
              }
              else
              {
                nSubjectInter.is_out_point = false;
                nClipInter.is_out_point = false;
              }

              is_has_intersect_point = true; //有交点				
              //有交点，返回重新处理
              break;
            }
          }

          nClipCur = nClipCur.next;
        }

        //如果没有交点继续处理下一个边，否则重新处理该点与插入的交点所形成的线段
        //原因：这条线段上还可能有其他的交点，注意hasIntersectPoint是只检测一次true就break，而且无法确定同一条边的交点先后顺序，所以如果有交点的时候要重新处理该点与插入的交点所形成的线段
        if (is_has_intersect_point == false)
          nSubjectCur = nSubjectCur.next;
      }

      return insCnt;
    }

    //是否与重叠
    private static bool IsOverLap_Cut(Line line1, Polygon polygon2)
    {
      var line2s = polygon2.line_list;
      foreach (var line2 in line2s)
      {
        Vector2? intersectPoint;
        var lineRelation = line2.Intersection(line1, out intersectPoint);
        if (lineRelation == Line.LineClassification.COLLINEAR)
          return true;
      }

      return false;
    }

    #endregion

    #region 合并

    //不能出现A多边形的连接两条边的顶点在B多边形的一条边上的情况
    public static List<Polygon> Union(Polygon clip, Polygon subject)
    {
      var ret = new List<Polygon>();
      pClip = clip;
      pSubject = subject;

      //包围盒不相交
      if (pSubject.box_rectangle.IsIntersect(pClip.box_rectangle) == false)
        return null;

      //所有顶点和交点
      lnClip = PolygonNodeUtil.CreateNodes(clip, false, false); //副多边形
      lnSubject = PolygonNodeUtil.CreateNodes(subject, false, true); //主多边形

      //插入交点
      var insCnt = IntersectPoint_Union();

      //生成多边形
      if (insCnt > 0)
      {
        ret = LinkToPolygon_Union();
        return ret;
      }

      return null;
    }

    private static List<Polygon> LinkToPolygon_Union()
    {
      //保存合并后的多边形数组
      var rcNodes = new List<Vector2>();
      var startV = GetLeftBottomVertex();
      PolygonNode node = null;
      if (GetNodeIndex(lnSubject, startV) != -1)
        node = lnSubject[GetNodeIndex(lnSubject, startV)];
      else if (GetNodeIndex(lnClip, startV) != -1)
        node = lnClip[GetNodeIndex(lnClip, startV)];

      while (node != null)
      {
        rcNodes.Add(node.vertex);
        if (node.is_intersect_point)
        {
          if (node.is_out_point == false) //该交点为进点（跟踪主多边形边界）
          {
            if (node.is_subject == false) //当前点在副多边形中
              node = node.other; //切换到主多边形中
          }
          else //该交点为出点（跟踪副多边形边界）
          {
            if (node.is_subject) //当前点在主多边形中
              node = node.other; //切换到副多边形中
          }
        }

        if (node.next == null) //末尾点返回到开始点
        {
          if (node.is_subject)
            node = lnSubject[0];
          else
            node = lnClip[0];
        }
        else
        {
          node = node.next;
        }

        //与首点相同，生成一个多边形
        if (node.vertex == rcNodes[0])
          break;
      }

      var ret = new List<Polygon>();
      ret.Add(new Polygon(rcNodes.ToArray()));

      return ret;
    }

    private static int IntersectPoint_Union()
    {
      var insCnt = 0; //交点数
      var nSubjectCur = lnSubject[0];
      PolygonNode nClipCur;
      Line lSubject;
      Line lClip;
      bool hasIntersectPoint; //是否有交点
      while (nSubjectCur != null) //主多边形
      {
        lSubject = nSubjectCur.next == null
          ? new Line(nSubjectCur.vertex, lnSubject[0].vertex)
          : new Line(nSubjectCur.vertex, nSubjectCur.next.vertex);

        //奇异情况:与裁剪多边形重合的主多边形的边不参与求交点 http://course.baidu.com/view/58a00497daef5ef7ba0d3c52.html
        if (IsOverLap_Union(lSubject, pClip))
        {
          nSubjectCur = nSubjectCur.next;
          continue;
        }

        nClipCur = lnClip[0];
        hasIntersectPoint = false;

        while (nClipCur != null)
        {
          lClip = nClipCur.next == null
            ? new Line(nClipCur.vertex, lnClip[0].vertex)
            : new Line(nClipCur.vertex, nClipCur.next.vertex);

          Vector2? intersectPoint;
          var lineRelation = lSubject.Intersection(lClip, out intersectPoint);
          if (lineRelation == Line.LineClassification.SEGMENTS_INTERSECT) //相交
          {
            var a_on_lClip = lClip.ClassifyPoint(lSubject.point_A) == PointClassification.ON_SEGMENT;
            var a_index_of_lnClip = GetNodeIndex(lnClip, lSubject.point_A);
            var b_on_lClip = lClip.ClassifyPoint(lSubject.point_B) == PointClassification.ON_SEGMENT;
            var b_index_of_lnClip = GetNodeIndex(lnClip, lSubject.point_B);

            //主多边形边上的顶点A是在副多边形上
            if (a_on_lClip && (a_index_of_lnClip == -1 ||
                               a_index_of_lnClip != -1 && lnClip[a_index_of_lnClip].is_checked == false))
            {
              insCnt++;
              PolygonNode nSubjectInter = null;
              PolygonNode nClipInter = null;

              if (GetNodeIndex(lnSubject, lSubject.point_A) == -1)
              {
                nSubjectInter = new PolygonNode(lSubject.point_A, true, true);
                nSubjectInter.is_checked = true;
                lnSubject.Add(nSubjectInter);
                nSubjectInter.next = nSubjectCur.next;
                nSubjectCur.next = nSubjectInter;
                hasIntersectPoint = true;
              }
              else
              {
                nSubjectInter = lnSubject[GetNodeIndex(lnSubject, lSubject.point_A)];
                nSubjectInter.is_intersect_point = true;
                nSubjectInter.is_checked = true;
              }

              if (GetNodeIndex(lnClip, lSubject.point_A) == -1)
              {
                nClipInter = new PolygonNode(lSubject.point_A, true, false);
                nClipInter.is_checked = true;
                lnClip.Add(nClipInter);
                nClipInter.next = nClipCur.next;
                nClipCur.next = nClipInter;
              }
              else
              {
                nClipInter = lnClip[GetNodeIndex(lnClip, lSubject.point_A)];
                nClipInter.is_checked = true;
                nClipInter.is_intersect_point = true;
              }


              //双向引用
              nSubjectInter.other = nClipInter;
              nClipInter.other = nSubjectInter;

              if (!IsOutter(lSubject, pClip))
              {
                nSubjectInter.is_out_point = true;
                nClipInter.is_out_point = true;
              }
              else
              {
                nSubjectInter.is_out_point = false;
                nClipInter.is_out_point = false;
              }
            }

            //主多边形边上的顶点B是在副多边形上
            if (b_on_lClip && (b_index_of_lnClip == -1 ||
                               b_index_of_lnClip != -1 && lnClip[b_index_of_lnClip].is_checked == false))
            {
              insCnt++;
              PolygonNode nSubjectInter = null;
              PolygonNode nClipInter = null;

              if (GetNodeIndex(lnSubject, lSubject.point_B) == -1)
              {
                nSubjectInter = new PolygonNode(lSubject.point_B, true, true);
                nSubjectInter.is_checked = true;
                lnSubject.Add(nSubjectInter);
                nSubjectInter.next = nSubjectCur.next;
                nSubjectCur.next = nSubjectInter;
                hasIntersectPoint = true;
              }
              else
              {
                nSubjectInter = lnSubject[GetNodeIndex(lnSubject, lSubject.point_B)];
                nSubjectInter.is_intersect_point = true;
                nSubjectInter.is_checked = true;
              }

              if (GetNodeIndex(lnClip, lSubject.point_B) == -1)
              {
                nClipInter = new PolygonNode(lSubject.point_B, true, false);
                nClipInter.is_checked = true;
                lnClip.Add(nClipInter);
                nClipInter.next = nClipCur.next;
                nClipCur.next = nClipInter;
              }
              else
              {
                nClipInter = lnClip[GetNodeIndex(lnClip, lSubject.point_B)];
                nClipInter.is_checked = true;
                nClipInter.is_intersect_point = true;
              }

              nSubjectInter.other = nClipInter;
              nClipInter.other = nSubjectInter;

              if (!IsOutter(lSubject, pClip))
              {
                nSubjectInter.is_out_point = false;
                nClipInter.is_out_point = false;
              }
              else
              {
                nSubjectInter.is_out_point = true;
                nClipInter.is_out_point = true;
              }
            }

            //交点不在顶点上的情况（主多边形的一条线段肯能跟副多边形的多条边相交）
            //忽略交点已在顶点列表中的                        
            if (GetNodeIndex(lnSubject, intersectPoint.Value) == -1)
            {
              insCnt++;
              ///////// 插入交点
              var nSubjectInter = new PolygonNode(intersectPoint.Value, true, true);
              nSubjectInter.is_checked = true;
              var nClipInter = new PolygonNode(intersectPoint.Value, true, false);
              nClipInter.is_checked = true;

              lnSubject.Add(nSubjectInter);
              lnClip.Add(nClipInter);
              //双向引用
              nSubjectInter.other = nClipInter;
              nClipInter.other = nSubjectInter;
              //插入
              nSubjectInter.next = nSubjectCur.next;
              nSubjectCur.next = nSubjectInter;
              nClipInter.next = nClipCur.next;
              nClipCur.next = nClipInter;


              //出点
              if (lSubject.ClassifyPoint(lClip.point_B) == PointClassification.LEFT_SIDE)
              {
                nSubjectInter.is_out_point = true;
                nClipInter.is_out_point = true;
              }
              else
              {
                nSubjectInter.is_out_point = false;
                nClipInter.is_out_point = false;
              }

              hasIntersectPoint = true; //有交点				
              //有交点，返回重新处理
              break;
            }
          }

          nClipCur = nClipCur.next;
        }

        //如果没有交点继续处理下一个边，否则重新处理该点与插入的交点所形成的线段
        //原因：这条线段上还可能有其他的交点，注意hasIntersectPoint是只检测一次true就break，而且无法确定同一条边的交点先后顺序，所以如果有交点的时候要重新处理该点与插入的交点所形成的线段
        if (hasIntersectPoint == false)
          nSubjectCur = nSubjectCur.next;
      }

      return insCnt;
    }

    //是否与重叠;两个端点都落在副多边形的一条边上才算
    private static bool IsOverLap_Union(Line line1, Polygon polygon2)
    {
      /*
          List<Line> line2s = polygon2.GetLines();
          Vector2 pointA1 = line1.PointA;
          Vector2 pointB1 = line1.PointB;
          foreach (Line line2 in line2s)
          {
              if (line2.ClassifyPoint(pointA1) == PointClassification.ON_SEGMENT && line2.ClassifyPoint(pointB1) == PointClassification.ON_SEGMENT)
              {
                  return true;
              }
          }
           * */
      var line2s = polygon2.line_list;
      foreach (var line2 in line2s)
        if (line2.Contains(line1))
          return true;

      return false;
    }

    #endregion


    #region priavte

    //找最左下角那个点
    //http://stackoverflow.com/questions/2667748/how-do-i-combine-complex-polygons  的Step 3. Find left-bottom vertex.
    private static Vector2 GetLeftBottomVertex()
    {
      var vs = new List<Vector2>();
      vs.AddRange(pSubject.vertexe_list);
      vs.AddRange(pClip.vertexe_list);
      var leftBottom = vs[0];
      foreach (var v in vs)
        if (v.x < leftBottom.x)
          leftBottom = v;
        else if (v.x == leftBottom.x)
          if (v.y < leftBottom.y)
            leftBottom = v;
      return leftBottom;
    }

    //取得节点的索引
    private static int GetNodeIndex(List<PolygonNode> ln, Vector2 point)
    {
      for (var i = 0; i < ln.Count; i++)
        if (point.EqualsEPSILON(ln[i].vertex))
          return i;
      return -1;
    }

    //线段是否在多边形的外侧,polygon2副多边形
    private static bool IsOutter(Line line, Polygon polygon2)
    {
      var intersectPoints = new List<Vector2>();
      foreach (var line2 in polygon2.line_list)
      {
        Vector2? intersectPoint;
        var lineRelation = line2.Intersection(line, out intersectPoint);
        if (lineRelation == Line.LineClassification.COLLINEAR)
          return false;
        if (lineRelation == Line.LineClassification.SEGMENTS_INTERSECT)
          if (!intersectPoints.Contains(intersectPoint.Value))
            intersectPoints.Add(intersectPoint.Value);
      }

      if (!intersectPoints.Contains(line.point_B))
        intersectPoints.Add(line.point_B);
      if (!intersectPoints.Contains(line.point_A))
        intersectPoints.Insert(0, line.point_A);
      for (var i = 1; i < intersectPoints.Count; i++)
      {
        var v1 = intersectPoints[i - 1];
        var v2 = intersectPoints[i];
        var v = (v1 + v2) / 2;
        if (polygon2.Contains(v))
          return false;
      }

      return true;
    }

    #endregion

    #endregion
  }
}