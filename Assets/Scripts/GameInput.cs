using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }
    // Start is called before the first frame update
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        // Enable the input actions map
        playerInputActions.Player.Enable();
        playerInputActions.Player.InteractCounter.performed += InteractCounter_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        // Remove all old listeners
        playerInputActions.Player.InteractCounter.performed -= InteractCounter_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        // reconstruct new PlayerInputObject as well : destroy the old PlayerInputActions object
        // so no unfreed memory
        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractCounter_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        // if InteractCounter btn pressed, and event handler not null, invoke it
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVec = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;
        return inputVec;
    }
}
