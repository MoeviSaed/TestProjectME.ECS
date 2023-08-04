using Snake.Features.Items.Views;
using UnityEngine;

[CreateAssetMenu(fileName = "FeatureSnakeData", menuName = "ScriptableObjects/FeatureSnakeData", order = 1)]
public class FeatureSnakeData : ScriptableObject
{
    public SnakeView snakeHead;
    public SnakeView snakeBody;
    [Space]
    public int startCount;
}
