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
    public sealed class SnakeSpawnSystem : ISystemFilter {
        
        private ItemsFeature _itemsFeature;
        private CellFeature _cellFeature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this._itemsFeature);
            this.GetFeature(out this._cellFeature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-SnakeSpawnSystem")
                .With<SnakeInitializer>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var data = entity.Get<SnakeInitializer>();
            Entity cellEntity = Entity.Empty;

            if (data.Index == 0)
            {
                if (!TrySpawnHead(in entity, ref cellEntity)) return;
            }
            else SpawnBody(data.Index, in entity, ref cellEntity);

            entity.Set(new IsSnake()
            {
                Index = data.Index
            });

            entity.Set(new IsItem()
            {
                currentEntityId = cellEntity.id,
                spawnOffset = data.spawnOffset,
                look = Look.left
            });

            Debug.Log("CellId: " + cellEntity.id);

            entity.SetPosition((Vector3)entity.GetPosition() + data.spawnOffset);

            entity.Remove<SnakeInitializer>();
        }

        private bool TrySpawnHead(in Entity entity, ref Entity cellEntity)
        {
            if (!TryGetPosition(ref cellEntity)) return false;

            world.InstantiateView(_itemsFeature.sneakHeadViewSourceId, entity);
            entity.SetPosition(cellEntity.GetPosition());
            return true;
        }

        private void SpawnBody(int newBodyIndex, in Entity entity, ref Entity cellEntity)
        {
            Entity previousPart = Entity.Empty;
            _itemsFeature.GetSnakePart(newBodyIndex - 1, ref previousPart);
            GetNearPosition(previousPart.Get<IsItem>().currentEntityId, ref cellEntity);
            world.InstantiateView(_itemsFeature.sneakBodyViewSourceId, entity);
            entity.SetPosition(cellEntity.GetPosition());
        }

        private bool TryGetPosition(ref Entity cellEntity)
        {
            Entity cell = Entity.Empty;

            if (_cellFeature.TryGetFreeCell(ref cell))
            {
                ref var isCell = ref cell.Get<IsCell>();
                isCell.IsFree = false;
                cellEntity = cell;
                return true;
            }
            return false;
        }

        private void GetNearPosition(int cellId, ref Entity cellEntity)
        {
            Entity cell = Entity.Empty;
            bool value = _cellFeature.TryGetAdjacentCell(cellId, ref cell);
            ref var isCell = ref cell.Get<IsCell>();
            isCell.IsFree = false;
            cellEntity = cell;

            //Debug.Log(value);

           // Debug.Log("Current: " + isCell.IsFree);
           // Debug.Log("Original: " + world.GetEntityById(cellEntity.id).Get<IsCell>().IsFree);
        }
    }
}