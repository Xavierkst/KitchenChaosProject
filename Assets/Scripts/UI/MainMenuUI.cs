using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    // Grab the Play button and transition from MainMenuScene to GameScene
    [SerializeField] private Button playButton;
    // Grab the Quit button and fire off quit when it gets clicked
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
