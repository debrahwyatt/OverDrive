using UnityEngine;


public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;
    private Controller controller;
    private GameObject thisGameObject;
    
    private float initialVelocity;

    //[Range(0.0f, 50.0f)]
    private float moveSpeed;
    public void Setup(Vector3 shootDir, float lifeTime, int currentPower)
    {
        moveSpeed = 100f * currentPower * 0.00025f;
        this.shootDir = shootDir;
        Destroy(gameObject, lifeTime);
        thisGameObject = GameObject.Find("Player");
        controller = thisGameObject.GetComponent<Controller>();
    }

    private void Update()
    {
        initialVelocity = controller.currentVelocity;
        transform.position += shootDir * (moveSpeed + initialVelocity)  * Time.deltaTime;
    }
}
