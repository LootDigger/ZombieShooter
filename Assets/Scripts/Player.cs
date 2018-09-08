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

    #endregion


    #region Unity lifeCycle


    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        PlayerControl();
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





    #endregion

}

