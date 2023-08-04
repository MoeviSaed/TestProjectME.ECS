using ME.ECS;

namespace Snake.Features.Items.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    using Snake.Features.Cells;
    using Snake.Features.Cells.Components;
    using UnityEngine;
    using UnityEngine.SceneManagement;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class SnakeMovementSystem : ISystemFilter {

        private InputModule _inputModule;
        private ItemsFeature _itemsFeature;
        private CellFeature _cellFeature;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            _inputModule = world.GetModule<InputModule>();
            _itemsFeature = world.GetFeature<ItemsFeature>();
            _cellFeature = world.GetFeature<CellFeature>();
        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {

            return Filter.Create("Filter-SnakeSetMoveSystem")
                .With<IsSnake>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            IsSnake snake = entity.Get<IsSnake>();
            if (!snake.IsHead) return;

            if (entity.ReadTimer(0) <= 0)
            {
                entity.SetTimer(0, 0.5f);
                Move(entity);
            }
            else return;
        }

        private void Move(Entity head)
        {
            int length = _itemsFeature.SnakeLength;

            Entity headOldCellEntity = Entity.Empty;
            Entity headNextCellEntity = Entity.Empty;

            Entity nextCellEntity = Entity.Empty;
            Entity oldCellEntity = Entity.Empty;

            Entity snakePartEntity = Entity.Empty;
            Entity previousSnakePartEntity = Entity.Empty;

            if (!GetCellAndLookByInput(head, ref headOldCellEntity, ref headNextCellEntity, out Look look))
            {
                if (!CheckApple(ref headNextCellEntity))
                {
                    GameOver();
                    return;
                }
            }

            for (int i = length - 1; i >= 0; i--)
            {
                _itemsFeature.GetSnakePart(i, ref snakePartEntity);

                if (snakePartEntity.Get<IsSnake>().IsHead) continue;

                _itemsFeature.GetSnakePart(i - 1 < 0 ? 0 : i - 1, ref previousSnakePartEntity);

                oldCellEntity = world.GetEntityById(snakePartEntity.Get<IsItem>().currentEntityId);
                nextCellEntity = world.GetEntityById(previousSnakePartEntity.Get<IsItem>().currentEntityId);

                snakePartEntity.Get<IsItem>().look = previousSnakePartEntity.Get<IsItem>().look;
                SetItemToMove(ref snakePartEntity, ref oldCellEntity, ref nextCellEntity, i == length - 1);
                SetRotation(ref snakePartEntity);
            }

            head.Get<IsItem>().look = look;
            SetItemToMove(ref head, ref headOldCellEntity, ref headNextCellEntity, false);
            SetRotation(ref head);

            void SetItemToMove(ref Entity snakePart, ref Entity oldCell, ref Entity nextCell, bool freeOldCell)
            {
                ref var isCell = ref oldCell.Get<IsCell>();
                if (freeOldCell) isCell.IsFree = true;

                isCell = ref nextCell.Get<IsCell>();
                isCell.IsFree = false;

                ref IsItem isItem = ref snakePart.Get<IsItem>();
                isItem.currentEntityId = nextCell.id;

                snakePart.SetPosition((Vector3)nextCell.GetPosition() + isItem.spawnOffset);
            }

            void SetRotation(ref Entity snakeEntity)
            {
                Vector3 rotation = snakeEntity.GetRotation().ToEuler();
                rotation.z = IsItem.GetZAxisByLook(snakeEntity.Get<IsItem>().look);
                snakeEntity.SetRotation(Quaternion.Euler(rotation));
            }
        }

        private bool CheckApple(ref Entity nextHeadCell)
        {
            Entity apple = world.GetEntityById(_itemsFeature.AppleId);
            if (apple.Get<IsItem>().currentEntityId != nextHeadCell.id) return false;

            apple.Set<ItemToMove>();
            _itemsFeature.AddSnakePart();
            return true;
        }

        private bool GetCellAndLookByInput(Entity snakePartEntity, ref Entity oldCellEntity, ref Entity nextCellEntity, out Look look)
        {
            oldCellEntity = world.GetEntityById(snakePartEntity.Get<IsItem>().currentEntityId);

            Look currentLook = snakePartEntity.Get<IsItem>().look;
            look = currentLook;

            if (currentLook == Look.up || currentLook == Look.down)
            {
                if (_inputModule.Horizontal > 0) look = Look.right;
                else if (_inputModule.Horizontal < 0) look = Look.left;
                else look = currentLook;
            }
            else if (currentLook == Look.right || currentLook == Look.left)
            {
                if (_inputModule.Vertical > 0) look = Look.up;
                else if (_inputModule.Vertical < 0) look = Look.down;
                else look = currentLook;
            }

            switch(look)
            {
                case Look.right: nextCellEntity = world.GetEntityById(oldCellEntity.Get<IsCell>().adjacentCells.right); break;
                case Look.down: nextCellEntity = world.GetEntityById(oldCellEntity.Get<IsCell>().adjacentCells.down); break;
                case Look.left: nextCellEntity = world.GetEntityById(oldCellEntity.Get<IsCell>().adjacentCells.left); break;
                case Look.up: nextCellEntity = world.GetEntityById(oldCellEntity.Get<IsCell>().adjacentCells.up); break;
            }

            Debug.Log(nextCellEntity.Get<IsCell>().IsFree);

            return nextCellEntity.Get<IsCell>().IsFree;
        }

        private void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("GameOver");
        }
    }
}