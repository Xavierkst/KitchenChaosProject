using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownText : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    int prevCountdownNum = 4;
    private string NUMBER_POPUP = "NumberPopup";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        GameMgr.Instance.OnGameStateChanged += GameMgr_OnGameStateChanged;
        Hide();
    }

    private void Update() {
        int currCountdownNum = Mathf.CeilToInt(GameMgr.Instance.GetCountdownToStartTimer());
        countdownText.text = currCountdownNum.ToString();
        if (GameMgr.Instance.IsCountdownToStartActive() && currCountdownNum != prevCountdownNum) {
            prevCountdownNum = currCountdownNum;
            // trigger animation
            animator.SetTrigger(NUMBER_POPUP);
            // trigger sound on sound manager
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void GameMgr_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameMgr.Instance.IsCountdownToStartActive()) {
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
