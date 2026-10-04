using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] StoveCounter stoveCounter;
    Animator animator;
    private const string COOK = "Cook";
    private void Awake() {
        animator.GetComponent<Animator>();
    }

    private void Start() {
    }


}
