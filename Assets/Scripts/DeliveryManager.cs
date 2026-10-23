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
                }
            }
        }
    }

    private RecipeSO GenerateRandomRecipeSO() {
        return recipeSOList.recipeSOList[Random.Range(0, recipeSOList.recipeSOList.Count)];
    }
    
    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        foreach (RecipeSO recipeSO in waitingRecipeSOList) {
            // Check each ingredient on the plate and skip to next recipeSO as soon as one mismatch found
            bool isValidRecipe = true;
            foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                isValidRecipe &= recipeSO.kitchenObjectSOList.Contains(plateKitchenObjectSO);
                if (!isValidRecipe) {
                    break;
                }
            }

            if (isValidRecipe) {
                waitingRecipeSOList.Remove(recipeSO);
                return true;
            }
        }

        return false;
    }

}
