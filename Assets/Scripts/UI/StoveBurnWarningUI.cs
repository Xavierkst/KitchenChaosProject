using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;

    private void Awake() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnWarningProgressAmount = 0.5f;
        bool show = e.progressNormalized > burnWarningProgressAmount;
        if (stoveCounter.IsCooked() && show) {
            Show();
            return;
        }
        Hide();
    }

    private void Start() {
        Hide();
        
    }
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
