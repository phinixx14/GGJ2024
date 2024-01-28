using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager FindInstance() {
        return GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public Canvas PauseUI;
    public Canvas GameOverUI;
    public Canvas YouWinUI;

    Button btnUnpause_Pause;
    Button btnExit_Pause;
    Button btnExit_GameOver;
    Button btnRestart_GameOver;

    public bool IsPaused { get; private set; }
    public int Lives { get; set; }
    public int Score { get; set; }

    private void Start() {
        IsPaused = false;
        Lives = 3;
        Score = 0;

        List<Canvas> canvases = new List<Canvas>(FindObjectsOfType<Canvas>(true));
        PauseUI = canvases.Find(c => c.name == "Pause Screen Canvas").GetComponent<Canvas>();
        GameOverUI = canvases.Find(c => c.name == "Game Over Canvas").GetComponent<Canvas>();
        YouWinUI = canvases.Find(c => c.name == "You Win Canvas").GetComponent<Canvas>();

        List<Button> pauseUIButtons = new List<Button>(PauseUI.gameObject.GetComponentsInChildren<Button>());
        btnUnpause_Pause = pauseUIButtons.Find(btn => btn.name == "Unpause Button").GetComponent<Button>();
        btnExit_Pause = pauseUIButtons.Find(btn => btn.name == "Exit Button").GetComponent<Button>();

        List<Button> gameOverUIButtons = new List<Button>(GameOverUI.gameObject.GetComponentsInChildren<Button>());
        btnRestart_GameOver = gameOverUIButtons.Find(btn => btn.name == "Restart Button").GetComponent<Button>();
        btnExit_GameOver = gameOverUIButtons.Find(btn => btn.name == "Exit Button").GetComponent<Button>();

        PauseUI.gameObject.SetActive(false);
        btnUnpause_Pause.onClick.AddListener(Pause);
        btnExit_Pause.onClick.AddListener(ExitGame);

        GameOverUI.gameObject.SetActive(false);
        btnRestart_GameOver.onClick.AddListener(ResetGame);
        btnExit_GameOver.onClick.AddListener(Application.Quit);

        YouWinUI.gameObject.SetActive(false);
    }

    void PlayerDeath() {
        if (--Lives <= 0) {
            GameOverUI.gameObject.SetActive(true);
        }
    }

    public delegate void OnPlayerDeathArgs(object sender);
    public event OnPlayerDeathArgs OnPlayerDeath;

    public delegate void OnPauseArgs();
    public event OnPauseArgs OnPause;
    public event OnPauseArgs OnUnpause;

    public void TriggerDeath() {
        OnPlayerDeath.Invoke(this);
        PlayerDeath();
    }

    void ResetGame() {
        Debug.Log("RESET GAME");
        Score = 0;
        Lives = 3;
    }
    void ExitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void Pause() {
        if (GameOverUI.gameObject.activeInHierarchy || YouWinUI.gameObject.activeInHierarchy) {
            return;
        }

        if (IsPaused) {
            // do unpause
            OnUnpause.Invoke();
            IsPaused = false;
            PauseUI.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else {
            // do pause
            OnPause.Invoke();
            IsPaused = true;
            PauseUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}