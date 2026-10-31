using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {
    public static GameMgr Instance { get; private set; }
    private enum State {
        WaitingToStart,
        CountdownToStart,
        GameStarted,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gameStartedTimer = 5f;

    public event EventHandler OnGameStateChanged;

    private void Awake() {
        Instance = this;
        this.state = State.WaitingToStart;
    }

    private void Update() {
        switch(state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f) {
                    waitingToStartTimer = 1f;
                    state = State.CountdownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f) {
                    countdownToStartTimer = 3f;
                    state = State.GameStarted;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                } 
                break;
            case State.GameStarted:
                gameStartedTimer -= Time.deltaTime;
                if (gameStartedTimer <= 0f) {
                    gameStartedTimer = 1f;
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(this.state);
    }

    public bool IsGamePlaying() {
        return state == State.GameStarted;
    }

    public bool IsCountingDownToStart() {
        return state == State.CountdownToStart;
    }
    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }
}
