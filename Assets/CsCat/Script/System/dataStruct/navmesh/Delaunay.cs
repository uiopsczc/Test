using UnityEngine;
using System.Collections.Generic;
using System;

namespace CsCat
{
  /// <summary>
  /// http://blog.163.com/caty_nuaa/blog/static/9039072010425238645/
  /// </summary>
  public class Delaunay
  {
    #region field

    /// <summary>
    /// 所有多边形，第0个元素为区域外的边界
    /// </summary>
    private List<Polygon> polygon_list;

    /// <summary>
    /// 所有顶点列表，前outEdgeVecNum个为外边界
    /// </summary>
    private List<Vector2> vertex_list;

    /// <summary>
    /// 所有约束边
    /// </summary>
    private List<Line> edge_list;

    /// <summary>
    /// 区域外边界顶点数
    /// </summary>
    private int out_edge_vec_nmu;

    /// <summary>
    /// 线段堆栈
    /// </summary>
    private List<Line> line_list;

    /// <summary>
    /// 生成的Delaunay三角形
    /// </summary>
    private List<Triangle> triangle_list;

    #endregion

    #region ctor

    public Delaunay()
    {
    }

    #endregion


    #region public method

    public List<Cell> GetCells()
    {
      List<Cell> cell_list = new List<Cell>();
      for (int j = 0; j < triangle_list.Count; j++)
      {
        Triangle triangle = triangle_list[j];
        Cell cell = new Cell(triangle.pointA, triangle.pointB, triangle.pointC);
        cell.index = j;
        cell_list.Add(cell);
      }

      LinkCells(cell_list);
      return cell_list;
    }

    /// <summary>
    /// 搜索单元网格的邻接网格，并保存链接数据到网格中，以提供给寻路用
    /// </summary>
    /// <param name="pv"></param>
    public void LinkCells(List<Cell> pv)
    {
      foreach (Cell cell_A in pv)
      {
        foreach (Cell cell_B in pv)
        {
          if (cell_A != cell_B)
          {
            cell_A.CheckAndLink(cell_B);
          }
        }
      }
    }

    public List<Triangle> CreateDelaunay(List<Polygon> polygon_list)
    {
      //Step1.  建立单元大小为 E*E 的均匀网格，并将多边形的顶点和边放入其中.
      //         其中 E=sqrt(w*h/n)，w 和 h 分别为多边形域包围盒的宽度、高度，n 为多边形域的顶点数 
      this.polygon_list = polygon_list;
      InitData(polygon_list);

      //Step2.    取任意一条外边界边 p1p2 .
      Line init_edge = GetInitOutEdge();
      this.line_list.Add(init_edge);

      Line edge;
      do
      {
        //Step3.     计算 DT 点 p3，构成约束 Delaunay 三角形 Δp1p2p3 .
        edge = this.line_list[this.line_list.Count - 1];
        line_list.RemoveAt(this.line_list.Count - 1);
        //开始处理edge;
        Vector2? p3 = FindDT(edge);








        if (p3 == null)
          continue;
        Line line13 = new Line(edge.pointA, p3.Value);
        Line line32 = new Line(p3.Value, edge.pointB);



        //Delaunay三角形放入输出数组
        Triangle triangle = new Triangle(edge.pointA, edge.pointB, p3.Value);
        this.triangle_list.Add(triangle);

        //Step4.    如果新生成的边 p1p3 不是约束边，若已经在堆栈中，
        //         则将其从中删除；否则，将其放入堆栈；类似地，可处理 p3p2 .
        int index;
        if (IndexOfList(line13, this.edge_list) < 0)
        {
          index = IndexOfList(line13, this.line_list);
          if (index > -1)
            line_list.RemoveAt(index);
          else
            line_list.Add(line13);
        }

        if (IndexOfList(line32, this.edge_list) < 0)
        {
          index = IndexOfList(line32, this.line_list);
          if (index > -1)
            line_list.RemoveAt(index);
          else
            line_list.Add(line32);
        }

        //Step5.    若堆栈不空，则从中取出一条边，转Step3；否则，算法停止 .
      } while (this.line_list.Count > 0);

      return triangle_list;
    }

    /// <summary>
    /// 不与该多边形的边重合(好像是没用的，姑且留着)
    /// </summary>
    /// <param name="line2"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public bool ContainInner(Line line2, Polygon polygon)
    {
      if (polygon.Contains(line2))
      {
        Vector2 dir2 = line2.GetDirection();
        foreach (Line line1 in polygon.lineList)
        {
          Vector2 dir1 = line1.GetDirection();
          if (dir2.EqualsEPSILON(dir1) || dir2.EqualsEPSILON(-dir1))
            return false;
        }

        return true;
      }
      else
        return false;
    }

    #endregion

    #region private method

    private void InitData(List<Polygon> polygon_list)
    {
      //填充顶点和线列表
      this.vertex_list = new List<Vector2>();
      this.edge_list = new List<Line>();
      foreach (Polygon polygon in polygon_list)
      {
        PutVertex(this.vertex_list, polygon.vertexList);
        PutEdge(this.edge_list, polygon.vertexList);
      }

      out_edge_vec_nmu = polygon_list[0].vertexList.Count;

      this.line_list = new List<Line>();
      this.triangle_list = new List<Triangle>();
    }

    /// <summary>
    /// 将src中的点放入dst     
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="src"></param>
    private void PutVertex(List<Vector2> dst, List<Vector2> src)
    {
      foreach (Vector2 v in src)
        dst.Add(v);

    }

    /// <summary>
    /// 根据src中的点生成多边形线段，并放入dst     
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="src"></param>
    private void PutEdge(List<Line> dst, List<Vector2> src)
    {
      if (src.Count < 3) //不是一个多边形
        return;
      Vector2 p1 = src[0];
      Vector2 p2;
      for (int i = 1; i < src.Count; i++)
      {
        p2 = src[i];
        dst.Add(new Line(p1, p2));
        p1 = p2;
      }

      p2 = src[0];
      dst.Add(new Line(p1, p2));

    }

    /// <summary>
    /// 获取初始外边界    
    /// </summary>
    private Line GetInitOutEdge()
    {
      Line init_edge = this.edge_list[0];
      //检查是否有顶点p在该边上，如果有则换一个外边界
      bool is_loop_sign;
      int loop_index = 0;
      do
      {
        is_loop_sign = false;
        loop_index++;
        foreach (Vector2 vertex in this.vertex_list)
        {
          if (vertex.Equals(init_edge.pointA) || vertex.Equals(init_edge.pointB))
            continue;
          if (init_edge.ClassifyPoint(vertex) == PointClassification.OnLine ||
              init_edge.ClassifyPoint(vertex) == PointClassification.OnSegment)
          {
            is_loop_sign = true;
            init_edge = this.edge_list[loop_index];
            break;
          }
        }
      } while (is_loop_sign && loop_index < out_edge_vec_nmu - 1); //只取外边界

      return init_edge;
    }

    /// <summary>
    /// 判断线段是否是约束边,return 线段的索引，如果没有找到，返回-1     
    /// </summary>
    /// <param name="line"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private int IndexOfList(Line line, List<Line> list)
    {
      Line lt;
      for (int i = 0; i < list.Count; i++)
      {
        lt = list[i];
        if (lt.Equals(line))
          return i;
      }

      return -1;
    }


    /// <summary>
    /// 好像是没用的，姑且留着
    /// </summary>
    /// <param name="line1"></param>
    /// <param name="line2"></param>
    /// <param name="line3"></param>
    /// <returns></returns>
    bool IsInSamePolygon(Line line1, Line line2, Line line3)
    {
      for (int i = 1; i < polygon_list.Count; i++)
      {
        Polygon p = polygon_list[i];
        if (ContainInner(line1, p) || ContainInner(line2, p) || ContainInner(line3, p))
          return true;
      }

      return false;
    }

    /// <summary>
    /// 计算 DT 点
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private Vector2? FindDT(Line line)
    {
      Vector2 p1 = line.pointA;
      Vector2 p2 = line.pointB;

      //搜索所有可见点             TODO 按y方向搜索距线段终点最近的点
      List<Vector2> all_point_list = new List<Vector2>(); //搜索所有可见点,按逆时针加入符合条件的点（一定要逆时针，否则会出bug）

      foreach (Vector2 vertex in this.vertex_list)
      {
        if (IsVisiblePointOfLine(vertex, line))
        {
          //判断是否在同一个多边形内
          Line line1 = new Line(p1, vertex);
          Line line2 = new Line(vertex, p2);
          Line line3 = new Line(p2, p1);

          //好像是没用的，姑且留着
          if (IsInSamePolygon(line1, line2, line3))
            continue;
          all_point_list.Add(vertex);
          //AddToSmallestTopList(allVPointsturts, vt, Vector2.Dot(line.GetDirection(), ((vt - line.PointA).normalized)));
        }
      }

      ///最小值放在最前面的list
      all_point_list.Sort(delegate (Vector2 v1, Vector2 v2)
        {
          float d1 = Vector2.Dot(line.GetDirection(), ((v1 - line.pointA).normalized));
          float d2 = Vector2.Dot(line.GetDirection(), ((v2 - line.pointA).normalized));
          if (d1 > d2)
            return -1;
          else if (d1 == d2)
            return 0;
          else
            return 1;

        }
      );
      if (all_point_list.Count == 0)
        return null;

      Vector2 p3 = all_point_list[0];

      bool is_loop_sign = false;
      do
      {
        is_loop_sign = false;

        //Step1. 构造 Δp1p2p3 的外接圆 C（p1，p2，p3）及其网格包围盒 B（C（p1，p2，p3））
        Circle circle = this.CircumCircle(p1, p2, p3);
        Rectangle bounds_box = this.CircleBounds(circle);

        //Step2. 依次访问网格包围盒内的每个网格单元：
        //         若某个网格单元中存在可见点 p, 并且 ∠p1pp2 > ∠p1p3p2，则令 p3=p，转Step1；否则，转Step3.
        float angle132 = Math.Abs(LineAngle(p1, p3, p2)); // ∠p1p3p2
        foreach (Vector2 vec in all_point_list)
        {
          if (vec.Equals(p1) || vec.Equals(p2) || vec.Equals(p3))
            continue;
          //不在包围盒中
          if (bounds_box.Contains(vec) == false)
            continue;


          //夹角
          float a1 = Math.Abs(LineAngle(p1, vec, p2));

          if (a1 > angle132)
          {
            /////转Step1
            p3 = vec;
            is_loop_sign = true;
            break;
          }
        }

        ///////转Step3
      } while (is_loop_sign);

      //Step3. 若当前网格包围盒内所有网格单元都已被处理完，
      //         也即C（p1，p2，p3）内无可见点，则 p3 为的 p1p2 的 DT 点
      return p3;
    }


    /// <summary>
    /// 判断点vec是否为line的可见点,return true:vec是line的可见点      
    /// </summary>
    /// <param name="vec"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    private bool IsVisiblePointOfLine(Vector2 vec, Line line)
    {
      if (vec.Equals(line.pointA) || vec.Equals(line.pointB))
        return false;

      //（1） p3 在边 p1p2 的右侧 (多边形顶点顺序为顺时针)；
      if (line.ClassifyPoint(vec) == PointClassification.RightSide)
      {
        if (IsVisibleIn2Point(line.pointA, vec)) //（2） p3 与 p1 可见，即 p1p3 不与任何一个约束边相交；
          if (IsVisibleIn2Point(line.pointB, vec)) //（3） p3 与 p2 可见
            return true;
      }

      return false;
    }


    /// <summary>
    /// 点pa和pb是否可见
    /// </summary>
    /// <param name="pa"></param>
    /// <param name="pb"></param>
    /// <returns></returns>
    private bool IsVisibleIn2Point(Vector2 pa, Vector2 pb)
    {
      Line line_papb = new Line(pa, pb);
      return !IsIntersectWithEdges(line_papb);
    }

    /// <summary>
    /// (pa和pb构成的线段不与任何约束边相交，不包括顶点)
    /// </summary>
    /// <param name="line_papb"></param>
    /// <returns></returns>
    private bool IsIntersectWithEdges(Line line_papb)
    {
      Vector2? intersect_point;
      foreach (Line line_tmp in this.edge_list)
      {
        //两线段的关系
        Line.LineClassification line_relation = line_papb.Intersection(line_tmp, out intersect_point);

        if (line_relation == Line.LineClassification.SegmentsIntersect) //相交
        {
          //排除端点相交的情况
          if (intersect_point.Value.EqualsEPSILON(line_papb.pointA) ||
              intersect_point.Value.EqualsEPSILON(line_papb.pointB))
            continue; //两条线段不是真正相交，继续检查下一条约束边
          else
            return true; //两条线段是真正相交
        }

        //不相交，检查下一条约束边   
      }

      return false;
    }


    /// <summary>
    /// 返回三角形的外接圆
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <returns></returns>
    private Circle CircumCircle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
      //解方程
      //p1:(Ax,Ay) p2:(Bx,By) p3(Cx,CY)
      //圆心:(Rx,Ry)
      //(Rx-Ax)^2+(Ry-Ay)^2=(Rx-Bx)^2+(Ry-By)^2
      //(Rx-Ax)^2+(Ry-Ay)^2=(Rx-Cx)^2+(Ry-Cy)^2
      float m1, m2, mx1, mx2, my1, my2;
      float dx, dy, rsqr;
      float xc, yc, r;

      /* Check for coincident points */

      if (Math.Abs(p1.y - p2.y) < FloatConst.Epsilon && Math.Abs(p2.y - p3.y) < FloatConst.Epsilon)
        return null;

      m1 = -(p2.x - p1.x) / (p2.y - p1.y);
      m2 = -(p3.x - p2.x) / (p3.y - p2.y);
      mx1 = (p1.x + p2.x) / 2;
      mx2 = (p2.x + p3.x) / 2;
      my1 = (p1.y + p2.y) / 2;
      my2 = (p2.y + p3.y) / 2;

      if (Math.Abs(p2.y - p1.y) < FloatConst.Epsilon)
      {
        xc = (p2.x + p1.x) / 2;
        yc = m2 * (xc - mx2) + my2;
      }
      else if (Math.Abs(p3.y - p2.y) < FloatConst.Epsilon)
      {
        xc = (p3.x + p2.x) / 2;
        yc = m1 * (xc - mx1) + my1;
      }
      else
      {
        xc = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
        yc = m1 * (xc - mx1) + my1;
      }

      dx = p2.x - xc;
      dy = p2.y - yc;
      rsqr = dx * dx + dy * dy;
      r = (float)Math.Sqrt(rsqr);

      return new Circle(new Vector2(xc, yc), r);
    }

    private Rectangle CircleBounds(Circle circle)
    {
      return new Rectangle(circle.center, new Vector2(circle.radius * 2, circle.radius * 2));
    }

    /// <summary>
    /// 返回顶角在o点，起始边为os，终止边为oe的夹角, 即∠soe (单位：弧度) 
    /// 角度小于pi，返回正值;   角度大于pi，返回负值       
    /// </summary>
    /// <param name="s"></param>
    /// <param name="o"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private float LineAngle(Vector2 s, Vector2 o, Vector2 e)
    {
      float cosfi, fi, norm;
      float dsx = s.x - o.x;
      float dsy = s.y - o.y;
      float dex = e.x - o.x;
      float dey = e.y - o.y;

      cosfi = dsx * dex + dsy * dey;
      norm = (dsx * dsx + dsy * dsy) * (dex * dex + dey * dey);
      cosfi /= ((float)Math.Sqrt(norm));

      if (cosfi >= 1.0)
        return 0;
      if (cosfi <= -1.0)
        return (float)-Math.PI;

      fi = (float)Math.Acos(cosfi);

      if (dsx * dey - dsy * dex > 0)
        return fi; // 说明矢量oe 在矢量 os的逆时针方向 
      return -fi;
    }

    #endregion










  }
}

