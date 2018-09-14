using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {

    #region Private fields

    private int mainScore;
    private int waveCounter;

    #endregion


    #region Serializable fields

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
    TextMeshProUGUI currentWave;

    [SerializeField]
    GameObject countWaveUI;

    #endregion


    #region Unity lifecycle


    void Start()
    {
        mainScore = 0;
        waveCounter = 1;
        EventController.Subscribe(Consts.Events.events.startGame, StartGame);
        EventController.Subscribe(Consts.Events.events.spawnWave, UpdateWaveCounter);

        EventController.Subscribe(Consts.Events.events.updateHealth, UpdateHealthBar);
        EventController.Subscribe(Consts.Events.events.addScoreForTheFZ, AddScoreFastZombie);
        EventController.Subscribe(Consts.Events.events.addScoreForTheSZ, AddScoreSlowZombie);
        EventController.Subscribe(Consts.Events.events.lose, ShowDeathScreen);

    }

    #endregion


    #region private methods

    void UpdateWaveCounter()
    {
        waveCounter++;
        currentWave.text = waveCounter.ToString();
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
        mainScore += Consts.Values.scoreCountForSlowZombieKill;
        score.text = mainScore.ToString();
    }


    void AddScoreFastZombie()
    {

        mainScore += Consts.Values.scoreCountForFastZombieKill;
        score.text = mainScore.ToString();
    }

    void ShowDeathScreen()
    {
        wavedSurvivedResult.text = waveCounter.ToString();
        scoreResult.text = mainScore.ToString();
        DeadScreen.SetActive(true);

    }

    #endregion

}
