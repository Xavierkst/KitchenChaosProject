using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSelected : MonoBehaviour {
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start() {
        Player.Instance.OnSelectCounterChanged += Player_OnSelectCounterChanged;
    }

    private void Player_OnSelectCounterChanged(object sender, Player.OnCounterChangedEventArgs e) {
        if (e.selectedCounter == this.baseCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject visualGameObj in visualGameObjectArray) {
            visualGameObj.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject visualGameObj in visualGameObjectArray) {
            visualGameObj.SetActive(false);
        }
    }
}
