using ME.ECS;

namespace Snake.Features.Items.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Snake.Features.Cells.Components;
    using Snake.Features.Cells;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class AppleSpawnSystem : ISystemFilter
    {
        private ItemsFeature _itemsFeature;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {

            world.GetFeature(out _itemsFeature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-AppleSpawnSystem")
                .With<AppleInitializer>()
                .Push();
            
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            var data = entity.Get<AppleInitializer>();

            entity.Set(new IsApple());

            entity.Set(new ItemToMove()
            {
            });

            entity.Set(new IsItem()
            {
                spawnOffset = data.spawnOffset
            });

            world.InstantiateView(_itemsFeature.appleViewSourceId, entity);
            entity.Remove<AppleInitializer>();
        }

        private void GetSpawnIndex()
        {

        }
    }
}