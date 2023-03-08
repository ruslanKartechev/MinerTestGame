namespace Pool
{
    public interface IPool<T>
    {
        int Count { get; set; }
        void Init();
        IPooledObject<T> GetItem();
        void ReturnItem(IPooledObject<T> item);
        void CollectAllBack();
    }
}