using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;

    // Every kitchen obj tracks which clear counter its currently on, and every clear counter tracks which kitchen obj
    // it has.
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }

    public void SetClearCounter(ClearCounter clearCounter) {
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }

        if (clearCounter.HasKitchenObject()) {
            Debug.Log("Counter already has a kitchen object!");
        }
        this.clearCounter = clearCounter;
        this.clearCounter.SetKitchenObject(this);

        // Setting this transf as this KitchenObject's parent transform will apply the parent transform to it
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        // remb to reset object's objectSpace to origin/zero just incase
        transform.localPosition = Vector3.zero; 
    }
}
