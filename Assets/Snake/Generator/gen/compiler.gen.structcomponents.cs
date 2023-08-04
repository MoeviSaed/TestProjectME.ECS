namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.CellsInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.IsCell>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.CubeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.CubeMovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.CubeSpeed>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.AppleInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.IsItem>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.IsSnake>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.ItemToMove>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.SnakeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.CellContainer>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.SceneCellInitialize>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.IsCube>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.IsApple>(true, true, true, false, false, false, false, false, false);

        }

        static partial void Init(State state, ref ME.ECS.World.NoState noState) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.CellsInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.IsCell>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.CubeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.CubeMovementDirection>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.CubeSpeed>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.AppleInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.IsItem>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.IsSnake>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.ItemToMove>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.SnakeInitializer>(false, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.CellContainer>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cells.Components.SceneCellInitialize>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Cubes.Components.IsCube>(true, true, true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Items.Components.IsApple>(true, true, true, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(state, ref noState);


            state.structComponents.ValidateUnmanaged<Snake.Features.Cells.Components.CellsInitializer>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Cells.Components.IsCell>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Cubes.Components.CubeInitializer>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Cubes.Components.CubeMovementDirection>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Cubes.Components.CubeSpeed>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Items.Components.AppleInitializer>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Items.Components.IsItem>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Items.Components.IsSnake>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Items.Components.ItemToMove>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Items.Components.SnakeInitializer>(ref state.allocator, false);
            state.structComponents.ValidateUnmanaged<Snake.Features.Cells.Components.CellContainer>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<Snake.Features.Cells.Components.SceneCellInitialize>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<Snake.Features.Cubes.Components.IsCube>(ref state.allocator, true);
            state.structComponents.ValidateUnmanaged<Snake.Features.Items.Components.IsApple>(ref state.allocator, true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateDataUnmanaged<Snake.Features.Cells.Components.CellsInitializer>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Cells.Components.IsCell>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Cubes.Components.CubeInitializer>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Cubes.Components.CubeMovementDirection>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Cubes.Components.CubeSpeed>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Items.Components.AppleInitializer>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Items.Components.IsItem>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Items.Components.IsSnake>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Items.Components.ItemToMove>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Items.Components.SnakeInitializer>(false);
            entity.ValidateDataUnmanaged<Snake.Features.Cells.Components.CellContainer>(true);
            entity.ValidateDataUnmanaged<Snake.Features.Cells.Components.SceneCellInitialize>(true);
            entity.ValidateDataUnmanaged<Snake.Features.Cubes.Components.IsCube>(true);
            entity.ValidateDataUnmanaged<Snake.Features.Items.Components.IsApple>(true);

        }

    }

}
