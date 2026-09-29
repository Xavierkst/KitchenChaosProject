using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    public event EventHandler onPlayerGrabbedSlicedObject;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // if player is holding kitchenObj and CuttingCounter is not, assign it over
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if (!player.HasKitchenObject()) {
                // CuttingCounter has an object, player doesn't. So, chop the item
                // CutObjects(player);
                GetKitchenObject().SetKitchenObjectParent(player);
            } 
        } 
    }

    public override void InteractAlternate(Player player) {
        // kitchen object must exist on the counter and must not be chopped
        if (HasKitchenObject()) {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(player, cutKitchenObjectSO);
            // onPlayerGrabbedSlicedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
