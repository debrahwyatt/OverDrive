using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Melee : MonoBehaviour
{
    private Vector3 shootDir;
    private Controller controller;
    private GameObject thisGameObject;

    private float initialVelocity;

    //[Range(0.0f, 50.0f)]
    private float moveSpeed = 100f;
    public void Setup(Vector3 shootDir, float lifeTime)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, lifeTime);
        thisGameObject = GameObject.Find("Player");
        controller = thisGameObject.GetComponent<Controller>();
    }

    private void Update()
    {
        //initialVelocity = controller.currentVelocity;
        transform.position += shootDir * (moveSpeed) * Time.deltaTime;
    }
}
