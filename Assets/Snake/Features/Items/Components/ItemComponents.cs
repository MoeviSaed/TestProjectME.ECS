using ME.ECS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Features.Items.Components
{
    public struct IsItem : IComponent
    {
        public int currentEntityId;
        public Look look;
        public Vector3 spawnOffset;

        public static float GetZAxisByLook(Look look)
        {
            return look switch
            {
                Look.right => 180f,
                Look.down => 90f,
                Look.left => 0f,
                Look.up => 270f,
                _ => 0f,
            };
        }
    }

    public struct ItemToMove : IComponent
    {
        public int moveCellId;
    }

    public struct IsApple : IComponent
    {

    }
    public struct AppleInitializer : IComponent
    {
        public Vector3 spawnOffset;
    }

    public struct SnakeInitializer : IComponent
    {
        public int Index;
        public Vector3 spawnOffset;
    }

    public struct IsSnake : IComponent
    {
        public int Index;

        public bool IsHead => Index == 0;
    }


    public enum Look
    {
        up = 0,
        down = 1,
        right = 2,
        left = 3
    }
}