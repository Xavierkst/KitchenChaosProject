using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeSOList;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesCountMax = 4;
    private int recipesDelivered = 0;

    // For UI
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    // For Audio
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    private void Awake() {
        Instance = this;
        spawnRecipeTimer = spawnRecipeTimerMax;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            // Spawn random recipe from waitingRecipeSOList
            if (waitingRecipeSOList.Count < waitingRecipesCountMax) {
                RecipeSO waitingRecipeSO = GenerateRandomRecipeSO();
                if (waitingRecipeSO) {
                    Debug.Log(waitingRecipeSO.name);
                    waitingRecipeSOList.Add(waitingRecipeSO);
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    private RecipeSO GenerateRandomRecipeSO() {
        return recipeSOList.recipeSOList[UnityEngine.Random.Range(0, recipeSOList.recipeSOList.Count)];
    }
    
    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        foreach (RecipeSO recipeSO in waitingRecipeSOList) {
            // Don't bother checking if the no. of ingredients aren't the same
            if (recipeSO.kitchenObjectSOList.Count != plateKitchenObject.GetKitchenObjectSOList().Count) {
                continue;
            }

            // Check each ingredient on the plate and skip to next recipeSO as soon as one mismatch found
            bool isValidRecipe = true;
            foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                isValidRecipe &= recipeSO.kitchenObjectSOList.Contains(plateKitchenObjectSO);
                if (!isValidRecipe) {
                    break;
                }
            }

            if (isValidRecipe) {
                ++recipesDelivered;
                waitingRecipeSOList.Remove(recipeSO);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetRecipesDelivered() {
        return this.recipesDelivered;
    }

}
