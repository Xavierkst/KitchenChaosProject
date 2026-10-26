using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] Transform counterTopPoint;
    public static event EventHandler OnObjectPutdown;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter interact called.");
    }

    public virtual void InteractAlternate(Player player) {
        // Debug.LogError("Interact alt called.");
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }

    public KitchenObject GetKitchenObject() {
        return this.kitchenObject;
    }

    public Transform GetKitchenObjectFollowTransform() {
        return this.counterTopPoint;
    }

    public bool HasKitchenObject() {
        return this.kitchenObject != null;
    }


    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null) {
            OnObjectPutdown?.Invoke(this, EventArgs.Empty);
        }
    }
}
