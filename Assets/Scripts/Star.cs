using Unity.Entities;
using UnityEngine;

public class Star : MonoBehaviour
{
    public class StarBaker : Baker<Star>
    {
        public override void Bake(Star authoring)
        {
            var starEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<StarOrbitProperties>(starEntity);
        }
    }
}
