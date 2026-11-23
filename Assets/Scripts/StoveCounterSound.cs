using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    bool playFlashingSound = false;
    private float flashTimer;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float showBurnWarningAmt = 0.5f;
        playFlashingSound = stoveCounter.IsCooked() && e.progressNormalized >= showBurnWarningAmt;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool playAudio = e.state == StoveCounter.State.Cooking || e.state == StoveCounter.State.Cooked;
        if (playAudio) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }

    private void Update() {
        if (playFlashingSound) {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0f) {
                float maxFlashTimer = 0.2f;
                flashTimer = maxFlashTimer;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
