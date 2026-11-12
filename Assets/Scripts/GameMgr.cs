using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {
    public static GameMgr Instance { get; private set; }
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GameStarted,
        GameOver
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gameStartedTimer;
    private float gameStartedTimerMax = 40f;
    private bool gamePaused = false;

    public event EventHandler OnGameStateChanged;

    private void Awake() {
        Instance = this;
        this.state = State.WaitingToStart;
        gameStartedTimer = gameStartedTimerMax;
    }

    private void GameMgr_OnInteractAction(object sender, EventArgs e) {
        if (state == State.WaitingToStart && !isGamePaused()) {
            state = State.CountdownToStart;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameMgr_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameMgr_OnInteractAction;
    }

    private void GameMgr_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch(state) {
            case State.WaitingToStart:
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
                    gameStartedTimer = gameStartedTimerMax;
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGameStarted() {
        return state == State.GameStarted;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }
    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }
    public float GetGameStartedTimer() {
        return gameStartedTimer;
    }
    public float GetGameStartedTimerNormalized() {
        return gameStartedTimer / gameStartedTimerMax;
    }
    public bool isGamePaused() {
        return gamePaused;
    }

    public void TogglePauseGame() {
        gamePaused = !gamePaused;

        if (gamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            return;
        }

        Time.timeScale = 1f;
        OnGameUnpaused?.Invoke(this, EventArgs.Empty);
    }
}
