using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] Transform pfBullet;

    private GameObject thisGameObject;
    private Player player;

    void Start()
    {
        thisGameObject = GameObject.Find("Player");
        player = thisGameObject.GetComponent<Player>();
    }
    public void charge(int cost)
    {
        if (Input.GetKey(KeyCode.Mouse1) && !PauseMenu.GameIsPaused && player.currentMana > -cost)
        {
            player.currentMana -= cost;
            player.currentCharge += 0.02f;
            player.projectileLocked = true;
        }
    }

    public void shoot()
    {
        Transform projectileSpawn = GameObject.Find("ProjectileSpawn").transform;
        Transform aimTarget = GameObject.Find("AimTarget").transform;


        if (Input.GetKeyUp(KeyCode.Mouse1) && !PauseMenu.GameIsPaused && player.projectileLocked == true)
        {
            Spawner();
            player.projectileLocked = false;
        }

        void Spawner()
        {
            Vector3 shootDir = (aimTarget.position - projectileSpawn.position).normalized;

            //Initial projectile size modifier
            int modifier = 20 * (int)(player.currentCharge * player.currentPower * 0.000125f);
            Vector3 scale = new Vector3(modifier, modifier, modifier);
            pfBullet.localScale = scale;

            Transform bulletTransform = Instantiate(pfBullet, projectileSpawn.position, Quaternion.identity);
            bulletTransform.GetComponent<Projectile>().Setup(shootDir, player.currentCharge, player.currentPower);
            player.currentCharge = 1f;
        }
    }
}
