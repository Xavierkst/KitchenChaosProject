using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    // [SerializeField] private ClearCounter secondClearCounter;
    // [SerializeField] private bool test;

    private KitchenObject kitchenObject;
    // public void Update() {
    //     if (test && Input.GetKeyDown(KeyCode.T)) {
    //         // set the kitch obj to another clear counter
    //         // but this clear counter will still think it owns the kitchen obj
    //         // even tho kitchen object has been set that it is being placed on second clear counter
    //         if (kitchenObject) {
    //             kitchenObject.SetKitchenObjectParent(secondClearCounter);
    //         } 
    //     } 
    // }

    public void interact(Player player) {
        if (!kitchenObject) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            // Debug.Log(kitchenObject.GetKitchenObjectParent());
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
