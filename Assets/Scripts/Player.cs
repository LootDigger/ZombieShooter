using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    

    #region private fields

    private Rigidbody rb;
    private bool isAlive;

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

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject blood;

   #endregion


    #region Properties

    public float Health { get; set; }

    #endregion


    #region Unity lifeCycle


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MedKit")
        {
            if (Health == 100f)
                return;
            else
                Destroy(other.gameObject);


           

            if ((Health + Consts.Values.medKitCureEffect) >= 100)
            {
                
                Health = 100f;
            }
            else
                Health += Consts.Values.medKitCureEffect;

            EventController.InvokeEvent(Consts.Events.events.updateHealth);
        }

    }

    void Start()
    {
        isAlive = false;
        Health = 100f;
        EventController.Subscribe(Consts.Events.events.lose, Lose);
        EventController.Subscribe(Consts.Events.events.fZhitPlayer, fZHitPlayer);
        EventController.Subscribe(Consts.Events.events.sZhitPlayer, sZHitPlayer);
        EventController.Subscribe(Consts.Events.events.startGame, StartGame);

        rb = GetComponent<Rigidbody>();
        StartCoroutine(Shooting());
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            PlayerControl();
        }

        CheckHealth();



      


    }

    #endregion


    #region private methods


    void StartGame()
    {
        isAlive = true;
    }

    void Lose()
    {
        isAlive = false;
    }

    void PlayerControl()
    {

        rb.MovePosition(transform.position + Vector3.right * speed * leftStick.Horizontal + Vector3.forward * speed * leftStick.Vertical);


        Vector3 tmpAngles = transform.localEulerAngles;
        tmpAngles.y = Vector3.Angle(new Vector3(0, 1), new Vector2(rightStick.Horizontal, rightStick.Vertical));

        if (rightStick.Horizontal < 0)
            tmpAngles.y = 360 - tmpAngles.y;
        transform.localEulerAngles = tmpAngles;

    }

    

    void fZHitPlayer()
    {
        Health -= Consts.Values.fZDamage;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        Instantiate(blood, transform.position, Quaternion.identity);
    }


    void sZHitPlayer()
    {
        Health -= Consts.Values.sZDamage;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        Instantiate(blood, transform.position, Quaternion.identity);

    }



    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.2f);
        
        StartCoroutine(Shooting());

    }
   

    void CheckHealth()
    {
        if (Health <= 0)
        {
            EventController.InvokeEvent(Consts.Events.events.lose);
            isAlive = false;
        }
    }

    #endregion

}

