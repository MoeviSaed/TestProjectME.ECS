using ME.ECS;

namespace Snake.Features.Items.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Snake.Features.Cells;
    using UnityEngine;
    using Snake.Features.Cells.Components;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class AppleMoveSystem : ISystemFilter {

        private CellFeature _cellFeature;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {

            _cellFeature = world.GetFeature<CellFeature>();
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-AppleMoveSystem")
                .With<IsApple>()
                .With<ItemToMove>()
                .Push();

        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var spawn = ref entity.Get<ItemToMove>();
            Entity cell = Entity.Empty;

            if (TryGetCell(ref cell))
            {
                spawn.moveCellId = cell.id;

                ref IsItem entityItem = ref entity.Get<IsItem>();
                world.GetEntityById(entityItem.currentEntityId).Get<IsCell>().IsFree = true;
                entityItem.currentEntityId = cell.id;
                entity.SetPosition((Vector3)cell.GetPosition() + entityItem.spawnOffset);

                entity.Remove<ItemToMove>();
            }
        }

        private bool TryGetCell(ref Entity entity)
        {
            Entity cell = Entity.Empty;

            if (_cellFeature.TryGetFreeCell(ref entity))
            {
                ref var isCell = ref entity.Get<IsCell>();
                isCell.IsFree = false;
                return true;
            }
            return false;
        }
    
    }
    
}