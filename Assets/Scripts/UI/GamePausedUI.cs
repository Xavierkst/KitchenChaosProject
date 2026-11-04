using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Unity

public class GamePausedUI : MonoBehaviour {
    
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
