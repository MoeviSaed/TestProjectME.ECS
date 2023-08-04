
namespace Snake
{
    using TState = SnakeState;
    using Snake.Modules;
    using ME.ECS;
    using ME.ECS.Views.Providers;
    using UnityEngine;
    using Snake.Systems;
    using Snake.Features.Cells;
    using Snake.Features;
    using Snake.Features.Items.Systems;

    public class GameInitilizer
    {
        public World World { get; private set; }

        public GameInitilizer(World world)
        {
            World = world;
        }

        public void Initialize()
        {
            AddSystems();
            InitializeScene();
        }
        
        private void AddSystems()
        {
            World.AddModule<InputModule>();

            World.AddFeature(ScriptableObject.CreateInstance<CellFeature>());
            World.AddFeature(ScriptableObject.CreateInstance<ItemsFeature>());

            SystemGroup group = new SystemGroup("Input");
            group.AddSystem<SnakeMovementSystem>();
        }

        private void InitializeScene()
        {
            SystemGroup group = new SystemGroup("Scene");
            group.AddSystem<SceneInitializeSystem>();
        }
    }
}
