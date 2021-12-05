using UnityEngine;

public class Player : MonoBehaviour {

    public int currentHealth;
    public int currentMana;
    public int currentPower;
    public int gravity;


    public HealthBar healthBar;
    public ManaBar manaBar;
    public PowerBar powerBar;

    public int basePower = 10000;
    public int maxPower = 20000;
    private int maxHealth = 10000;
    private int maxMana = 10000;

    public int chargeRate;

    public float airControlPercent = 1f;

    public Transform projectileStart;
    public Transform target;

    public bool jumping = false;
    public bool flying = false;
    public bool overDrive = false;
    public bool poweringUp = false;
    public bool isGrounded = true;

    [SerializeField] private Transform pfBullet;

    public Controller thisPlayer;
    public OverDrive overDrive2;
    public Ki ki;

    void Start() 
    {
        currentPower = basePower;
        currentHealth = maxHealth;
        currentMana = maxMana;
        gravity = (int) -(currentPower * 0.005f);
        chargeRate = 12;

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);
        powerBar.SetMinMaxPower(basePower, maxPower);
        powerBar.SetPower(basePower);
    }

    void Update() 
    {
        gravity = (int)-(currentPower * 0.005f);

        ki.NaturalGain(5);

        int overDriveCost = 12;
        int overDriveGain = 10;
        int overDriveLoss = 2;
        overDrive2.overDrive(overDriveCost, overDriveGain, overDriveLoss);

        if (Input.GetKeyDown(KeyCode.G)) TakeDamage(20);
   
        PlayerShootProjectiles_OnShoot(200);
    
    }

    void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void ManaAdjust(int difference)
    {
        //never add more than the max
        if(currentMana + difference < maxMana)
        {
            currentMana += difference;
            manaBar.SetMana(currentMana);
        }
        else
        {
            currentMana = maxMana;
        }
    }

    public void SetPower(int power)
    {
        powerBar.SetPower(power);
    }

    // Update is called once per frame
    private void PlayerShootProjectiles_OnShoot(int cost)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !PauseMenu.GameIsPaused && currentMana > cost)
        {
            ManaAdjust(-cost);
            Transform bulletTransform = Instantiate(pfBullet, projectileStart.position, Quaternion.identity);
            Vector3 shootDir = (target.position - projectileStart.position).normalized;
            bulletTransform.GetComponent<Projectile>().Setup(shootDir);
        }
    }
}

