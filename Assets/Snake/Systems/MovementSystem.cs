using ME.ECS;

namespace Snake.Systems
{

    #pragma warning disable
    using Snake.Components;
    using Snake.Modules;
    using Snake.Systems;
    using Snake.Markers;
    using Components;
    using Modules;
    using Systems;
    using Markers;
    using UnityEngine;
    using Snake.Features.Cubes.Components;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif

    public class MovementSystem : ISystem, IAdvanceTick, ISystemFilter
    {
        public World world { get; set; }

        public bool jobs => true;

        public int jobsBatchCount => 64;

        public Filter filter { get; set; }

        void ISystemBase.OnConstruct() 
        {
            Debug.Log("MovementSystem Construct");
        }

        void ISystemBase.OnDeconstruct() 
        {
            Debug.Log("MovementSystem Deconstruct");
        }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            Debug.Log("MovementSystem Tick: " + deltaTime);
        }

        public Filter CreateFilter()
        {
            Debug.Log("MovementSystem Filter2 ");

            return Filter.Create("Filter-CubeMovementSystem")
                .With<IsCube>()
                .Push();
        }

        public void AdvanceTick(in Entity entity, in float deltaTime)
        {
            Debug.Log("MovementSystem Tick2: " + deltaTime);
        }
    }

}