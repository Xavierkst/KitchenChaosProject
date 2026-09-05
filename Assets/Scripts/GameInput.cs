using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    // Start is called before the first frame update
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        // Enable the input actions map
        playerInputActions.Player.Enable();
        playerInputActions.Player.InteractCounter.performed += InteractCounter_performed;
    }

    private void InteractCounter_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVec = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;
        // Debug.Log(inputVec);
        return inputVec;
    }
}
