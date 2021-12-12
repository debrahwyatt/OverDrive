using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform pfBullet;

    private GameObject thisGameObject;
    private Player player;

    void Start()
    {
        thisGameObject = GameObject.Find("Player");
        player = thisGameObject.GetComponent<Player>();
    }

    public void Shoot(int cost)
    {
        Transform projectileStart = GameObject.Find("ProjectileSpawn").transform;

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            player.projectileLocked = false;
        }
        if (Input.GetKey(KeyCode.Mouse1) && !PauseMenu.GameIsPaused && player.currentMana > -cost)
        {
            player.ManaAdjust(-cost);
            player.projectileChargeTime += 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && !PauseMenu.GameIsPaused && player.projectileLocked == false)
        {
            Spawner();
            player.projectileLocked = true;
        }
        if (Input.GetKey(KeyCode.Mouse1) && -cost > player.currentMana && player.projectileLocked == false)
        {
            Spawner();
            player.projectileLocked = true;
        }

        void Spawner()
        {
            int modifier = (int)(player.projectileChargeTime * player.currentPower * 0.00012f);
            Vector3 scale = new Vector3(modifier, modifier, modifier);
            pfBullet.localScale = scale;
            Transform bulletTransform = Instantiate(pfBullet, projectileStart.position, Quaternion.identity);

            Vector3 shootDir = (player.target.position - projectileStart.position).normalized;
            bulletTransform.GetComponent<Projectile>().Setup(shootDir, player.projectileChargeTime, player.currentPower);
            player.projectileChargeTime = 1;
        }
    }
}
