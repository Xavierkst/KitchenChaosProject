using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    // Start is called before the first frame update
    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        // Enable the input actions map
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVec = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;
        // Debug.Log(inputVec);
        return inputVec;
    }
}
