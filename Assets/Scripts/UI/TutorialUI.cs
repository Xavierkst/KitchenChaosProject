using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI UpKeyText;
    [SerializeField] private TextMeshProUGUI LeftKeyText;
    [SerializeField] private TextMeshProUGUI DownKeyText;
    [SerializeField] private TextMeshProUGUI RightKeyText;
    [SerializeField] private TextMeshProUGUI InteractKeyText;
    [SerializeField] private TextMeshProUGUI AltInteractKeyText;
    [SerializeField] private TextMeshProUGUI PauseKeyText;
    [SerializeField] private TextMeshProUGUI GamepadInteractKeyText;
    [SerializeField] private TextMeshProUGUI GamepadAltInteractKeyText;
    [SerializeField] private TextMeshProUGUI GamepadPauseKeyText;

    private void Start() {
        GameInput.Instance.OnRebindBinding += GameInput_OnRebindBinding;
        GameMgr.Instance.OnGameStateChanged += GameMgr_OnGameStateChanged;
        UpdateVisual();
        Show();
    }
    private void GameMgr_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameMgr.Instance.IsCountdownToStartActive()) {
            Hide();
        }
    }
    private void GameInput_OnRebindBinding(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        UpKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        DownKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        LeftKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        RightKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        InteractKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        AltInteractKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.AltInteract);
        PauseKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        GamepadInteractKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        GamepadAltInteractKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadAltInteract);
        GamepadPauseKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
