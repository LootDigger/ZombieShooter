using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour {

    #region Private fields

    private bool isPaused;
    private Vector3 tmpColor;

    #endregion


    #region Serializable fields

    [SerializeField]
    LeaderBoard localBoard;

    [SerializeField]
    SpriteRenderer bloodEffect;

    [SerializeField]
    Slider healthBarSlider;
    
    [SerializeField]
    Slider flashLightSlider;

    [SerializeField]
    TextMeshProUGUI score;

    [SerializeField]
    GameObject scorePanel;

    [SerializeField]
    GameObject startGameBtn;

    [SerializeField]
    GameObject pauseGameBtn;

    [SerializeField]
    GameObject flashLightBtn;

    [SerializeField]
    GameObject joysticks;

    [SerializeField]
    GameObject lighterBar;

    [SerializeField]
    GameObject healthBar;

    [SerializeField]
    GameObject DeadScreen;

    [SerializeField]
    GameObject PauseScreen;

    [SerializeField]
    Image HealthBarFill;

    [SerializeField]
    FlashLight flashLight;

    [SerializeField]
    TextMeshProUGUI waveCountinPause;

    [SerializeField]
    TextMeshProUGUI ZombieKilledCount;

    [SerializeField]
    TextMeshProUGUI wavedSurvivedResult;

    [SerializeField]
    TextMeshProUGUI scoreResult;

    [SerializeField]
    TextMeshProUGUI BestWavedSurvivedResult;

    [SerializeField]
    TextMeshProUGUI BestScoreResult;

    [SerializeField]
    TextMeshProUGUI currentWaveUI;

    [SerializeField]
    GameObject countWaveUI;

    #endregion


    #region Unity lifecycle


    void Start()
    {
        isPaused = false;
        GameConditionsManager.mainScore = 0;
        EventController.Subscribe(Consts.Events.events.startGame, StartGame);
        EventController.Subscribe(Consts.Events.events.updateHealth, UpdateHealthBar);
        EventController.Subscribe(Consts.Events.events.addScoreForTheFZ, AddScoreFastZombie);
        EventController.Subscribe(Consts.Events.events.addScoreForTheSZ, AddScoreSlowZombie);
        EventController.Subscribe(Consts.Events.events.lose, ShowDeathScreen);
        EventController.Subscribe(Consts.Events.events.spawnWave, UpdateWaveCounter);
        EventController.Subscribe(Consts.Events.events.replay, Replay);
        EventController.Subscribe(Consts.Events.events.updateWaveUI, UpdateWaveCounter);
        EventController.Subscribe(Consts.Events.events.fZhitPlayer, ShowBloodEffect);
        EventController.Subscribe(Consts.Events.events.sZhitPlayer, ShowBloodEffect);


    }



    void Update()
    {
        CheckFlashLight();


    }

    #endregion


    #region private methods
    
    void Replay()
    {
        if(isPaused)
        {
            PauseGame();
        }

        joysticks.SetActive(true);
        DeadScreen.SetActive(false);
        UpdateWaveCounter();
        UpdateHealthBar();
        UpdateScore();
        UpdateWaveCounter();

    }



    void UpdateWaveCounter()
    {
        currentWaveUI.text = GameConditionsManager.currentWave.ToString();

        if (GameConditionsManager.currentWave >= localBoard.WavesBestResult)
        {
            localBoard.WavesBestResult = GameConditionsManager.currentWave;
        }
    }

    void UpdateScore()
    {

        score.text = GameConditionsManager.mainScore.ToString();

    }

    void UpdateHealthBar()
    {
        
        float health = GameObject.Find("Player").GetComponent<Player>().Health;
        health /= 100f;

        if (health >= 0)
        {
            healthBarSlider.value = health; 
        }
        if (health >= 0.5)
        {
            HealthBarFill.color = new Color((1f-health) * 2, 1, 0);
        }
        if (health < 0.5)
        {
            HealthBarFill.color = new Color(1, (health * 2f), 0);
        }

    }

    public void PauseGame()
    {
        OnShowpauseScreen();    
        isPaused = !isPaused;
        EventController.InvokeEvent(Consts.Events.events.pause);
        PauseScreen.SetActive(isPaused);
        pauseGameBtn.SetActive(!isPaused);
    }

    void StartGame()
    {
        startGameBtn.SetActive(false);
        joysticks.SetActive(true);
        healthBar.SetActive(true);
        scorePanel.SetActive(true);
        pauseGameBtn.SetActive(true);
        countWaveUI.SetActive(true);
        flashLightBtn.SetActive(true);
        lighterBar.SetActive(true);
    }


    void AddScoreSlowZombie()
    {
        GameConditionsManager.mainScore += Consts.Values.Zombie.scoreCountForSlowZombieKill;
        score.text = GameConditionsManager.mainScore.ToString();
    
        if(GameConditionsManager.mainScore >=localBoard.ScoreBestResult)
        {
            localBoard.ScoreBestResult = GameConditionsManager.mainScore;
        }


    }


    void AddScoreFastZombie()
    {

        GameConditionsManager.mainScore += Consts.Values.Zombie.scoreCountForFastZombieKill;
        score.text = GameConditionsManager.mainScore.ToString();

        if (GameConditionsManager.mainScore >= localBoard.ScoreBestResult)
        {
            localBoard.ScoreBestResult = GameConditionsManager.mainScore;
        }
    }

    void ShowDeathScreen()
    {
        joysticks.SetActive(false);
        wavedSurvivedResult.text = GameConditionsManager.currentWave.ToString();
        scoreResult.text = GameConditionsManager.mainScore.ToString();
        DeadScreen.SetActive(true);

        BestScoreResult.text = localBoard.ScoreBestResult.ToString();
        BestWavedSurvivedResult.text = localBoard.WavesBestResult.ToString();


    }


    void OnShowpauseScreen()
    {
        ZombieKilledCount.text = GameConditionsManager.countOfKilledZombies.ToString();
        waveCountinPause.text = GameConditionsManager.currentWave.ToString();

    }

    void CheckFlashLight()
    {
        flashLightSlider.value = (flashLight.lightPower/15f);

    }


    void ShowBloodEffect()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(bloodEffect.DOColor(new Vector4(1, 0, 0, 1), 0.2f));
        seq.Append(bloodEffect.DOColor(new Vector4(1, 0, 0, 0), 0.2f));

    }


    #endregion




    #region Public Methods

    public void OnFlashLightTurnedOn()
    {
        EventController.InvokeEvent(Consts.Events.events.flashLightTurned);
    }


    #endregion
}
