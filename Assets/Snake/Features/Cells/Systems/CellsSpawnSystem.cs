using ME.ECS;

namespace Snake.Features.Cells.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class CellsSpawnSystem : ISystemFilter {
        
        private CellFeature feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {

            this.GetFeature(out this.feature);
            
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {

            return Filter.Create("Filter-CellsSpawnSystem")
                .With<CellsInitializer>()
                .Push();
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var data = entity.Get<CellsInitializer>();

            entity.Set(new IsCell()
            {
                index = data.index,
                IsFree = true,
                adjacentCells = data.adjacentCells
            });

            entity.Set(new CellContainer());
            entity.Set(new SceneCellInitialize());

            entity.SetPosition(data.position);
            world.InstantiateView(feature.viewSourceId, entity);
            entity.Remove<CellsInitializer>();
        }
    }
    
}