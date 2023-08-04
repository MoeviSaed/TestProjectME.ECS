using ME.ECS;

namespace Snake.Features.Cubes.Components {

    public struct CubeInitializer : IComponent {
        public UnityEngine.Vector3 position;
        public UnityEngine.Vector3 direction;
        public float speed;
    }
    
    public struct IsCube : IComponent
    {
    }

    public struct CubeSpeed : IComponent
    {
        public float value;
    }

    public struct CubeMovementDirection : IComponent
    {
        public UnityEngine.Vector3 value;
    }
}