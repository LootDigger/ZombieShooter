using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region private fields

    private Rigidbody rb;

    #endregion

    
    #region serialize Fields

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private SimpleTouchController leftStick;

    [SerializeField]
    private SimpleTouchController rightStick;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject bulletSpawnPos;

    #endregion


    #region Unity lifeCycle


    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        PlayerControl();

        if(Input.GetKeyDown(KeyCode.Space))
        { Shot(); }
    }

    #endregion


    #region private methods

    void PlayerControl()
    {
        rb.MovePosition(transform.position + Vector3.right * speed * leftStick.GetTouchPosition.x + Vector3.forward * speed * leftStick.GetTouchPosition.y);


        Vector3 tmpAngles = transform.localEulerAngles;
         tmpAngles.y = Vector3.Angle(new Vector3(0, 1), rightStick.GetTouchPosition);

        if (rightStick.GetTouchPosition.x < 0)
            tmpAngles.y = 360 - tmpAngles.y;
        transform.localEulerAngles = tmpAngles;

    }


    void Shot()
    {
        Vector3 bulletFlyDirection;
        float x = bulletSpawnPos.transform.position.x - transform.position.x;
        float z = bulletSpawnPos.transform.position.z - transform.position.z;
         
        bulletFlyDirection = new Vector3(x, 0,z);
        Instantiate(bullet,bulletSpawnPos.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(bulletFlyDirection *100f,ForceMode.Force);


    }



    #endregion

}

