using UnityEngine;

#region Namespaces
namespace Snake.Generator.Systems { } namespace Snake.Generator.Components { } namespace Snake.Generator.Modules { } namespace Snake.Generator.Features { } namespace Snake.Generator.Markers { } namespace Snake.Generator.Views { }
#endregion

namespace Snake.Generator
{

    using TState = SnakeState;
    using Snake.Modules;
    using ME.ECS;
    using ME.ECS.Views.Providers;
    using Snake.Generator.Modules;

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    [DefaultExecutionOrder(-1000)]
    public sealed class SnakeInitializer : InitializerBase {

        private World world;
        public float tickTime = 0.033f;
        public uint inputTicks = 3;
        public int entitiesCapacity = 200;

        private GameInitilizer gameInitilizer;

        public void OnDrawGizmos() {

            if (this.world != null) {
                
                this.world.OnDrawGizmos();
            }
        }

        public void Update() {

            if (this.world == null) {

                // Initialize world
                WorldUtilities.CreateWorld<TState>(ref this.world, this.tickTime);
                {
                    #if FPS_MODULE_SUPPORT
                    this.world.AddModule<FPSModule>();
                    #endif
                    this.world.AddModule<StatesHistoryModule>();
                    this.world.GetModule<StatesHistoryModule>().SetTicksForInput(this.inputTicks);
                    this.world.AddModule<NetworkModule>();
                    
                    // Create new state
                    this.world.SetState<TState>(WorldUtilities.CreateState<TState>());
                    
                    // Set world seed
                    this.world.SetSeed(1u);
                    
                    ComponentsInitializer.DoInit();
                    this.world.SetEntitiesCapacity(this.entitiesCapacity);
                    this.Initialize(this.world);

                    Initilize();
                }
                
            }

            if (this.world != null && this.world.IsLoading() == false && this.world.IsLoaded() == false) {
                
                this.world.SetWorldThread(System.Threading.Thread.CurrentThread);
                this.world.Load(() => {
                
                    // Save initialization state
                    this.world.SaveResetState<TState>();

                });
                
            }

            if (this.world != null && this.world.IsLoaded() == true) {

                var dt = Time.deltaTime;
                this.world.PreUpdate(dt);
                this.world.Update(dt);

            }

        }

        private void Initilize()
        {
            gameInitilizer = new GameInitilizer(world);
            gameInitilizer.Initialize();
        }

        public void LateUpdate() {
            
            if (this.world != null && this.world.IsLoaded() == true) this.world.LateUpdate(Time.deltaTime);
            
        }

        public void OnDestroy() {
            
            if (this.world == null || this.world.isActive == false) return;
            
            this.DeInitializeFeatures(this.world);
            // Release world
            WorldUtilities.ReleaseWorld<TState>(ref this.world);

        }

    }
    
}

namespace ME.ECS
{

    public static partial class ComponentsInitializer {

        public static void InitTypeId() {
            
            ComponentsInitializer.InitTypeIdPartial();
            
        }
        
        static partial void InitTypeIdPartial();
        
        public static void DoInit() {
            
            ComponentsInitializer.Init(Worlds.current.GetState(), ref Worlds.currentWorld.GetNoStateData());
            
        }

        static partial void Init(State state, ref World.NoState noState);

    }

}