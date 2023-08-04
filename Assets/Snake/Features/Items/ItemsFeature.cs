using ME.ECS;

namespace Snake.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using Items.Components; using Items.Modules; using Items.Systems; using Items.Markers;
    using Snake.Features.Items;
    using UnityEngine;
    using System.Collections.Generic;

    namespace Items.Components {}
    namespace Items.Modules {}
    namespace Items.Systems {}
    namespace Items.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class ItemsFeature : Feature {

        public ViewId appleViewSourceId { get; private set; }
        public ViewId sneakHeadViewSourceId { get; private set; }
        public ViewId sneakBodyViewSourceId { get; private set; }

        public int SnakeLength => _snakePartsId.Count;
        public int AppleId { get; private set; }

        private List<int> _snakePartsId;

        private FeatureSnakeData _snakeData;
        private ItemsSpawnData _spawnData;

        protected override void OnConstruct()
        {
            AddSystems();
            AddEnities();
        }

        protected override void OnDeconstruct()
        {
            
        }

        private void AddSystems()
        {
            SystemGroup group = new SystemGroup("Items");
            group.AddSystem<AppleSpawnSystem>();
            group.AddSystem<AppleMoveSystem>();

            group.AddSystem<SnakeSpawnSystem>();
            group.AddSystem<SnakeMovementSystem>();
        }

        private void AddEnities()
        {
            _snakeData = Resources.Load<FeatureSnakeData>("FeatureSnakeData");
            _spawnData = Resources.Load<ItemsSpawnData>("ItemsSpawnData");

            sneakHeadViewSourceId = world.RegisterViewSource(_snakeData.snakeHead);
            sneakBodyViewSourceId = world.RegisterViewSource(_snakeData.snakeBody);

            _snakePartsId = new List<int>();

            for (int i = 0; i < _snakeData.startCount; i++)
                AddSnakePart(i);
            
            FeatureAppleData appleData = Resources.Load<FeatureAppleData>("FeatureAppleData");
            appleViewSourceId = world.RegisterViewSource(appleData.viewSource);

            var appleEntity = world.AddEntity();
            appleEntity.Set(new AppleInitializer()
            {
                spawnOffset = _spawnData.spawnOffset
            });

            AppleId = appleEntity.id;
        }

        public void AddSnakePart() => AddSnakePart(_snakePartsId.Count);
        public void AddSnakePart(int index)
        {
            var snakeEntity = world.AddEntity();
            snakeEntity.Set(new SnakeInitializer()
            {
                Index = index,
                spawnOffset = _spawnData.spawnOffset
            });

            if (index == 0) snakeEntity.SetTimer(0, 1f);
            _snakePartsId.Add(snakeEntity.id);
        }

        public void GetSnakePart(int partIndex, ref Entity entity)
        {
            entity = Entity.Empty;
            if (partIndex < _snakePartsId.Count) entity = world.GetEntityById(_snakePartsId[partIndex]);
            else entity = world.GetEntityById(_snakePartsId[_snakePartsId.Count - 1]);
        }
    }

}