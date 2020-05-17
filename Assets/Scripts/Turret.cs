using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Transform aimTarget;
    public GameObject aimWarningPrefab;
    public float aimSpeed = 1.0f;
    public float timeToShoot = 1.0f;
    public float range = 1.0f;

    private GameObject currentAimWarning;
    private float shotTimer;

    // Update is called once per frame
    void Update()
    {
        if(currentAimWarning == null && GameManager.instance.DistanceToPlayer(this) < range)
        {
            currentAimWarning = Instantiate(aimWarningPrefab,bulletSpawnPoint.position,aimWarningPrefab.transform.rotation);
        }
        else if(GameManager.instance.DistanceToPlayer(this) < range)
        {
            currentAimWarning.transform.position = Vector3.MoveTowards(currentAimWarning.transform.position,GameManager.instance.playerTransform.position,aimSpeed*Time.deltaTime);
            shotTimer += Time.deltaTime;
            if(shotTimer > timeToShoot)
            {
                Instantiate(bulletPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation,GameManager.instance.levelTransform);
                shotTimer = 0.0f;
            }
        }
    }
}
