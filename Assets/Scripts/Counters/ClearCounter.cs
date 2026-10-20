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
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject() is PlateKitchenObject) {
                    // add ingredient to the player's plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    plateKitchenObject.AddIngredient(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
