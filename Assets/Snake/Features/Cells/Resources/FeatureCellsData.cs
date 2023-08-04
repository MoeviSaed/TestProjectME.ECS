using Snake.Features.Cells.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Features.Cells
{
    [CreateAssetMenu(fileName = "FeatureCellsData", menuName = "ScriptableObjects/FeatureCellsData", order = 1)]
    public class FeatureCellsData : ScriptableObject
    {
        public Vector2Int Count;
        public Vector2 Offset;

        public CellView viewSource;
    }
}