using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {
    private List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    private int ingredientCount;
    private int ingredientCountMax = 6;

    // Need event handler that sends info on what ingredients has just been added 
    // eg. 1 - bread, 2 - cabbage, 3 - .... so on
    // Visual class will SetActive() each portion 

    // A function that takes a kitchen object as input and finds if it has a corresp output 
    // (each plateKitchenObjSO is a build up to a completed KitchenObject item)
    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
        ingredientCount = 0;
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        }

        if (!kitchenObjectSO || isFull() || kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        }

        kitchenObjectSOList.Add(kitchenObjectSO);
        // Invoke event handler when ingredient successfully added
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
            kitchenObjectSO = kitchenObjectSO
        });
        ++ingredientCount;

        return true;
    }

    public bool isFull() {
        return ingredientCount >= ingredientCountMax;
    }
}
