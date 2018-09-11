﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
<<<<<<< HEAD

    

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

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject bulletSpawnPos;
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
        StartCoroutine(Shooting());
      //  StartCoroutine(Shooting());
    }

    void FixedUpdate()
    {
        PlayerControl();
        CheckHealth();



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
        tmpAngles.y = Vector3.Angle(new Vector3(0, 1), new Vector2(rightStick.Horizontal, rightStick.Vertical));

        if (rightStick.Horizontal < 0)
            tmpAngles.y = 360 - tmpAngles.y;
        transform.localEulerAngles = tmpAngles;

    }


    void Shot()
    {
        Vector3 bulletFlyDirection;
        float x = bulletSpawnPos.transform.position.x - transform.position.x;
        float z = bulletSpawnPos.transform.position.z - transform.position.z;

        bulletFlyDirection = new Vector3(x, 0, z);
        GameObject go = Instantiate(bullet, bulletSpawnPos.transform.position, Quaternion.identity);
        Destroy(go, 5f);
        go.GetComponent<Rigidbody>().AddForce(bulletFlyDirection * 100f, ForceMode.Force);

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

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.2f);
        Shot();
        StartCoroutine(Shooting());

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

=======
	public ActionController actionController;
	public Transform spawnpoint;
		// Where to respawn the player on meteorite strike
	public float respawnTime = 4.0f;
		// The time, in seconds, the player should lie dead on the ground before respawning
	
	
	public const int kMaxCargo = 3;
	
	
	private AnimationController animationController;
	private MovementController movementController;
	private int cargo = 0, points = 0;
	private bool respawning = false;
	
	
	public int Cargo
	{
		get
		{
			return cargo;
		}
	}
	
	
	public int Points
	{
		get
		{
			return points;
		}
	}
	
	
	void Reset ()
	{
		Setup ();
	}
	
	
	void Setup ()
	{
		if (actionController == null)
		{
			actionController = GetComponentInChildren<ActionController> ();
		}
	}
	
	
	void Start ()
	{
		Setup ();
		
		if (spawnpoint == null)
		{
			Debug.LogError ("No spawnpoint set. Please correct and restart.", this);
			enabled = false;
			return;
		}
		
		if (actionController == null)
		{
			Debug.LogError ("No action controller set. Please correct and restart.", this);
			enabled = false;
			return;
		}
		
		animationController = GetComponent<AnimationController> ();
		movementController = GetComponent<MovementController> ();
	}
	
	
	public bool AddCargo ()
	{
		if (cargo >= kMaxCargo)
		{
			return false;
		}
		
		cargo++;
		
		return true;
	}
	
	
	public bool UnloadCargo ()
	{
		if (cargo <= 0)
		{
			return false;
		}
		
		cargo--;
		points++;
		
		return true;
	}
	
	
	public void OnMeteoriteStrike ()
	{
		StartCoroutine (Respawn ());
	}
	
	
	IEnumerator Respawn ()
	{
		if (respawning)
		{
			yield break;
		}
		
		respawning = true;
		
		movementController.enabled = false;
		actionController.enabled = false;
		animationController.state = CharacterState.Dying;
		cargo = 0;
		
		yield return new WaitForSeconds (respawnTime);
		
		transform.position = spawnpoint.position;
		transform.rotation = spawnpoint.rotation;
		animationController.state = CharacterState.Falling;
		movementController.enabled = true;
		actionController.enabled = true;
		
		respawning = false;
	}
>>>>>>> parent of 7412d52... Revert "Delete Logs and remove light component from bullet"
}

