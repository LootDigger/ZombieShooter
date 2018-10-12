using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIWindow : Window
{


    #region UnityLifeCycle

    void Start()
    {
        EventController.Subscribe(Consts.Events.events.startGame, OpenWindow);
        CloseWindow();
    }


    #endregion



    #region private methods

    protected override void OpenWindow()
    {
        this.gameObject.SetActive(true);
    }


    protected override void CloseWindow()
    {
        this.gameObject.SetActive(false);

    }

    #endregion




}
