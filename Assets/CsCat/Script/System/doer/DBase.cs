using System.Collections;

namespace CsCat
{
  // 数据对象
  public class DBase
  {
    private string rid;
    private string id;
    public Hashtable db;
    public Hashtable db_tmp;
    private Doer doer;

    public DBase(string id_or_rid, Hashtable db = null, Hashtable db_tmp = null)
    {
      if (IdUtil.IsRid(id_or_rid))
      {
        string rid = id_or_rid;
        this.rid = rid;
        this.id = IdUtil.RidToId(rid);
      }
      else
      {
        this.id = id_or_rid;
        this.rid = IdUtil.NewRid(this.id);
      }

      this.db = db ?? new Hashtable();
      this.db_tmp = db_tmp ?? new Hashtable();
    }

    //////////////////////////////////GetXXX////////////////////////////////////
    public string GetId()
    {
      return this.id;
    }

    public string GetRid()
    {
      return this.rid;
    }

    public string GetRidSeq()
    {
      return IdUtil.RidToSeq(this.rid);
    }

    public Doer GetDoer()
    {
      return this.doer;
    }

    public T GetDoer<T>() where T : Doer
    {
      return GetDoer() as T;
    }

    //////////////////////////////////SetXXX////////////////////////////////////
    public void SetDoer(Doer doer)
    {
      this.doer = doer;
    }
  }
}