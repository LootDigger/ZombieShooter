using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        EventController.Subscribe(Consts.Events.events.upgradeWeapon, UpgradeWeapon);
        isRightPistolShooting = false;
        StartCoroutine(Shooting());
    }


    void Update()
    {

        if (rightStick.Vertical != 0 || rightStick.Horizontal != 0)
        {
            isNeedToShoot = true;
        }
        else if(rightStick.Vertical == 0 && rightStick.Horizontal == 0)
        {
            isNeedToShoot = false;
        }

    }
    
    #endregion



    #region Private Methods
    
    void CalculateShootingDelay()
    {
        shootingDelay = (60f/ Consts.Values.Weapons.PistolShootingSpeed);
    }

    void UpgradeWeapon()
    {
        Consts.Values.Weapons.PistolShootingSpeed *= 1.5f;
        CalculateShootingDelay();
        Debug.Log("Shooting firerate now is " + Consts.Values.Weapons.PistolShootingSpeed);
    }

    IEnumerator Shooting()
    {

        Shot();
        yield return new WaitForSeconds(shootingDelay);
        StartCoroutine(Shooting());
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
            GameObject go = Instantiate(bullet, bulletSpawnPos[i].transform.position, Quaternion.identity);
            Destroy(go, 5f);
            go.GetComponent<Rigidbody>().AddForce(bulletFlyDirection * 100f, ForceMode.Force);
            isRightPistolShooting = !isRightPistolShooting;
        }
    }

    #endregion
}
