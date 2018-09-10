using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

    #region Serializable fields

    [SerializeField]
    Slider healthBar;

    #endregion


    #region Unity lifecycle


    void Start()
    {
        EventController.Subscribe(Consts.Events.events.updateHealth, UpdateHealthBar);
    }

    #endregion


    #region private methods

    void UpdateHealthBar()
    {
        
        float health = GameObject.Find("Player").GetComponent<Player>().Health;
        health /= 100f;
        Debug.Log(health);

        if (health >= 0)
        {
            Debug.Log("UpdateHealthBar");
            healthBar.value = health;
        }
    }

    #endregion

}
