using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct StarOrbitSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BlackholeTag>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var blackholeEntity = SystemAPI.GetSingletonEntity<BlackholeTag>();
        var blackholeScale = SystemAPI.GetComponent<LocalTransform>(blackholeEntity).Scale;
        var blackholeRadius = blackholeScale / 2f - 1f;
        new StarOrbitJob
        {
            DeltaTime = SystemAPI.Time.DeltaTime,
            BlackholeRadiusSq = blackholeRadius * blackholeRadius,
            Ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
        }.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct StarOrbitJob : IJobEntity
    {
        public float DeltaTime;
        public float BlackholeRadiusSq;
        public EntityCommandBuffer.ParallelWriter Ecb;

        private void Execute([ChunkIndexInQuery] int chunkIndex, StarOrbitAspect aspect)
        {
            aspect.Orbit(DeltaTime);

            if (aspect.Scale < aspect.IntendedScale)
                aspect.ScaleUp(DeltaTime);

            if (aspect.IsInDestructionRange(float3.zero, BlackholeRadiusSq))
            {
                Ecb.DestroyEntity(chunkIndex, aspect.entity);
            }
        }
    }
}