using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        // if player is NOT holding kitchenObj and ClearCounter holds a kitchenObj, switch KitchenObjectParent
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                // both don't have objects
            }
        } else {
            // if player is holding kitchenObj and ClearCounter is not, switch
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            } 
        }
    }
}
