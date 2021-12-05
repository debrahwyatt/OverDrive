using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;

    //[Range(0.0f, 50.0f)]
    public float moveSpeed = 30f;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

/*    private void OnTriggerEnter(Collider other)
    {
        TargetJoint2D target = Collider.GetComponent<Target>();
        if(Collider.GetComponent)
    }*/

}
