using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter {
    [SerializeField] private CookingRecipeSO[] cookingRecipeSOArray;
    [SerializeField] private BurnedRecipeSO[] burnedRecipeSOArray;
    private float cookingProgress;
    private float burnedProgress;
    private CookingRecipeSO cookingRecipeSO;
    private BurnedRecipeSO burnedRecipeSO;
    private State state;

    private enum State {
        Idle,
        Cooking,
        Cooked, 
        Burned
    }
    public void Start() {
        state = State.Idle;
    }

    public void Update() {
        if (HasKitchenObject()) {
            switch(state) {
                case State.Idle:
                    if (HasKitchenObject()) {
                        state = State.Cooking;
                    }
                    break;
                case State.Cooking:
                    cookingProgress += Time.deltaTime;
                    if (cookingProgress > cookingRecipeSO.cookingProgressMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(this, GetCookingOutputForInput(cookingRecipeSO.input));
                        state = State.Cooked;
                        // swap uncooked to cooked patty CookingRecipeSO
                        burnedRecipeSO = GetBurnedRecipeSOWithInput(cookingRecipeSO.output);
                        burnedProgress = 0f;
                    }
                    break;
                case State.Cooked:
                    burnedProgress += Time.deltaTime;
                    if (burnedProgress > burnedRecipeSO.burnedProgressMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(this, GetBurnedOutputForInput(burnedRecipeSO.input));
                        state = State.Burned;
                    }
                    break;
                case State.Burned:
                    if (!HasKitchenObject()) {
                        state = State.Idle;
                        cookingProgress = 0f;
                        burnedProgress = 0f;
                    }
                    break;
            }
            Debug.Log(state);
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Check that kitchen object has an output for its input -- 
            // should let player put cooked patty if he/she wants to
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cookingRecipeSO = GetCookingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                state = State.Cooking;
                cookingProgress = 0f;
                burnedProgress = 0f;
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
            }
        }
    }

    public CookingRecipeSO GetCookingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSOArray) {
            if (cookingRecipeSO.input == inputKitchenObjectSO) {
                return cookingRecipeSO;
            }
        }
        return null;
    }
    public BurnedRecipeSO GetBurnedRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurnedRecipeSO burnedRecipeSO in burnedRecipeSOArray) {
            if (burnedRecipeSO.input == inputKitchenObjectSO) {
                return burnedRecipeSO;
            }
        }
        return null;
    }


    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        return GetCookingRecipeSOWithInput(inputKitchenObjectSO) != null || 
            GetBurnedRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    public KitchenObjectSO GetCookingOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CookingRecipeSO cookingRecipeSO = GetCookingRecipeSOWithInput(inputKitchenObjectSO);
        return cookingRecipeSO ? cookingRecipeSO.output : null;
    }

    public KitchenObjectSO GetBurnedOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        BurnedRecipeSO burnedRecipeSO = GetBurnedRecipeSOWithInput(inputKitchenObjectSO);
        return burnedRecipeSO ? burnedRecipeSO.output : null;
    }

}
