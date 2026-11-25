using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DeliveryResultUI : MonoBehaviour {
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;
    private const string FOOD_DELIVERED_POPUP = "FoodDelivered";
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        Hide();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        Show();
        iconImage.sprite = failedSprite;
        backgroundImage.color = failedColor;
        message.text = "DELIVERY\nFAILED!";
        animator.SetTrigger(FOOD_DELIVERED_POPUP);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        Show();
        iconImage.sprite = successSprite;
        backgroundImage.color = successColor;
        message.text = "DELIVERY\nSUCCESS!";
        animator.SetTrigger(FOOD_DELIVERED_POPUP);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}
