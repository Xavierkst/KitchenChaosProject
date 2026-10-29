using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {
    private Player player;
    private float footstepsTimer;
    private float footstepsTimerMax = 0.1f;
    private float VOLUME = 1f;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepsTimer -= Time.deltaTime;
        // This body only runs every footstepsTimer num of seconds
        if (footstepsTimer < 0f) {
            footstepsTimer = footstepsTimerMax;

            if (player.IsWalking()) {
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, VOLUME);
            }
        }
    }
}
