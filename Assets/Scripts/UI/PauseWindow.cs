using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : Window {

    #region UnityLifeCycle

    void Start()
    {
        EventController.Subscribe(Consts.Events.events.pause, OpenWindow);
        CloseWindow();
    }


    #endregion



    #region private methods

    protected override void OpenWindow()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Otkrzyc");
    }


    protected override void CloseWindow()
    {
        this.gameObject.SetActive(false);

    }

    #endregion
}
