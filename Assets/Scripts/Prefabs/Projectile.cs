using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int detonationModifier;
    private int currentPower;

    private float moveSpeed;
    private float initialVelocity;
    private float projectileCharge;
    private float detonationSize;

    private bool stop = false;

    private Vector3 shootDir;

    private Controller controller;
    private GameObject thisGameObject;

    public void Setup(Vector3 shootDir, float newProjectileCharge, int newCurrentPower)
    {
        projectileCharge = newProjectileCharge;
        currentPower = newCurrentPower;
        thisGameObject = GameObject.Find("Player");
        controller = thisGameObject.GetComponent<Controller>();

        this.shootDir = shootDir;
        moveSpeed = 25 * currentPower * 0.0005f;
        detonationModifier = (int)(projectileCharge * projectileCharge * currentPower * 0.00025f);
        detonationSize = 150 * detonationModifier;

        Destroy(gameObject, projectileCharge * projectileCharge);

        Physics.IgnoreCollision(thisGameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }
    private void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        if (!stop)
        {
            initialVelocity = controller.currentVelocity;
            transform.position += (shootDir * (moveSpeed + initialVelocity) * Time.deltaTime);
        }
        else
        {
            transform.localScale += new Vector3(detonationModifier, detonationModifier, detonationModifier);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (stop != true && collision.gameObject.name != "projectile")
        {
            stop = true;
            Debug.Log(damage());

            Physics.IgnoreCollision(thisGameObject.GetComponent<Collider>(), GetComponent<Collider>(), false);
            //Debug.Log(detonationSize / 100000f + 0.15f);
            Destroy(gameObject, detonationSize / 100000f + 0.15f);
        }

        if (stop == true && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().currentHealth -= damage();
        }
        if (stop == true && collision.gameObject.tag == "Bot")
        {
            collision.gameObject.GetComponent<Bot>().currentHealth -= damage();
        }
        if (collision.gameObject.tag == "Destructable")
        {
            collision.gameObject.GetComponent<Destructable>().currentHealth -= damage();
        }



        int damage()
        {
            return (int)(currentPower * 0.0001f * projectileCharge * projectileCharge * projectileCharge * detonationSize / transform.localScale.magnitude);
        }

    }
}
