using UnityEngine;

namespace Mechadroids {
    public class PlayerActiveState : IEntityState {
        private readonly PlayerEntityHandler playerEntityHandler;
        private readonly InputHandler inputHandler;
        private readonly PlayerTurretReference playerReference;
        private readonly HitIndicator hitIndicatorInstance;

        private float currentSpeed;
        private float rotationAmount;
        private float turretAngle = 0f;
        private float barrelAngle = 0f;

        public PlayerActiveState(
            PlayerEntityHandler playerEntityHandler,
            InputHandler inputHandler,
            PlayerTurretReference playerReference,
            HitIndicator hitIndicatorInstance) {
            this.playerEntityHandler = playerEntityHandler;
            this.inputHandler = inputHandler;
            this.playerReference = playerReference;
            this.hitIndicatorInstance = hitIndicatorInstance;
        }

        public void Enter() {
            Debug.Log("Entered Active state");
        }

        public void LogicUpdate() {
            if(inputHandler.MovementInput.magnitude == 0 && inputHandler.MouseDelta.magnitude == 0) {
                playerEntityHandler.SwitchState(playerEntityHandler.IdleState);
            }
        }

        public void PhysicsUpdate() {
            HandleMovement();
            HandleTurretAiming();
        }

        public void Exit() {
            // Clean up when exiting the state
        }

        private void HandleMovement() {
            if(inputHandler.MovementInput.y != 0) {
                currentSpeed += inputHandler.MovementInput.y * playerReference.playerSettings.acceleration * Time.deltaTime;
            }
            else {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, playerReference.playerSettings.deceleration * Time.deltaTime);
            }
            
            currentSpeed = EntityHelper.HandleSlope(playerReference.tankBody, playerReference.playerSettings.maxSlopeAngle, currentSpeed);
            
            currentSpeed = Mathf.Clamp(currentSpeed, -playerReference.playerSettings.moveSpeed, playerReference.playerSettings.moveSpeed);
            playerReference.tankBody.Translate(Vector3.forward * (currentSpeed * Time.deltaTime));
            
            rotationAmount = inputHandler.MovementInput.x * playerReference.playerSettings.rotationSpeed * Time.deltaTime;
            playerReference.tankBody.Rotate(Vector3.up, rotationAmount);
        }
        private void HandleTurretAiming() {
            Vector2 mouseInput = inputHandler.MouseDelta;

            // Update turret horizontal angle
            turretAngle += mouseInput.x * playerReference.playerSettings.turretRotationSpeed * Time.deltaTime - rotationAmount;
            turretAngle = Mathf.Clamp(turretAngle, playerReference.playerSettings.minTurretAngle, playerReference.playerSettings.maxTurretAngle);

            // Update barrel elevation angle
            barrelAngle -= mouseInput.y * playerReference.playerSettings.barrelRotationSpeed * Time.deltaTime; // Inverted because moving mouse up should raise the barrel
            barrelAngle = Mathf.Clamp(barrelAngle, playerReference.playerSettings.minBarrelElevation, playerReference.playerSettings.maxBarrelElevation);

            // Apply turret rotation relative to tank body
            Quaternion turretRotation = playerReference.tankBody.rotation * Quaternion.Euler(0f, turretAngle, 0f);
            playerReference.turretBase.rotation = turretRotation;


            // Apply barrel rotation
            Quaternion barrelRotation = Quaternion.Euler(barrelAngle, 0f, 0f);
            playerReference.barrel.localRotation = barrelRotation;
        }
    }

    }
