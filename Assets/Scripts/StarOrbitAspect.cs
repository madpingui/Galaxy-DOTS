using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct StarOrbitAspect : IAspect
{
    public readonly Entity entity;
    private readonly RefRW<LocalTransform> transform;
    private readonly RefRO<StarOrbitProperties> starOrbitProperties;

    // Cache properties for better performance
    private float ForwardSpeed => starOrbitProperties.ValueRO.ForwardSpeed;
    private float OrbitalSpeed => starOrbitProperties.ValueRO.OrbitalSpeed;
    public float Scale => transform.ValueRO.Scale;
    public float IntendedScale => starOrbitProperties.ValueRO.IntendedScale;

    public void Orbit(float deltaTime)
    {
        float3 currentPosition = transform.ValueRO.Position;
        float3 direction = math.normalize(-currentPosition); // Simplify direction calculation
        float3 right = math.normalize(math.cross(direction, math.up()));

        float3 movement = direction * ForwardSpeed + right * OrbitalSpeed;
        transform.ValueRW.Position += movement * deltaTime;
    }

    public void ScaleUp(float deltaTime)
    {
        var scaleSpeed = IntendedScale / 5f; // Calculate the scale speed (units per second)
        var newScale = math.min(Scale + scaleSpeed * deltaTime, IntendedScale); // Calculate the new scale
        
        transform.ValueRW.Scale = newScale; // Update the scale
    }

    public bool IsInDestructionRange(float3 blackholePos, float blackholeRadiusSq)
    {
        return math.distancesq(blackholePos, transform.ValueRO.Position) <= blackholeRadiusSq;
    }
}
