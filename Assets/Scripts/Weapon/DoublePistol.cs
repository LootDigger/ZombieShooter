using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tod;

public class DoublePistol : MonoBehaviour {

    #region Private fields

    private bool isNeedToShoot;
    private bool isRightPistolShooting;
    private float shootingDelay;

    #endregion

    #region Serializable fields

    [SerializeField]
    private GameObject[] bulletSpawnPos;

    [SerializeField]
    private GameObject[] handBeginingPos;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private MobileInputController rightStick;

    [SerializeField]
    private GameObject fireBall;

    #endregion



    #region Unity lifeCycle

    void Start()
    {
        CalculateShootingDelay();
       // EventController.Subscribe(Consts.Events.events.upgradeWeapon, UpgradeWeapon);
        EventController.Subscribe(Consts.Events.events.replay, Replay);
        EventController.pickUpM16 += PickUpM16;
        EventController.pickUpM14 += PickUpM14;


        isRightPistolShooting = false;
        StartCoroutine(Shooting());
    }


    void Update()
    {

        if (rightStick.Vertical != 0 || rightStick.Horizontal != 0)
        {
            isNeedToShoot = true;
        }
        else if (rightStick.Vertical == 0 && rightStick.Horizontal == 0)
        {
            isNeedToShoot = false;
        }

    }

    #endregion



    #region Private Methods

    void PickUpM16()
    {

        if (gameObject.activeSelf)
            gameObject.SetActive(false);

    }



    void PickUpM14()
    {

        if (gameObject.activeSelf)
            gameObject.SetActive(false);

    }


    void Replay()
    {

        Consts.Values.Weapons.PistolShootingSpeed = Consts.Values.Weapons.minimumPistolShootingSpeed;
        CalculateShootingDelay();

    }

    void CalculateShootingDelay()
    {
        shootingDelay = (60f/ Consts.Values.Weapons.PistolShootingSpeed);
    }

    void UpgradeWeapon()
    {
        Consts.Values.Weapons.PistolShootingSpeed *= 1.5f;
        CalculateShootingDelay();
    }

    IEnumerator Shooting()
    {

        Shot();
        yield return new WaitForSeconds(shootingDelay);
        StartCoroutine(Shooting());
    }


    IEnumerator BulletLife(GameObject go)
    {

       
        yield return new WaitForSeconds(5f);
      
        UnityPoolManager.Instance.Push(go.GetComponent<UnityPoolObject>());
        
    }




    void Shot()
    {
        if (isNeedToShoot)
        {
           
            int i;
            if (isRightPistolShooting)
                i = 1;
            else
                i = 0;
            Vector3 bulletFlyDirection;
            float x = bulletSpawnPos[i].transform.position.x - handBeginingPos[i].transform.position.x;
            float z = bulletSpawnPos[i].transform.position.z - handBeginingPos[i].transform.position.z;

            Destroy(Instantiate(fireBall,bulletSpawnPos[i].transform.position,Quaternion.identity), 0.1f);

            bulletFlyDirection = new Vector3(x, 0, z);
            GameObject go = UnityPoolManager.Instance.Pop<UnityPoolObject>(5, true).gameObject;

       
            go.GetComponent<Rigidbody>().velocity = Vector3.zero;
            go.transform.SetPositionAndRotation(bulletSpawnPos[i].transform.position, Quaternion.Euler(Vector3.zero));
          
            StartCoroutine(BulletLife(go));

            go.GetComponent<Rigidbody>().AddForce(bulletFlyDirection * 100f, ForceMode.Force);


            isRightPistolShooting = !isRightPistolShooting;
        }
    }

    #endregion
}
