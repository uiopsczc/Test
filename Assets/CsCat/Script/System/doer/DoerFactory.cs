// 逻辑对象

using System;
using System.Collections.Generic;

namespace CsCat
{
    public class DoerFactory : TickObject
    {
        private Dictionary<string, Dictionary<string, DBase>> idDict =
            new Dictionary<string, Dictionary<string, DBase>>();

        protected virtual string defaultDoerClassPath => null;

        protected virtual string GetClassPath(string id)
        {
            return defaultDoerClassPath;
        }

        protected virtual DBase _NewDBase(string idOrRid)
        {
            return new DBase(idOrRid);
        }

        protected virtual Doer _NewDoer(string id)
        {
            string classPath = GetClassPath(id);
            Type type = TypeUtil.GetType(classPath);
            var doer = this.AddChildWithoutInit(null, type) as Doer;
            return doer;
        }

        // 获得所有逻辑对象数量
        public int GetDoerCount(string id = null)
        {
            int count = 0;
            if (id == null)
            {
                foreach (var value in this.idDict.Values)
                {
                    count = count + value.Count;
                }
            }
            else
            {
                if (this.idDict.ContainsKey(id))
                    count = this.idDict[id].Count;
            }

            return count;
        }

        //获得所有逻辑对象
        public List<Doer> GetAllDoers(string id = null)
        {
            List<Doer> result = new List<Doer>();
            if (id == null)
            {
                foreach (var value in this.idDict.Values)
                foreach (var dbase in value.Values)
                    result.Add(dbase.GetDoer());
            }
            else
            {
                if (this.idDict.ContainsKey(id))
                {
                    foreach (var dbase in this.idDict[id].Values)
                        result.Add(dbase.GetDoer());
                }
            }

            return result;
        }

        public Doer FindDoers(string idOrRid)
        {
            string id = IdUtil.RidToId(idOrRid);
            bool isId = IdUtil.IsId(idOrRid);
            if (idDict.TryGetValue(id, out var dict))
            {
                if (dict.Count != 0)
                {
                    foreach (var rid in this.idDict[id].Keys)
                    {
                        var dbase = idDict[id][rid];
                        if (isId)
                            return dbase.GetDoer();
                        if (rid.Equals(idOrRid))
                            return dbase.GetDoer();
                    }
                }
            }

            return null;
        }

        // 创建逻辑对象
        public Doer NewDoer(string idOrRid)
        {
            string id = IdUtil.RidToId(idOrRid);
            var doer = this._NewDoer(id);
            var dbase = this._NewDBase(idOrRid);
            doer.SetDBase(dbase);
            dbase.SetDoer(doer);
            doer.factory = this;
            string rid = dbase.GetRid();
            var dbaseDict = this.idDict.GetOrAddDefault(id, () => new Dictionary<string, DBase>());
            dbaseDict[rid] = dbase;
            doer.Init();
            doer.PostInit();
            doer.SetIsEnabled(true, false);
            return doer;
        }

        // 释放逻辑对象
        public void ReleaseDoer(Doer doer)
        {
            string id = doer.GetId();
            string rid = doer.GetRid();
            if (this.idDict.ContainsKey(id))
                this.idDict[id].Remove(rid);
            doer.DoRelease();
            this.RemoveChild(doer.key);
        }
    }
}