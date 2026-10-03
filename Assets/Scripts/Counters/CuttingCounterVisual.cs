using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimator : MonoBehaviour {
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    private const string CUT = "Cut";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnCuttingCmd += ContainerCounter_OnCuttingCmd;
    }

    private void ContainerCounter_OnCuttingCmd(object sender, System.EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
