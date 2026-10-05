using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveOnGameObject;
    [SerializeField] GameObject sizzlingParticlesGameObject;

    private void Start() {
        // animator.GetComponent<Animator>();
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool showVisual = e.state == StoveCounter.State.Cooked || e.state == StoveCounter.State.Cooking;
        stoveOnGameObject.SetActive(showVisual);
        sizzlingParticlesGameObject.SetActive(showVisual);
    }
}
