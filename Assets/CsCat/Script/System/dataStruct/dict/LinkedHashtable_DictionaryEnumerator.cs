using System.Collections;

namespace CsCat
{
    public partial class LinkedHashtable
    {
        class DictionaryEnumerator : IDictionaryEnumerator
        {

            ArrayList keyList;
            ArrayList valueList;
            int position = -1;
            private DictionaryEntry current;
            private DictionaryEntry entry;


            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    object key = keyList[position];
                    object value = valueList[position];
                    entry.Key = key;
                    entry.Value = value;
                    return entry;
                }
            }

            object IDictionaryEnumerator.Key => keyList[position];

            object IDictionaryEnumerator.Value => valueList[position];

            object IEnumerator.Current
            {
                get
                {
                    object key = keyList[position];
                    object value = valueList[position];
                    current.Key = key;
                    current.Value = value;
                    return current;
                }
            }


            public DictionaryEnumerator(ArrayList keyList, ArrayList valueList)
            {
                Init(keyList, valueList);
            }

            public void Init(ArrayList keyList, ArrayList valueList)
            {
                this.keyList = keyList;
                this.valueList = valueList;
            }

            public void Reset()
            {
                position = -1;
            }


            bool IEnumerator.MoveNext()
            {
                position++;
                return position < keyList.Count;
            }

            void IEnumerator.Reset()
            {
                Reset();
            }

        }
    }
}