using ME.ECS;

namespace Snake.Features.Cubes.Views {
    
    using ME.ECS.Views.Providers;
    
    public class CubeView : MonoBehaviourView {
        
        public override bool applyStateJob => true;

        public override void OnInitialize() {
            
        }
        
        public override void OnDeInitialize() {
            
        }
        
        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) 
        {   
            transform.position = entity.GetPosition();
        }
        
        public override void ApplyState(float deltaTime, bool immediately) {
            
        }
        
    }
    
}