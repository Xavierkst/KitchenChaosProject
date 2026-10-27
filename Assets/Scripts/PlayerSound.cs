using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {
    private Player player;
    private float footstepsTimer;
    private float footstepsTimerMax = 0.4f;
    public static event EventHandler OnPlayerMove;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepsTimer -= Time.deltaTime;
        if (footstepsTimer < 0f) {
            footstepsTimer = footstepsTimerMax;

            if (player.IsWalking()) {
                OnPlayerMove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
