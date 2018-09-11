﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region private fields

    private Rigidbody rb;
    
    #endregion

    
    #region serialize Fields

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private MobileInputController leftStick;

    [SerializeField]
    private MobileInputController rightStick;

    //[SerializeField]
    //private GameObject bullet;

    //[SerializeField]
    //private GameObject bulletSpawnPos;

    #endregion


    #region Properties

    public float Health { get; set; }

    #endregion


    #region Unity lifeCycle


    void Start()
    {
        Health = 100f;
        EventController.Subscribe(Consts.Events.events.hitPlayer, HitPlayer);
        rb = GetComponent<Rigidbody>();
      //  StartCoroutine(Shooting());
    }

    void FixedUpdate()
    {
        PlayerControl();
        CheckHealth();      
        
    }

    #endregion


    #region private methods

    void PlayerControl()
    {
        Debug.Log(leftStick.Horizontal);
        Debug.Log(rightStick.Vertical);
        rb.MovePosition(transform.position + Vector3.right * speed * leftStick.Horizontal + Vector3.forward * speed * leftStick.Vertical);


        Vector3 tmpAngles = transform.localEulerAngles;
         tmpAngles.y = Vector3.Angle(new Vector3(0, 1), new Vector2(rightStick.Horizontal,rightStick.Vertical));

       if (rightStick.Horizontal < 0)
            tmpAngles.y = 360 - tmpAngles.y;
        transform.localEulerAngles = tmpAngles;

    }


    //void Shot()
    //{
    //    Vector3 bulletFlyDirection;
    //    float x = bulletSpawnPos.transform.position.x - transform.position.x;
    //    float z = bulletSpawnPos.transform.position.z - transform.position.z;
         
    //    bulletFlyDirection = new Vector3(x, 0, z);
    //    GameObject go = Instantiate(bullet, bulletSpawnPos.transform.position, Quaternion.identity);
    //    Destroy(go, 5f);
    //   go.GetComponent<Rigidbody>().AddForce(bulletFlyDirection * 100f, ForceMode.Force);

    //}


    void HitPlayer()
    {
        Health -= Consts.Values.damage;
        Debug.Log("Player health now is " + Health);
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
    }

    //IEnumerator Shooting()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    Shot();
    //    StartCoroutine(Shooting());

    //}
    
    void CheckHealth()
    {
        //if (Health <= 0)
        //    EventController.InvokeEvent(Consts.Events.events.lose);
    }

    #endregion

}

