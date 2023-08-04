using Snake.Features.Items.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Features.Items
{
    [CreateAssetMenu(fileName = "FeatureAppleData", menuName = "ScriptableObjects/FeatureAppleData", order = 1)]
    public class FeatureAppleData : ScriptableObject
    {
        public ItemView viewSource;
    }
}