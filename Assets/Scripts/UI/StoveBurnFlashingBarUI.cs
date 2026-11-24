using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    private const string ABOUT_TO_BURN = "AboutToBurn";
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(ABOUT_TO_BURN, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnWarningProgressAmount = 0.5f;
        bool showFlashing = stoveCounter.IsCooked() && e.progressNormalized >= burnWarningProgressAmount;

        animator.SetBool(ABOUT_TO_BURN, showFlashing);
    }
}
