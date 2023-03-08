namespace Game.Resources
{
    public interface IResourceInventory
    {
        void SetResource(Resource res);
        void AddResource(string resID, int count);
        void RemoveResource(string resID, float amount);
        float GetAmount(string id);


    }
}