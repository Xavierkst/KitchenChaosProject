using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeftUI : MonoBehaviour {
    [SerializeField] private Image timeLeftImage;

    private void Update() {
        timeLeftImage.fillAmount = GameMgr.Instance.GetGameStartedTimerNormalized();
    }
}
