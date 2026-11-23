using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    private bool flashBurnWarning;

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnWarningProgressAmount = 0.5f;
        bool show = e.progressNormalized >= burnWarningProgressAmount;
        flashBurnWarning = stoveCounter.IsCooked() && show;

        if (flashBurnWarning) {
            Show();
            return;
        } 

        Hide();
    }

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
