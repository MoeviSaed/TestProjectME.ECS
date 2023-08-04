using ME.ECS;
using UnityEngine;

namespace Snake.Features.Cells.Components {

    public struct CellsInitializer : IComponent
    {
        public Vector3 position;
        public Vector2Int index;
        public AdjacentCells adjacentCells;
    }

    public struct SceneCellInitialize : IComponent
    {

    }

    public struct IsCell : IComponent
    {
        public AdjacentCells adjacentCells;
        public Vector2Int index;
        public bool IsFree;
    }

    public struct CellContainer : IComponent
    {
        
    }

    public struct AdjacentCells 
    {
        public int up;
        public int down;
        public int right;
        public int left;
    }
}