using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSelected : MonoBehaviour {
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start() {
        Player.Instance.OnSelectCounterChanged += Player_OnSelectCounterChanged;
    }

    private void Player_OnSelectCounterChanged(object sender, Player.OnCounterChangedEventArgs e) {
        if (e.selectedCounter == this.clearCounter) {
            Show();
        } else {
            Hide();
        }
    }
    private void Show() {
        visualGameObject.SetActive(true);
    }

    private void Hide() {
        visualGameObject.SetActive(false);
    }
}
