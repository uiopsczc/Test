namespace CsCat
{
    public interface ICopyable
    {
        void CopyTo(object dest);

        void CopyFrom(object source);
    }
}