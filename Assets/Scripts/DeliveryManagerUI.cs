using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    // Listen for when an ingredient gets spawned, and instantiate a RecipeTemplate
    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeGenerated;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeGenerated(object sender, System.EventArgs e) {
        // Generate a new recipeTemplate object with the appropriate details about the recipe
        UpdateVisual();
    }
    private void UpdateVisual() {
        // Clear all pre-existing containers b4 regenerating new ones
        ClearAllRecipeTemplates();
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()) {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
        }
    }
    private void ClearAllRecipeTemplates() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
    }
}
