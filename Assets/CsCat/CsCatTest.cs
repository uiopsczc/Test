using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
    public class CsCatTest : MonoBehaviour
    {
        void Start()
        {
            List<char> list = new List<char>();
            list.Add('a');
            list.Add('b');
            list.Add('e');
            list.Add('e');
            list.Add('e');
            list.Add('f');

//      list.Reverse();

            int index = list.BinarySearchCat('e', IndexOccurType.Last_Index, (a, b) => a - b);
            LogCat.log(list);
            LogCat.log(index);
        }
    }
}