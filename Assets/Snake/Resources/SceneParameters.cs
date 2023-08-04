using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    [CreateAssetMenu(fileName = "SceneParameters", menuName = "ScriptableObjects/SceneParameters", order = 0)]
    public class SceneParameters : ScriptableObject
    {
        public int startSnakeLenght;
        
    }
}