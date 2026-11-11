using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOptionsUI : MonoBehaviour {
    public static GameOptionsUI Instance { get; private set; }

    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button closeOptionsButton;
    [SerializeField] private Button MoveUpRebindButton;
    [SerializeField] private Button MoveDownRebindButton;
    [SerializeField] private Button MoveLeftRebindButton;
    [SerializeField] private Button MoveRightRebindButton;
    [SerializeField] private Button InteractRebindButton;
    [SerializeField] private Button AltInteractRebindButton;
    [SerializeField] private Button PauseRebindButton;
    [SerializeField] private Button GamepadInteractRebindButton;
    [SerializeField] private Button GamepadAltInteractRebindButton;
    [SerializeField] private Button GamepadPauseRebindButton;


    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI MoveUpRebindText;
    [SerializeField] private TextMeshProUGUI MoveDownRebindText;
    [SerializeField] private TextMeshProUGUI MoveLeftRebindText;
    [SerializeField] private TextMeshProUGUI MoveRightRebindText;
    [SerializeField] private TextMeshProUGUI InteractRebindText;
    [SerializeField] private TextMeshProUGUI AltInteractRebindText;
    [SerializeField] private TextMeshProUGUI PauseRebindText;
    [SerializeField] private TextMeshProUGUI GamepadInteractRebindText;
    [SerializeField] private TextMeshProUGUI GamepadAltInteractRebindText;
    [SerializeField] private TextMeshProUGUI GamepadPauseRebindText;


    [SerializeField] private Transform rebindKeyBackground;

    private void Awake() {
        Instance = this;
        HideRebindKeyBackground();
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

        MoveUpRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveUp);
        });

        MoveDownRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveDown);
        });

        MoveLeftRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveLeft);
        });

        MoveRightRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveRight);
        });

        InteractRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact);
        });

        AltInteractRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.AltInteract);
        });

        PauseRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Pause);
        });

        GamepadInteractRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.GamepadInteract);
        });

        GamepadAltInteractRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.GamepadAltInteract);
        });

        GamepadPauseRebindButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.GamepadPause);
        });
    }

    public void UpdateVisual() {
        musicVolumeText.text = "MUSIC: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        soundEffectsText.text = "SOUND EFFECTS: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);

        MoveUpRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        MoveDownRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        MoveLeftRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        MoveRightRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        InteractRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        AltInteractRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.AltInteract);
        PauseRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        GamepadInteractRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        GamepadAltInteractRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadAltInteract);
        GamepadPauseRebindText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    private void Start() {
        GameMgr.Instance.OnGameUnpaused += GameMgr_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void GameMgr_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void ShowRebindKeyBackground() {
        rebindKeyBackground.gameObject.SetActive(true);
    }

    private void HideRebindKeyBackground() {
        rebindKeyBackground.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding) {
        ShowRebindKeyBackground();
        GameInput.Instance.RebindBinding(binding, () => {
            HideRebindKeyBackground();
            UpdateVisual();
        });
    }
}
