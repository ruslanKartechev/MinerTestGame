using React;

namespace Game.Resources
{
    [System.Serializable]
    public class Resource
    {
        public Resource() {}
        public Resource(string id, float amount)
        {
            ID = id;
            Amount = new ReactiveProperty<float>();
            Amount.Value = amount;
        }

        public string ID;
        public ReactiveProperty<float> Amount;
        
    }
}