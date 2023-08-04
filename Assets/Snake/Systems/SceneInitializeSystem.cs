using ME.ECS;

namespace Snake.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Snake.Features.Cells.Components;
    using UnityEngine;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SceneInitializeSystem : ISystemFilter
    {
        private SceneParameters _sceneParameters;
           
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            _sceneParameters = Resources.Load<SceneParameters>("SceneParameters");
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-SceneInitializeSystem")
                .With<SceneCellInitialize>()
                .Push();
            
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
             
        }
    }
}