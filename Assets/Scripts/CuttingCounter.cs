using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public event EventHandler onPlayerGrabbedSlicedObject;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // if player is holding kitchenObj and CuttingCounter is not, assign it over
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(player, outputKitchenObjectSO);
            // onPlayerGrabbedSlicedObject?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }
}
