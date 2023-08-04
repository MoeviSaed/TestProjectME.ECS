using ME.ECS;

namespace Snake.Features.Cells {

    using Components; using Modules; using Systems; using Features; using Markers;
    using CellsFeature.Components; using CellsFeature.Modules; using CellsFeature.Systems; using CellsFeature.Markers;
    using System;
    using UnityEngine;

    namespace CellsFeature.Components {}
    namespace CellsFeature.Modules {}
    namespace CellsFeature.Systems {}
    namespace CellsFeature.Markers {}

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class CellFeature : Feature {

        public ViewId viewSourceId { get; private set; }
        public Vector2Int borders {get; private set;}

        private int[] _cellsId;
        private int[][] _cellsIdByIndex;

        protected override void OnConstruct()
        {
            FeatureCellsData data = Resources.Load<FeatureCellsData>("FeatureCellsData");
            viewSourceId = world.RegisterViewSource(data.viewSource);

            borders = data.Count;

            AddSystems();
            AddEnities(data);
        }

        private void AddSystems()
        {
            SystemGroup group = new SystemGroup("Cells");
            group.AddSystem<CellsSpawnSystem>();
        }

        private void AddEnities(FeatureCellsData data)
        {
            int count = data.Count.x * data.Count.y;
            _cellsId = new int[count];

            _cellsIdByIndex = new int[data.Count.x][];
                

            for (int i = 0; i < count; i++)
            {
                var entity = world.AddEntity();

                GetDataForCell(data, i, out Vector2 position, out Vector2Int index);

                entity.Set(new CellsInitializer()
                {
                    position = position,
                    index = index
                });

                _cellsId[i] = entity.id;

                if (_cellsIdByIndex[index.x] == null) _cellsIdByIndex[index.x] = new int[data.Count.y];
                _cellsIdByIndex[index.x][index.y] = entity.id;
            }

            for (int i = 0; i < _cellsId.Length; i++)
            {
                
                Entity entity = world.GetEntityById(_cellsId[i]);
                ref CellsInitializer cell = ref entity.Get<CellsInitializer>();

                for (int j = 0; j < _cellsId.Length; j++)
                {
                    Entity checkEntity = world.GetEntityById(_cellsId[j]);
                    CellsInitializer checkCell = checkEntity.Get<CellsInitializer>();

                    bool? right = null;
                    bool? up = null;
                    int difference;
                    
                    if (cell.index.y == checkCell.index.y)
                    {
                        difference = cell.index.x - checkCell.index.x;
                        right = difference == -1 ? true : difference == 1 ? false : null;
                    }

                    if (cell.index.x == checkCell.index.x)
                    {
                        difference = cell.index.y - checkCell.index.y;
                        up = difference == -1 ? true : difference == 1 ? false : null;
                    }

                    if (right != null && up != null) continue;

                    if (right == true) cell.adjacentCells.right = checkEntity.id;
                    else if (right == false) cell.adjacentCells.left = checkEntity.id;

                    if (up == true) cell.adjacentCells.up = checkEntity.id;
                    else if (up == false) cell.adjacentCells.down = checkEntity.id;
                }

                if (cell.index.x == 0) cell.adjacentCells.left = world.GetEntityById(_cellsIdByIndex[data.Count.x - 1][cell.index.y]).id;
                else if (cell.index.x == (data.Count.x - 1)) cell.adjacentCells.right = world.GetEntityById(_cellsIdByIndex[0][cell.index.y]).id;

                if (cell.index.y == 0) cell.adjacentCells.down = world.GetEntityById(_cellsIdByIndex[cell.index.x][data.Count.y - 1]).id;
                else if (cell.index.y == (data.Count.y - 1)) cell.adjacentCells.up = world.GetEntityById(_cellsIdByIndex[cell.index.x][0]).id;
            }

           // Debug.Log(world.GetEntityById(world.GetEntityById(_cellsId[0]).Get<CellsInitializer>().adjacentCells.up).Get<CellsInitializer>().index);
           // Debug.Log(world.GetEntityById(world.GetEntityById(_cellsId[0]).Get<CellsInitializer>().adjacentCells.down).Get<CellsInitializer>().index);
           // Debug.Log(world.GetEntityById(world.GetEntityById(_cellsId[0]).Get<CellsInitializer>().adjacentCells.left).Get<CellsInitializer>().index);
           // Debug.Log(world.GetEntityById(world.GetEntityById(_cellsId[0]).Get<CellsInitializer>().adjacentCells.right).Get<CellsInitializer>().index);
        }

        private void GetDataForCell(FeatureCellsData data, int i, out Vector2 position, out Vector2Int index)
        {
            float x = i % data.Count.x;
            float y = (int)(i / data.Count.x);

            index = new Vector2Int((int)x, (int)y);

            x *= data.Offset.x;
            y *= data.Offset.y;

            position = new Vector2(x, y);
        }

        protected override void OnDeconstruct() {
            
        }

        public bool TryGetFreeCell(ref Entity entity)
        {
            entity = Entity.Empty;
            
            for (int i = 0; i < 10; i++)
            {
                int index = UnityEngine.Random.Range(0, _cellsId.Length);
                var cell = world.GetEntityById(_cellsId[index]);
                var isCell = cell.Get<IsCell>();

                if (isCell.IsFree)
                {
                    entity = cell;
                    return true;
                }
            }
            return false;
        }

        public bool TryGetAdjacentCell(int cellId, ref Entity entity)
        {
            entity = Entity.Empty;
            Entity checkEntity = world.GetEntityById(cellId);
            ref IsCell cell = ref checkEntity.Get<IsCell>();

            entity = checkEntity;

            checkEntity = world.GetEntityById(cell.adjacentCells.up);
            if (checkEntity.Get<IsCell>().IsFree) entity = checkEntity;

            checkEntity = world.GetEntityById(cell.adjacentCells.left);
            if (checkEntity.Get<IsCell>().IsFree) entity = checkEntity;

            checkEntity = world.GetEntityById(cell.adjacentCells.down);
            if (checkEntity.Get<IsCell>().IsFree) entity = checkEntity;

            checkEntity = world.GetEntityById(cell.adjacentCells.right);
            if (checkEntity.Get<IsCell>().IsFree) entity = checkEntity;

            //Debug.Log(world.GetEntityById(cell.adjacentCells.right).Get<IsCell>().IsFree);

            return entity.id != world.GetEntityById(cellId).id;
        }
    }

}
/*   Check Adjacent cell
                    Debug.Log("Index: " + isCell.index);
                    Debug.Log("Left: " + world.GetEntityById(isCell.adjacentCells.left).Get<IsCell>().index);
                    Debug.Log("Right: " + world.GetEntityById(isCell.adjacentCells.right).Get<IsCell>().index);
                    Debug.Log("Up: " + world.GetEntityById(isCell.adjacentCells.up).Get<IsCell>().index);
                    Debug.Log("Down: " + world.GetEntityById(isCell.adjacentCells.down).Get<IsCell>().index);*/