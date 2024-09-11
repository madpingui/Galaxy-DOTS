using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct SpawnStarSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<StarProperties>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

        new SpawnStarJob
        {
            ecb = ecb,
            ElapsedTime = SystemAPI.Time.ElapsedTime
        }.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct SpawnStarJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ecb;
        public double ElapsedTime;

        private void Execute([ChunkIndexInQuery] int chunkIndex, SpawnStarAspect aspect)
        {
            // If the next spawn time has passed.
            if (aspect.StarSpawnTimer < ElapsedTime)
            {
                var newStar = ecb.Instantiate(chunkIndex, aspect.StarPrefab);
                var newStarTransform = aspect.GetRandomStarTransform();
                ecb.SetComponent(chunkIndex, newStar, newStarTransform);

                ecb.SetComponent(chunkIndex, newStar, new StarOrbitProperties
                {
                    OrbitalSpeed = aspect.GetRandomOrbital,
                    ForwardSpeed = aspect.GetRandomForward,
                    IntendedScale = aspect.GetRandomScale
                });

                // Resets the next spawn time.
                aspect.StarSpawnTimer = (float)ElapsedTime + aspect.GetStarSpawnRate();
            }
        }
    }
}
