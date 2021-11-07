using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace CsCat
{
    public partial class DoerAttrParser
    {
        protected Doer u; //主动对象，比如任务中接受任务的玩家
        protected Doer o; //被动对象，比如任务中的任务
        protected Doer e; //中间对象，比如任务中给与任务的npc
        protected Hashtable m; //相互传数据的hashttable

        public DoerAttrParser(Doer u = null, Doer o = null, Doer e = null, Hashtable m = null)
        {
            Set(u, o, e, m);
        }

        public void Set(Doer u = null, Doer o = null, Doer e = null, Hashtable m = null)
        {
            SetU(u);
            SetO(o);
            SetE(e);
            SetM(m);
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


        public object Parse(string eval)
        {
            if (string.IsNullOrEmpty(eval))
                return null;
            if (eval.EqualsIgnoreCase(StringConst.String_true))
                return true;
            if (eval.EqualsIgnoreCase(StringConst.String_false))
                return false;
            if (Regex.IsMatch(eval, "^[+-]?\\d+$")) //整数
            {
                if (eval.TrimLeft(StringConst.String_Plus).TrimLeft(StringConst.String_Minus).Length > 10)
                    return eval.TrimLeft(StringConst.String_Plus).To<long>();
                return eval.TrimLeft(StringConst.String_Plus).To<int>();
            }

            if (Regex.IsMatch(eval, "^[+-]?((\\d+)|(\\d+\\.\\d*)|(\\d*\\.\\d+))([eE]\\d+)?$")) //浮点数
                return eval.TrimLeft(StringConst.String_Plus).To<float>();
            if (eval.StartsWith(StringConst.String_NumberSign)) // 直接字符串
            {
                eval = eval.Substring(StringConst.String_NumberSign.Length);
                if (eval.IndexOf(StringConst.String_LeftCurlyBrackets) != -1)
                    return this.ParseString(eval);
                return eval;
            }

            string ueval;
            if (eval.IndexOf(StringConst.String_LeftCurlyBrackets) != -1)
                ueval = this.ParseString(eval);
            else
                ueval = eval;

            object o = XLuaManager.instance.luaEnv.LoadString(string.Format("return {0}", ueval)).Call()[0];
            if (o == null)
                LogCat.error(
                    string.Format("执行表达式错误[{0}{1}]", eval,
                        StringConst.String_Comma + this.GetU() + StringConst.String_Comma + this.GetO() +
                        StringConst.String_Comma + this.GetE()));
            if (o is Int64)
                o = o.To<int>();
            else if (o is double)
                o = o.To<float>();
            return o;
        }
    }
}