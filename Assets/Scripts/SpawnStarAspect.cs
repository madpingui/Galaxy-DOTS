using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct SpawnStarAspect : IAspect
{
    private readonly RefRO<LocalTransform> transform;

    private readonly RefRO<StarProperties> starPorperties;
    private readonly RefRW<StarRandom> starRandom;
    private readonly RefRW<StarsSpawnTime> starsSpawnTime;

    public Entity StarPrefab => starPorperties.ValueRO.StarPrefab;

    public LocalTransform GetRandomStarTransform()
    {
        return new LocalTransform
        {
            Position = GetRandomPosition(),
            Rotation = quaternion.identity,
            Scale = 0 //Start invisible and scale it up so it doesnt pop
        };
    }

    private float3 GetRandomPosition()
    {
        float3 randomPosition;
        do
        {
            randomPosition = GetRandomPointInCappedSphere(starPorperties.ValueRO.SystemRadius * 0.5f);
        } while (math.distancesq(transform.ValueRO.Position, randomPosition) <= BLACKHOLE_SAFETY_RADIUS_SQ);

        return randomPosition;
    }

    private float3 GetRandomPointInCappedSphere(float radius)
    {
        // Generate a random direction for the x and z axes
        float2 horizontalDirection = math.normalize(starRandom.ValueRW.Value.NextFloat2Direction());

        // Generate a random distance for the x and z axes
        float horizontalDistance = starRandom.ValueRW.Value.NextFloat(0f, radius);

        // Generate the x and z positions based on the horizontal direction and distance
        float x = horizontalDirection.x * horizontalDistance;
        float z = horizontalDirection.y * horizontalDistance;

        // Restrict the y position
        float y = starRandom.ValueRW.Value.NextFloat(-10f, 10f);

        // Return the point with the restricted height
        return transform.ValueRO.Position + new float3(x, y, z);
    }

    private const float BLACKHOLE_SAFETY_RADIUS_SQ = 300f;

    public float GetStarSpawnRate() => starPorperties.ValueRO.StarSpawnRate;

    public float GetRandomForward => starRandom.ValueRW.Value.NextFloat(starPorperties.ValueRO.ForwardSpeedRange.x, starPorperties.ValueRO.ForwardSpeedRange.y);
    public float GetRandomOrbital => starRandom.ValueRW.Value.NextFloat(starPorperties.ValueRO.OrbitalSpeedRange.x, starPorperties.ValueRO.OrbitalSpeedRange.y);
    public float GetRandomScale => starRandom.ValueRW.Value.NextFloat(starPorperties.ValueRO.IntendedScaleRange.x, starPorperties.ValueRO.IntendedScaleRange.y);

    public float StarSpawnTimer
    {
        get => starsSpawnTime.ValueRO.Value;
        set => starsSpawnTime.ValueRW.Value = value;
    }
}
