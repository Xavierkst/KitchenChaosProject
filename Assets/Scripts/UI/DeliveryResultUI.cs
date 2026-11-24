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

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        Hide();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        iconImage.sprite = failedSprite;
        backgroundImage.color = failedColor;
        message.text = "DELIVERY\nSUCCESS!";
        Show();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        iconImage.sprite = successSprite;
        backgroundImage.color = successColor;
        message.text = "DELIVERY\nFAILED!";
        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}
