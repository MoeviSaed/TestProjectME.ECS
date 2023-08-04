using ME.ECS;
using ME.ECS.Extensions;

namespace Snake.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Snake.Features.Cubes.Components;
    using Snake.Features;
    using UnityEngine;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class CubeSpawnSystem : ISystemFilter 
    {

        private CubesFeature cubesFeature;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            cubesFeature = world.GetFeature<CubesFeature>();
            Debug.Log("Spawn Construct");
        }
        
        void ISystemBase.OnDeconstruct() {
            Debug.Log("Spawn Deconstruct");
        }
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => true;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {

            return Filter.Create("Filter-CubeSpawnSystem")
                .With<CubeInitializer>()
                .Push();
        }
    

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            var data = entity.Get<CubeInitializer>();

            entity.Set(new IsCube());

            entity.Set(new CubeMovementDirection()
            {
                value = data.direction
            });

            entity.Set(new CubeSpeed()
            {
                value = data.speed
            });

            entity.SetPosition(data.position);
            world.InstantiateView(cubesFeature.viewSourceId, entity);
            Debug.Log("Initialize: " + entity);
            entity.Remove<CubeInitializer>();
        }
    
    }
    
}