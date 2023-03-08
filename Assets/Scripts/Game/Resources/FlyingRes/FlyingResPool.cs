using Pool;
using UnityEngine;

namespace Game.Resources
{
    [CreateAssetMenu(fileName = nameof(FlyingResPool), menuName = "SO/Pool/" + nameof(FlyingResPool))]
    public class FlyingResPool : BasicPoolSO<FlyingRes>
    {
           
    }
}