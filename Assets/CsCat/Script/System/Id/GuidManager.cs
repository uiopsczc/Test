namespace CsCat
{
    public class GuidManager
    {
        public ulong keyNumber;

        public GuidManager(ulong currentKeyNumber)
        {
            this.keyNumber = currentKeyNumber;
        }

        public GuidManager()
        {
        }

        public string NewGuid(string id = null)
        {
            keyNumber++;
            return (id.IsNullOrWhiteSpace() ? StringConst.String_Empty : id) + IdConst.Rid_Infix + keyNumber;
        }
    }
}