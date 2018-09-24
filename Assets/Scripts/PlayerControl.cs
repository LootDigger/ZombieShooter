using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    #region Private methods

    Rigidbody rb;

    #endregion


    #region Serialized fields

    [SerializeField]
    private MobileInputController leftStick;

    [SerializeField]
    private MobileInputController rightStick;

    #endregion



    #region Unity lifeCycle

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
       // if (isAlive)
        {
            PlayerControll();
        }

    }

    #endregion



    #region PrivateMethod

    void PlayerControll()
    {
        rb.MovePosition(transform.position + Vector3.right * Consts.Values.Player.speed * leftStick.Horizontal + Vector3.forward * Consts.Values.Player.speed * leftStick.Vertical);

        if (rightStick.isPressed)
        {
            Vector3 tmpAngles = transform.localEulerAngles;
            tmpAngles.y = Vector3.Angle(new Vector3(0, 1), new Vector2(rightStick.Horizontal, rightStick.Vertical));


            if (rightStick.Horizontal < 0)
                tmpAngles.y = 360 - tmpAngles.y;
            transform.localEulerAngles = tmpAngles;
        }

    }

    #endregion


}
