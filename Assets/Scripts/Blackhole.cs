using Unity.Entities;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public class BlackholeBaker : Baker<Blackhole>
    {
        public override void Bake(Blackhole authoring)
        {
            var blackholeEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<BlackholeTag>(blackholeEntity);
        }
    }
}
