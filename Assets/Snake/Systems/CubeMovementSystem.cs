using ME.ECS;
using UnityEngine;

namespace Snake.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Snake.Features.Cubes.Components;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class CubeMovementSystem : ISystemFilter {
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            Debug.Log("Move Construct");
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => true;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {

            return Filter.Create("Filter-CubeMovementSystem")
                .With<IsCube>()
                .Push();
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            Debug.Log("Move tick");
            Vector3 pos = entity.GetPosition();
            pos += entity.Get<CubeMovementDirection>().value * (entity.Get<CubeSpeed>().value * deltaTime);
            entity.SetPosition(pos);
        }
    
    }
    
}