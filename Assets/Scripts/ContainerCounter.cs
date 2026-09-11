using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {

    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public override void interact(Player player) {

        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            // If object exists, transfer it to the Player
            kitchenObject.SetKitchenObjectParent(player);
        }
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
    }
}

