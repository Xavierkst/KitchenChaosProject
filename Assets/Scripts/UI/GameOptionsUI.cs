using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOptionsUI : MonoBehaviour {
    public static GameOptionsUI Instance { get; private set; }

    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private Button closeOptionsButton;

    private void Awake() {
        Instance = this;
        musicVolumeButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeOptionsButton.onClick.AddListener(() => {
            Hide();
        });
    }

    private void UpdateVisual() {
        musicVolumeText.text = "MUSIC: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        soundEffectsText.text = "SOUND EFFECTS: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
    }

    private void Start() {
        UpdateVisual();
        Hide();
        GameMgr.Instance.OnGameUnpaused += GameMgr_OnGameUnpaused;
    }

    private void GameMgr_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }
}
