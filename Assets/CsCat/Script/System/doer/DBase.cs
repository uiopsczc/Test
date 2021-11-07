using System.Collections;

namespace CsCat
{
    // 数据对象
    public class DBase
    {
        private string rid;
        private string id;
        public Hashtable db;
        public Hashtable dbTmp;
        private Doer doer;

        public DBase(string idOrRid, Hashtable db = null, Hashtable dbTmp = null)
        {
            if (IdUtil.IsRid(idOrRid))
            {
                string rid = idOrRid;
                this.rid = rid;
                this.id = IdUtil.RidToId(rid);
            }
            else
            {
                this.id = idOrRid;
                this.rid = IdUtil.NewRid(this.id);
            }

            this.db = db ?? new Hashtable();
            this.dbTmp = dbTmp ?? new Hashtable();
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