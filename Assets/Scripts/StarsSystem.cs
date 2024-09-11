using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class StarsSystem : MonoBehaviour
{
    public float SystemRadius;
    public GameObject StarPrefab;
    public float StarSpawnRate;
    public uint seed;

    public float2 ForwardSpeedRange = new float2(0.1f, 1f);  // Min and max values for forward speed
    public float2 OrbitalSpeedRange = new float2(3f, 15f);  // Min and max values for orbital speed
    public float2 IntendedScaleRange = new float2(0.1f, 0.5f);  // Min and max values for scale

    public class StarsSystemBaker : Baker<StarsSystem>
    {
        public override void Bake(StarsSystem authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new StarProperties
            {
                SystemRadius = authoring.SystemRadius,
                StarPrefab = GetEntity(authoring.StarPrefab, TransformUsageFlags.Dynamic),
                StarSpawnRate = authoring.StarSpawnRate,

                ForwardSpeedRange = authoring.ForwardSpeedRange,
                OrbitalSpeedRange = authoring.OrbitalSpeedRange,
                IntendedScaleRange = authoring.IntendedScaleRange

            });
            AddComponent(entity, new StarRandom
            {
                Value = Unity.Mathematics.Random.CreateFromIndex(authoring.seed)
            });
            AddComponent<StarsSpawnTime>(entity);
        }
    }
}