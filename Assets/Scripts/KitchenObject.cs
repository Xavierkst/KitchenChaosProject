using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;

    // Every kitchen obj tracks which clear counter its currently on, and every clear counter tracks which kitchen obj
    // it has.
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.Log("KitchenObjectParent already has a kitchen object!");
        }
        this.kitchenObjectParent = kitchenObjectParent;
        this.kitchenObjectParent.SetKitchenObject(this);

        // Setting this transf as this KitchenObject's parent transform will apply the parent transform to it
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        // reset object's objectSpace to origin/zero just incase
        transform.localPosition = Vector3.zero; 
    }
    public void DestroySelf() {
        this.kitchenObjectParent.ClearKitchenObject();
        Destroy(this.gameObject);
    }

    public static KitchenObject SpawnKitchenObject(IKitchenObjectParent kitchenObjectParent, KitchenObjectSO kitchenObjectSO) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}
