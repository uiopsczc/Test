namespace CsCat
{
    public interface ICopyable<T>
    {
        void CopyTo<T>(T dest);

        void CopyFrom<T>(T source);
    }
}