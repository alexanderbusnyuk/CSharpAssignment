using UnityEngine;

namespace Mechadroids {
    public class PlayerIdleState : IEntityState {
        private readonly PlayerEntityHandler playerEntityHandler;
        private readonly InputHandler inputHandler;
        private readonly PlayerTurretReference playerReference;
        private readonly HitIndicator hitIndicatorInstance;

    public PlayerIdleState(
        PlayerEntityHandler entityhandler,
        InputHandler inputHandler,
        PlayerTurretReference playerReference,
        HitIndicator hitIndicatorInstance) {
        this.playerEntityHandler = entityhandler;
        this.inputHandler = inputHandler;
        this.playerReference = playerReference;
        this.hitIndicatorInstance = hitIndicatorInstance;
        }
        public void Enter() {
            Debug.Log("Entered Idle State");
        }

        public void LogicUpdate() {
            if(inputHandler.MovementInput.magnitude > 0 || inputHandler.MouseDelta.magnitude > 0) {
                playerEntityHandler.SwitchState(playerEntityHandler.ActiveState);
            }
                
        }

        public void PhysicsUpdate() {
            
        }

        public void Exit() {
            // Clean up when exiting the state
        }
    }
}
