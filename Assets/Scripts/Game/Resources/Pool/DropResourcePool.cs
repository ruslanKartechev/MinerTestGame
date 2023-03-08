using Pool;
using UnityEngine;

namespace Game.Resources
{
    [CreateAssetMenu(fileName = nameof(DropResourcePool), menuName = "SO/Resources/" + nameof(DropResourcePool))]
    public class DropResourcePool : BasicPoolSO<ResourceView>
    {
    }
}