using System.Collections.Generic;

namespace CsCat
{
    /// <summary>
    ///   用于保存信息
    /// </summary>
    public class ObjectInfos
    {
        private readonly List<object> _list = new List<object>();

        //////////////////////////////////////////////////////////////////////
        // Get
        //////////////////////////////////////////////////////////////////////
        public List<object> GetList() => this._list;

        public ObjectInfos(params object[] args)
        {
            if (args == null) return;
            foreach (var arg in args)
                _list.Add(arg);
        }

        public override bool Equals(object obj)
        {
            var other = (ObjectInfos) obj;
            if (other == null)
                return false;
            var otherList = other.GetList();

            if (_list.Count != otherList.Count)
                return false;
            for (var i = _list.Count - 1; i >= 0; i--)
                if (!ObjectUtil.Equals(_list[i], otherList[i]))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return _list == null ? ObjectUtil.GetHashCode(_list) : ObjectUtil.GetHashCode(_list.ToArray());
        }
    }
}