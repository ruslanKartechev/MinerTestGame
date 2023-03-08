namespace Game.Spots
{
    public abstract partial class Spot
    {
        public enum ESpotPhase
        {
            Idle = 0,
            Input = 1,
            Production = 2,
            Output = 3
        }
    }
}