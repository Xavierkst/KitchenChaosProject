using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownText : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private void Awake() {
        countdownText.text = "3";
        Hide();
    }
    private void Start() {
        GameMgr.Instance.OnGameStateChanged += GameMgr_OnGameStateChanged;
    }
    private void Update() {
        countdownText.text = Mathf.Ceil(GameMgr.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void GameMgr_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameMgr.Instance.IsCountingDownToStart()) {
            Show();
            return;
        }

        Hide();
    }

    private void Show() {
        countdownText.gameObject.SetActive(true);
    }

    private void Hide() {
        countdownText.gameObject.SetActive(false);
    }
}
