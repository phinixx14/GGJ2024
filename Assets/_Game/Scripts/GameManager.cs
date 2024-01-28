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
    public PlayerManager PlayerPrefab;
    public LevelScroller LevelScrollerPrefab;
    public GameObject[] Stages;

    PlayerManager player;
    LevelScroller levelScroller;
    Canvas TitleUI;
    Canvas PauseUI;
    Canvas GameOverUI;
    Canvas YouWinUI;
    Canvas InstructionsUI;

    Button btnStart_Title;
    Button btnExit_Title;
    Button btnStart_Instructions;
    Button btnUnpause_Pause;
    Button btnExit_Pause;
    Button btnExit_GameOver;
    Button btnRestart_GameOver;

    public bool IsPaused { get; private set; }
    public int Lives { get; set; }
    public int Score { get; set; }
    public int CurrentStageIndex { get; set; }
    public GameObject CurrentStage { get; set; }

    private void Awake() {
        IsPaused = false;
        Lives = 3;
        Score = 0;

        List<Canvas> canvases = new List<Canvas>(FindObjectsOfType<Canvas>(true));
        TitleUI = canvases.Find(c => c.name == "Title Screen Canvas").GetComponent<Canvas>();
        PauseUI = canvases.Find(c => c.name == "Pause Screen Canvas").GetComponent<Canvas>();
        GameOverUI = canvases.Find(c => c.name == "Game Over Canvas").GetComponent<Canvas>();
        YouWinUI = canvases.Find(c => c.name == "You Win Canvas").GetComponent<Canvas>();
        InstructionsUI = canvases.Find(c => c.name == "Instructions Canvas").GetComponent<Canvas>();

        List<Button> titleUIButtons = new List<Button>(TitleUI.gameObject.GetComponentsInChildren<Button>());
        btnStart_Title = titleUIButtons.Find(btn => btn.name == "Start Button").GetComponent<Button>();
        btnExit_Title = titleUIButtons.Find(btn => btn.name == "Exit Button").GetComponent<Button>();
        
        List<Button> pauseUIButtons = new List<Button>(PauseUI.gameObject.GetComponentsInChildren<Button>());
        btnUnpause_Pause = pauseUIButtons.Find(btn => btn.name == "Unpause Button").GetComponent<Button>();
        btnExit_Pause = pauseUIButtons.Find(btn => btn.name == "Exit Button").GetComponent<Button>();

        List<Button> gameOverUIButtons = new List<Button>(GameOverUI.gameObject.GetComponentsInChildren<Button>());
        btnRestart_GameOver = gameOverUIButtons.Find(btn => btn.name == "Restart Button").GetComponent<Button>();
        btnExit_GameOver = gameOverUIButtons.Find(btn => btn.name == "Exit Button").GetComponent<Button>();

        List<Button> InstructionsUIButtons = new List<Button>(InstructionsUI.gameObject.GetComponentsInChildren<Button>());
        btnStart_Instructions = InstructionsUIButtons.Find(btn => btn.name == "Start Button").GetComponent<Button>();

        TitleUI.gameObject.SetActive(true);
        btnStart_Title.onClick.AddListener(TitleStartClicked);
        btnExit_Title.onClick.AddListener(ExitGame);

        InstructionsUI.gameObject.SetActive(false);
        btnStart_Instructions.onClick.AddListener(StartGame);

        PauseUI.gameObject.SetActive(false);
        btnUnpause_Pause.onClick.AddListener(Pause);
        btnExit_Pause.onClick.AddListener(ExitGame);

        GameOverUI.gameObject.SetActive(false);
        btnRestart_GameOver.onClick.AddListener(ResetGame);
        btnExit_GameOver.onClick.AddListener(ExitGame);

        YouWinUI.gameObject.SetActive(false);

        levelScroller = Instantiate(LevelScrollerPrefab, this.transform.parent);
        levelScroller.Stop();
    }
    private void Start() {
        //StartGame();
    }

    void PlayerDeath() {
        //if (--Lives <= 0) {
            GameOverUI.gameObject.SetActive(true);
        //}
    }

    public delegate void OnPlayerDeathArgs(object sender);
    public event OnPlayerDeathArgs OnPlayerDeath;

    public delegate void EmptyArgs();
    public event EmptyArgs OnReachedFinish;

    public delegate void OnPauseArgs();
    public event OnPauseArgs OnPause;
    public event OnPauseArgs OnUnpause;

    void TitleStartClicked() {
        TitleUI.gameObject.SetActive(false);
        InstructionsUI.gameObject.SetActive(true);
    }
    void StartGame() {
        InstructionsUI.gameObject.SetActive(false);
        player = Instantiate(PlayerPrefab);
        CurrentStageIndex = 0;
        CurrentStage = Stages[CurrentStageIndex];
        BuildStage(0);
    }
    GameObject BuildStage(int stageIndex) {
        levelScroller.Stop();

        if (levelScroller.stage) {
            Destroy(levelScroller.stage.gameObject);
        }
        
        GameObject stage = Instantiate(Stages[CurrentStageIndex]);
        levelScroller.stage = stage.transform;
        CurrentStage = stage;
        player.launcher.stage = stage.transform;
        levelScroller.Resume();
        return stage;
    }
    public void TriggerDeath() {
        OnPlayerDeath.Invoke(this);
        PlayerDeath();
    }
    public void TriggerReachedFinish() {
        OnReachedFinish.Invoke();
        YouWinUI.gameObject.SetActive(true);
    }
    void ResetGame() {
        Debug.Log("RESET GAME");
        Destroy(player.gameObject);
        GameOverUI.gameObject.SetActive(false);
        
        Score = 0;
        Lives = 3;

        StartGame();
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