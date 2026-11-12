using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    public event EventHandler<OnCounterChangedEventArgs> OnSelectCounterChanged;
    public event EventHandler OnPlayerMove;
    public event EventHandler OnObjectPickup;

    public class OnCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    public static Player instance;
    public static Player Instance {
        get {
            return instance;
        }
        private set {
            instance = value;
        }
    }

    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private float m_interactDist = 2f;
    private bool m_isWalking;
    private Vector3 lastInteractDir;

    private void Start() {
        // Add listener fn for when event handler is invoked
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void Awake() {
        lastInteractDir = transform.forward;

        if (Instance != null) {
            Debug.LogError("Player has more than one instance!");
        }

        Instance = this;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (!GameMgr.Instance.IsGameStarted() || GameMgr.Instance.isGamePaused()) {
            return;
        }

        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!GameMgr.Instance.IsGameStarted() || GameMgr.Instance.isGamePaused()) {
            return;
        }

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        bool withinInteractDist = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, m_interactDist, countersLayerMask);
        bool rayHitCounter = false;
        if (withinInteractDist) {
            rayHitCounter = raycastHit.transform.TryGetComponent(out BaseCounter clearCounter);
            if (rayHitCounter) {
                if (clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);
                    
                }
            } 
        }
        
        if (!withinInteractDist || !rayHitCounter) {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCtr) {
        this.selectedCounter = selectedCtr;
        OnSelectCounterChanged?.Invoke(this, new OnCounterChangedEventArgs { selectedCounter = this.selectedCounter });
    }

    private void HandleMovement() {
        float playerRadius = .5f;
        float playerHeight = 2f;
        float moveDist = Time.deltaTime * moveSpeed;
        Vector2 inputVec = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDist);
        // Let object slide left or right if 2 buttons are being pressed:
        if (!canMove) {
            // Try moving in x direction:
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = (moveDir.x < -0.5f || moveDir.x > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDist); 

            if (canMove) {
                moveDir = moveDirX;
            } else {
                // can't move X so try Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = (moveDir.z < -0.5f || moveDir.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDist); 
                if (canMove) {
                    moveDir = moveDirZ;
                }
            }
        }
 
        // movement
        if (canMove) {
            transform.position += moveDir * moveDist;
        }

        m_isWalking = moveDir != Vector3.zero;
        // where player is facing
        if (m_isWalking) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
            OnPlayerMove?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsWalking() {
        return m_isWalking;
    }
     public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (this.kitchenObject != null) {
            OnObjectPickup?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }

}
