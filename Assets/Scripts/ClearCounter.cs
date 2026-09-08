using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool test;

    private KitchenObject kitchenObject;
    public void Update() {
        if (test && Input.GetKeyDown(KeyCode.T)) {
            // set the kitch obj to another clear counter
            // but this clear counter will still think it owns the kitchen obj
            // even tho kitchen object has been set that it is being placed on second clear counter
            if (kitchenObject) {
                kitchenObject.SetClearCounter(secondClearCounter);
                Debug.Log(kitchenObject.GetClearCounter());
            }
        } 
    }

    public void interact() {
        if (!kitchenObject) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetClearCounter(this);
        } else {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }
}
