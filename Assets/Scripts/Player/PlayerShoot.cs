using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //public Transform target;
    //public GameObject foo = GameObject.Find("")
    [SerializeField] Transform pfBullet;

    public static void PlayerShootProjectiles_OnShoot()
    {
        Transform projectileStart = GameObject.Find("ProjectileSpawn").transform;

        //Transform bulletTransform = Instantiate(pfBullet, projectileStart.position, Quaternion.identity);

        //Vector3 shootDir = (target.position - projectileStart.position).normalized;
        //bulletTransform.GetComponent<Projectile>().Setup(shootDir);
    }
}
