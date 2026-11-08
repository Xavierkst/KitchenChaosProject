using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOptionsUI : MonoBehaviour {
    [SerializeField] private Button MusicVolumeButton;
    [SerializeField] private TextMeshProUGUI MusicVolumeText;
    [SerializeField] private Button SoundEffectsButton;
    [SerializeField] private TextMeshProUGUI SoundEffectsText;
    [SerializeField] private Button CloseOptionsButton;

    private void Awake() {
        MusicVolumeButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        SoundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        CloseOptionsButton.onClick.AddListener(() => {
            Hide();
        });

        GameMgr.Instance.OnGameUnpaused += GameMgr_OnGameUnpaused;
    }

    private void GameMgr_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        MusicVolumeText.text = "MUSIC: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        SoundEffectsText.text = "SOUND EFFECTS: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
    }

    private void Start() {
        Hide();
        UpdateVisual();
        GamePausedUI.OnOptionsButtonClicked += GamePausedUI_OnOptionsButtonClicked;
    }

    private void GamePausedUI_OnOptionsButtonClicked(object sender, System.EventArgs e) {
        Show();
    }

    // Make another listener for when user closes options menu to hide

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void OnDestroy() {
        MusicVolumeButton.onClick.RemoveAllListeners();
        SoundEffectsButton.onClick.RemoveAllListeners();
    }
}
