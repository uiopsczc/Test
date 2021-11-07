namespace CsCat
{
    public sealed class ByteOrder
    {
        private readonly string name;
        public static readonly ByteOrder BigEndian = new ByteOrder("BIG_ENDIAN");
        public static readonly ByteOrder LittleEndian = new ByteOrder("LITTLE_ENDIAN");

        private ByteOrder(string name)
        {
            this.name = name;
        }


        public override string ToString()
        {
            return name;
        }
    }
}