using Snake.Features.Cubes.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "FeatureCubeData", menuName = "ScriptableObjects/FeatureCubeData", order = 1)]
public class FeatureCubeData : ScriptableObject
{
    public int count;
    public CubeView viewSource;
}
