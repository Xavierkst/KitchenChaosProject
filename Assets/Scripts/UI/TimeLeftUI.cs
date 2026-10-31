using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeftUI : MonoBehaviour {
    [SerializeField] private Image timeLeftImage;

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Update() {
        timeLeftImage.fillAmount = GameMgr.Instance.GetGameStartedTimerNormalized();
    }
}
