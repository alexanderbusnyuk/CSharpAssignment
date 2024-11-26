using Mechadroids.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace Mechadroids {
    public class PlayerEntityHandler : IEntityHandler {
        private readonly PlayerPrefabs playerPrefabs;
        private readonly InputHandler inputHandler;
        private readonly Transform playerStartPosition;
        private readonly CinemachineCamera followCamera;
        private readonly DebugMenuHandler debugMenuHandler;

        private PlayerTurretReference playerReference;
        private HitIndicator hitIndicatorInstance;

        public IEntityState CurrentPlayerState { get; set; }
        public IEntityState ActiveState;
        public IEntityState IdleState;

        public PlayerEntityHandler(PlayerPrefabs playerPrefabs,
            InputHandler inputHandler,
            Transform playerStartPosition,
            CinemachineCamera followCamera,
            DebugMenuHandler debugMenuHandler) {
            this.playerPrefabs = playerPrefabs;
            this.inputHandler = inputHandler;
            this.playerStartPosition = playerStartPosition;
            this.followCamera = followCamera;
            this.debugMenuHandler = debugMenuHandler;
        }

        public void Initialize() {
            inputHandler.SetCursorState(false, CursorLockMode.Locked);

            playerReference = Object.Instantiate(playerPrefabs.playerReferencePrefab);
            playerReference.transform.position = playerStartPosition.position;
            followCamera.Follow = playerReference.barrel.transform;

            hitIndicatorInstance = Object.Instantiate(playerPrefabs.hitIndicatorPrefab);
            hitIndicatorInstance.gameObject.SetActive(false);
            IdleState = new PlayerIdleState(this, inputHandler, playerReference, hitIndicatorInstance);
            ActiveState = new PlayerActiveState(this, inputHandler, playerReference, hitIndicatorInstance);

            CurrentPlayerState = IdleState;
            CurrentPlayerState.Enter();

#if GAME_DEBUG
            InitializeDebugMenu();
#endif
        }

        private void InitializeDebugMenu() {

            debugMenuHandler.AddUIElement(UIElementType.Single, "MoveSpeed", new float [] { playerReference.playerSettings.moveSpeed }, (newValue) => {
                playerReference.playerSettings.moveSpeed = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "RotationSpeed", new float[] { playerReference.playerSettings.rotationSpeed }, (newValue) => {
                playerReference.playerSettings.rotationSpeed = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Acceleration", new float[] { playerReference.playerSettings.acceleration }, (newValue) => {
                playerReference.playerSettings.acceleration = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Deceleration", new float[] { playerReference.playerSettings.deceleration }, (newValue) => {
                playerReference.playerSettings.deceleration = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Max Slope Angle", new float[] { playerReference.playerSettings.maxSlopeAngle }, (newValue) => {
                playerReference.playerSettings.maxSlopeAngle = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Turret Rotation Speed", new float[] { playerReference.playerSettings.turretRotationSpeed }, (newValue) => {
                playerReference.playerSettings.turretRotationSpeed = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Barrel Rotation Speed", new float[] { playerReference.playerSettings.barrelRotationSpeed }, (newValue) => {
                playerReference.playerSettings.barrelRotationSpeed = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Max Barrel Elevation", new float[] { playerReference.playerSettings.maxBarrelElevation }, (newValue) => {
                playerReference.playerSettings.maxBarrelElevation = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Min Barrel Elevation", new float[] { playerReference.playerSettings.minBarrelElevation }, (newValue) => {
                playerReference.playerSettings.minBarrelElevation = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Min Turret Angle", new float[] { playerReference.playerSettings.minTurretAngle }, (newValue) => {
                playerReference.playerSettings.minTurretAngle = newValue[0];
            });
            debugMenuHandler.AddUIElement(UIElementType.Single, "Max Turret Angle", new float[] { playerReference.playerSettings.maxTurretAngle }, (newValue) => {
                playerReference.playerSettings.maxTurretAngle = newValue[0];
            });

        }

        public void Tick() {
            CurrentPlayerState.HandleInput();
            CurrentPlayerState.LogicUpdate();
        }

        public void PhysicsTick() {
            CurrentPlayerState.PhysicsUpdate();
            
        }

        public void LateTick() {
            // Implement if needed
        }

        public void SwitchState(IEntityState nextState) {
            CurrentPlayerState.Exit();
            CurrentPlayerState = nextState;
            CurrentPlayerState.Enter();
        }

        public void Dispose() {
            inputHandler.Dispose();
            if (hitIndicatorInstance != null) {
                Object.Destroy(hitIndicatorInstance.gameObject);
                hitIndicatorInstance = null;
            }
        }

        
    }

    // code that handles the player functionality. Should to the correct state

        // 
        //
        // 
        //
        // private void UpdateHitIndicator() {
        //     var ray = new Ray(playerReference.barrelEnd.position, playerReference.barrelEnd.forward);
        //     if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, playerReference.aimLayerMask)) {
        //         hitIndicatorInstance.gameObject.SetActive(true);
        //         hitIndicatorInstance.transform.position = hitInfo.point + hitInfo.normal * 0.01f;
        //         hitIndicatorInstance.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
        //     }
        //     else {
        //         if(hitIndicatorInstance != null) {
        //             hitIndicatorInstance.gameObject.SetActive(false);
        //         }
        //     }
        // }
}
