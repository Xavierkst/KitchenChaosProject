using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {
    private List<KitchenObjectSO> kitchenObjectSOList;
    private int ingredientCount;
    private int ingredientCountMax = 3;

    // A function that takes a kitchen object as input and finds if it has a corresp output 
    // (each plateKitchenObjSO is a build up to a completed KitchenObject item)
    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
        ingredientCount = 0;
    }

    public void AddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (kitchenObjectSO && ingredientCount < ingredientCountMax) {
            kitchenObjectSOList.Add(kitchenObjectSO);
            ++ingredientCount;
         }
    }
}
