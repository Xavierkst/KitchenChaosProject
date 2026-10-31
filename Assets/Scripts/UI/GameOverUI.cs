using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start() {
        GameMgr.Instance.OnGameStateChanged += GameMgr_OnGameStateChanged;
        Hide();
    }

    private void GameMgr_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameMgr.Instance.IsGameOver()) {
            recipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDelivered().ToString();
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
