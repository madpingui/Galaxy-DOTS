using Unity.Entities;
using Unity.Mathematics;

public struct StarProperties : IComponentData
{
    public float SystemRadius;
    public Entity StarPrefab;
    public float StarSpawnRate;

    public float2 ForwardSpeedRange;
    public float2 OrbitalSpeedRange;
    public float2 IntendedScaleRange;
}

public struct StarRandom : IComponentData
{
    public Random Value;
}

public struct StarsSpawnTime : IComponentData
{
    public float Value;
}

public struct StarOrbitProperties : IComponentData
{
    public float ForwardSpeed;
    public float OrbitalSpeed;
    public float IntendedScale;
}

public struct BlackholeTag : IComponentData
{
}
