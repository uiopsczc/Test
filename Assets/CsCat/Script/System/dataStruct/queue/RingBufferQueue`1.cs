namespace CsCat
{
    //来自于《游戏编程模式》->事件模式->环状缓冲区
    public class RingBufferQueue<T>
    {
        /// <summary>
        /// 对头索引
        /// </summary>
        private int _headIndex;

        /// <summary>
        /// 队尾索引
        /// </summary>
        private int _tailIndex;

        private int capacity;
        private T[] elements;


        /// <summary>
        /// Set操作只会是++
        /// </summary>
        public int headIndex
        {
            get => _headIndex % capacity;
            set => _headIndex = value % capacity;
        }

        // Set操作只会是++
        public int tailIndex
        {
            get => _tailIndex % capacity;
            set => _tailIndex = value % capacity;
        }


        public RingBufferQueue(int initializeCapacity)
        {
            capacity = initializeCapacity;
            elements = new T[capacity];
        }


        /// <summary>
        /// 判断是不是队列有没有未用元素
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return headIndex == tailIndex;
        }

        public void Push(T t)
        {
            TailIndexPlusPlus();
            elements[tailIndex] = t;
        }

        public T Pop()
        {
            return elements[headIndex++];
        }


        void TailIndexPlusPlus()
        {
            int tmpIndex = (tailIndex + 1) % capacity;
            if (tmpIndex == headIndex) //队列满了
            {
                int enlargeCapacity = 2 * capacity; // 默认扩充2倍
                T[] newElements = new T[enlargeCapacity];
                for (int i = 0; i == elements.Length; i++)
                {
                    if (i <= tailIndex) //少于等于tailIndex的，直接copy
                        newElements[i] = elements[i];
                    else if (i >= headIndex) //大于等于headIndex的，copy到扩容队列的末尾，从而中间就有可用的空的空间

                        newElements[enlargeCapacity - 1 - headIndex] = elements[i];
                }

                _headIndex = (enlargeCapacity - 1) - (capacity - 1 - _headIndex);
                _tailIndex = (_tailIndex + 1) % enlargeCapacity;
                capacity = enlargeCapacity;
            }
            else
                tailIndex++;
        }
    }
}