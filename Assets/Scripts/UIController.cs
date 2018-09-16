using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {

    #region Private fields
    
    #endregion


    #region Serializable fields

    [SerializeField]
    LeaderBoard localBoard;

    [SerializeField]
    Slider healthBarSlider;

    [SerializeField]
    TextMeshProUGUI score;

    [SerializeField]
    GameObject scorePanel;

    [SerializeField]
    GameObject startGameBtn;

    [SerializeField]
    GameObject pauseGameBtn;

    [SerializeField]
    GameObject joysticks;

    [SerializeField]
    GameObject healthBar;

    [SerializeField]
    GameObject DeadScreen;

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

        GameConditionsManager.mainScore = 0;
        EventController.Subscribe(Consts.Events.events.startGame, StartGame);
        EventController.Subscribe(Consts.Events.events.updateHealth, UpdateHealthBar);
        EventController.Subscribe(Consts.Events.events.addScoreForTheFZ, AddScoreFastZombie);
        EventController.Subscribe(Consts.Events.events.addScoreForTheSZ, AddScoreSlowZombie);
        EventController.Subscribe(Consts.Events.events.lose, ShowDeathScreen);
        EventController.Subscribe(Consts.Events.events.spawnWave, UpdateWaveCounter);

    }

    #endregion


    #region private methods

    void UpdateWaveCounter()
    {
        GameConditionsManager.currentWave++;       
        currentWaveUI.text = GameConditionsManager.currentWave.ToString();

        if (GameConditionsManager.currentWave >= localBoard.WavesBestResult)
        {
            localBoard.WavesBestResult = GameConditionsManager.currentWave;
        }
    }

    void UpdateHealthBar()
    {
        
        float health = GameObject.Find("Player").GetComponent<Player>().Health;
        health /= 100f;

        if (health >= 0)
        {
            healthBarSlider.value = health; 
        }
    }

    public void PauseGame()
    {
        EventController.InvokeEvent(Consts.Events.events.pause);
    }

    void StartGame()
    {
        startGameBtn.SetActive(false);
        joysticks.SetActive(true);
        healthBar.SetActive(true);
        scorePanel.SetActive(true);
        pauseGameBtn.SetActive(true);
        countWaveUI.SetActive(true);
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
        wavedSurvivedResult.text = GameConditionsManager.currentWave.ToString();
        scoreResult.text = GameConditionsManager.mainScore.ToString();
        DeadScreen.SetActive(true);

        BestScoreResult.text = localBoard.ScoreBestResult.ToString();
        BestWavedSurvivedResult.text = localBoard.WavesBestResult.ToString();


    }

    #endregion

}
