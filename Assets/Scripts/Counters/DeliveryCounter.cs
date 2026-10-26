using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {
    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // Validate if the recipe is correct by sending in the List<KitchenObjectSO>
                bool deliveredRecipe = DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
                if (deliveredRecipe) {
                    Debug.Log("Recipe was succesfully delivered!");
                } else {
                    Debug.Log("No recipe exists for this plate of ingredients.");
                }
            }
        }
    }
}
