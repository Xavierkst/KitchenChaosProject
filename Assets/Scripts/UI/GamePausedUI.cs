using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour {
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            Hide();
            GameMgr.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            Hide();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
    private void Start() {
        Hide();
        GameMgr.Instance.OnGamePaused += GameMgr_OnGamePaused;
        GameMgr.Instance.OnGameUnpaused += GameMgr_OnGameUnpaused;
    }

    private void GameMgr_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void GameMgr_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
