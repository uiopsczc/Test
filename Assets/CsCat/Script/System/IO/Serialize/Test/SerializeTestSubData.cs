using System.Collections.Generic;

namespace CsCat
{
    public class SerializeTestSubData : PropObserver
    {
        [Serialize] public Dictionary<string, List<int>> nameDictList = new Dictionary<string, List<int>>();
    }
}