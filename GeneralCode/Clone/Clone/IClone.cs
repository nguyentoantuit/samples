namespace Clone
{
    public interface IClone<T>
    {
        T FullClone();
        T ShallowClone();
    }
}
