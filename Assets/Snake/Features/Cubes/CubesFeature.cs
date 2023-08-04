using ME.ECS;
using Unity.Mathematics;

namespace Snake.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using Cubes.Components; using Cubes.Modules; using Cubes.Systems; using Cubes.Markers;
  

    using UnityEngine;

    namespace Cubes.Components {}
    namespace Cubes.Modules {}
    namespace Cubes.Systems {}
    namespace Cubes.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class CubesFeature : Feature {

        public ViewId viewSourceId { get; private set; }

        protected override void OnConstruct() 
        {
            Debug.Log("Feature Constuct");
            var data = Resources.Load<FeatureCubeData>("FeatureCubeData");
            viewSourceId = world.RegisterViewSource(data.viewSource);

            //AddSystems();
            //Initialize(data);
        }

        private void Initialize(FeatureCubeData data)
        { 
            for (int i = 0; i < data.count; i++)
            {
                var entity = world.AddEntity();

                entity.Set(new CubeInitializer()
                {
                    position = world.GetRandomInSphere(Vector3.zero, 10f),
                    direction = Vector3.back,
                    speed = world.GetRandomRange(1, 2)
                });
            }
        }

        private void AddSystems()
        { 
            SystemGroup group = new SystemGroup("Cubes");
            group.AddSystem<CubeSpawnSystem>();
            group.AddSystem<CubeMovementSystem>();
            //world.AddSystemGroup(ref group);
            Debug.Log("Add Systems");
        }

        protected override void OnDeconstruct() 
        {
            
        }

    }

}