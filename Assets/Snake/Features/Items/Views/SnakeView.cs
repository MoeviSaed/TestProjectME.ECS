using Snake.Features.Items.Components;
using UnityEngine;
using UnityEngine.Jobs;
using ME.ECS;

namespace Snake.Features.Items.Views
{
    public class SnakeView : ItemView
    {
        public override void ApplyStateJob(TransformAccess transform, float deltaTime, bool immediately)
        {
            base.ApplyStateJob(transform, deltaTime, immediately);
            transform.rotation = entity.GetRotation();
        }
    }
}