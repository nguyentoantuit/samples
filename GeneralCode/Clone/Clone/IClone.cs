namespace Clone
{
    interface IClone<T>
    {
        T FullClone();
        T ShallowClone();
    }
}
