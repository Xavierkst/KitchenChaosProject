using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public override void interact(Player player) {
        if (!kitchenObject) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            // Transfer object to player: 
            if (!player.HasKitchenObject()) {
                kitchenObject.SetKitchenObjectParent(player);
            } else {
                Debug.Log("Player already holding a kitchen object!");
            }
        }
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }
}
