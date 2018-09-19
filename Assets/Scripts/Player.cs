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
    private FlashLight flashLight;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private MobileInputController leftStick;

    [SerializeField]
    private MobileInputController rightStick;

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
            if (Health >= 100f)
                return;
            else
                Destroy(other.gameObject);

            if ((Health + Consts.Values.Meds.medKitCureEffect) >= 100)
            {

                Health = Consts.Values.Player.playermaxHealth;
            }
            else
                Health += Consts.Values.Meds.medKitCureEffect;

            EventController.InvokeEvent(Consts.Events.events.updateHealth);
        }
        else
            if(other.tag == "Booster")
        {
            EventController.InvokeEvent(Consts.Events.events.upgradeWeapon);
            Destroy(other.gameObject);
        }
        else
            if(other.tag == "Battery")
        {
            flashLight.lightPower = Consts.Values.FlashLight.batteryPower;
            Destroy(other.gameObject);
        }

    }

    void Start()
    {
        isAlive = false;
        Health = 100f;
        Debug.Log("Health = 100f");
        EventController.Subscribe(Consts.Events.events.lose, Lose);
        EventController.Subscribe(Consts.Events.events.fZhitPlayer, fZHitPlayer);
        EventController.Subscribe(Consts.Events.events.sZhitPlayer, sZHitPlayer);
        EventController.Subscribe(Consts.Events.events.startGame, StartGame);
        EventController.Subscribe(Consts.Events.events.replay, Replay);


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

    void Replay()
    {
        transform.position = Consts.Values.Player.startPos;
        Health = Consts.Values.Player.playermaxHealth;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        Debug.Log("Health = " + Health);
        isAlive = true;
    }




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

        if (rightStick.isPressed)
        {
            Vector3 tmpAngles = transform.localEulerAngles;
            tmpAngles.y = Vector3.Angle(new Vector3(0, 1), new Vector2(rightStick.Horizontal, rightStick.Vertical));


            if (rightStick.Horizontal < 0)
                tmpAngles.y = 360 - tmpAngles.y;
            transform.localEulerAngles = tmpAngles;
        }
        
    }

    

    void fZHitPlayer()
    {
        Health -= Consts.Values.Zombie.fZDamage;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        Destroy(Instantiate(blood, transform.position, Quaternion.identity),1f);
    }


    void sZHitPlayer()
    {
        Health -= Consts.Values.Zombie.sZDamage;
        EventController.InvokeEvent(Consts.Events.events.updateHealth);
        Destroy(Instantiate(blood, transform.position, Quaternion.identity), 1f);

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

