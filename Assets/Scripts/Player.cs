using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;
 
    private void Update() {
        HandleMovement();
        HandleInteraction();
    }
    private void HandleInteraction() {
        float interactDist = 2f;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }
        bool withinInteractDist = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDist, countersLayerMask);
        if (withinInteractDist) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                clearCounter.interact();
            }
        } 
    }
    private void HandleMovement() {
        float playerRadius = .5f;
        float playerHeight = 2f;
        float moveDist = Time.deltaTime * moveSpeed;
        Vector2 inputVec = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        isWalking = moveDir != Vector3.zero;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDist);
        // Let object slide left or right if 2 buttons are being pressed:
        if (!canMove) {
            // Try moving in x direction:
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDist); 
            if (canMove) {
                moveDir = moveDirX;
            } else {
                // can't move X so try Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDist); 
                if (canMove) {
                    moveDir = moveDirZ;
                } else {

                }
            }
        }
        
        if (canMove) {
            transform.position += moveDir * moveDist;
        }
        

    }
    public bool IsWalking() {
        return isWalking;
    }
}
