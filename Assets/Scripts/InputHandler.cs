using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechadroids {
    /// <summary>
    /// Handles input state from the Input System
    /// </summary>
    public class InputHandler: InputActions.IPlayerActions, InputActions.IUIActions
        {
        private InputActions inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        public InputActions InputActions => inputActions;

        public void Initialize() {
            inputActions = new();
            inputActions.Player.SetCallbacks(this);
            inputActions.Player.Enable();
            inputActions.UI.SetCallbacks(this);
            inputActions.UI.Enable();
        }

        public void SetCursorState(bool visibility, CursorLockMode lockMode) {
            Cursor.visible = visibility;
            Cursor.lockState = lockMode;
        }

        public void OnMove(InputAction.CallbackContext context) {
            MovementInput = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context) {
            MouseDelta = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context) {
        }

        public void OnInteract(InputAction.CallbackContext context) {
        }

        public void OnCrouch(InputAction.CallbackContext context) {
        }

        public void OnJump(InputAction.CallbackContext context) {
        }

        public void OnPrevious(InputAction.CallbackContext context) {
        }

        public void OnNext(InputAction.CallbackContext context) {
        }

        public void OnSprint(InputAction.CallbackContext context) {
        }

//UI Input
        public void OnNavigate(InputAction.CallbackContext context) {
        }

        public void OnSubmit(InputAction.CallbackContext context) {
        }

        public void OnCancel(InputAction.CallbackContext context) {
        }

        public void OnPoint(InputAction.CallbackContext context) {
        }

        public void OnClick(InputAction.CallbackContext context) {
        }

        public void OnRightClick(InputAction.CallbackContext context) {
        }

        public void OnMiddleClick(InputAction.CallbackContext context) {
        }

        public void OnScrollWheel(InputAction.CallbackContext context) {
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context) {
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) {
        }

        public void Dispose() {
            SetCursorState(true, CursorLockMode.None);
            inputActions.Disable();
        }
    }
}
