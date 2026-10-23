using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
    [SerializeField] private RecipeListSO recipeSOList;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesCountMax = 4;

    private void Awake() {
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

}
