using System.Collections;

namespace CsCat
{
    public partial class DoerAttrSetter
    {
        protected string desc;
        protected Doer u; //主动对象，比如任务中接受任务的玩家
        protected Doer o; //被动对象，比如任务中的任务
        protected Doer e; //中间对象，比如任务中给与任务的npc
        protected Hashtable m; //相互传数据的hashttable
        protected DoerAttrParser doerAttrParser;

        public DoerAttrSetter(string desc = null, DoerAttrParser doerAttrParser = null)
        {
            SetDesc(desc);
            if (doerAttrParser != null)
            {
                SetU(doerAttrParser.GetU());
                SetO(doerAttrParser.GetO());
                SetE(doerAttrParser.GetE());
                SetM(doerAttrParser.GetM());
                SetDoerAttrParser(doerAttrParser);
            }
        }

        public DoerAttrParser GetDoerAttrParser()
        {
            return doerAttrParser;
        }

        public void SetDoerAttrParser(DoerAttrParser doerAttrParser)
        {
            this.doerAttrParser = doerAttrParser;
        }

        public string GetDesc()
        {
            return desc;
        }

        public void SetDesc(string desc)
        {
            this.desc = desc;
        }

        public Doer GetU()
        {
            return u;
        }

        public void SetU(Doer u)
        {
            this.u = u;
        }

        public Doer GetO()
        {
            return o;
        }

        public void SetO(Doer o)
        {
            this.o = o;
        }

        public Doer GetE()
        {
            return e;
        }

        public void SetE(Doer e)
        {
            this.e = e;
        }

        public Hashtable GetM()
        {
            return m;
        }

        public void SetM(Hashtable m)
        {
            this.m = m;
        }
    }
}