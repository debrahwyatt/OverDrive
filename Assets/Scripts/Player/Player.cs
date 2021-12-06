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
    private float chargeTime = 1f;

    public float airControlPercent = 1f;

    public bool flying = true;
    public bool jumping = false;
    public bool overDrive = false;
    public bool poweringUp = false;
    public bool isGrounded = true;
    private bool projectileLocked = false;

    public Transform projectileStart;
    public Transform target;

    [SerializeField] private Transform pfBullet;

    public Controller thisPlayer;
    public OverDrive overDrive2;
    public Ki ki;
    public Jump jump;

    void Start() 
    {
        QualitySettings.vSyncCount = 0; // Disable vSync
        Application.targetFrameRate = 60; // Set Framerate

        currentPower = basePower;
        currentHealth = maxHealth;
        currentMana = maxMana;
        gravity = (int) -(currentPower * 0.005f);
        chargeRate = 25;

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);
        powerBar.SetMinMaxPower(basePower, maxPower);
        powerBar.SetPower(basePower);
    }

    void Update() 
    {
        if (PauseMenu.GameIsPaused) return;
        flying = true;
        if (flying == true) gravity = 0;
        else gravity = (int) -(currentPower * 0.005f);

        ki.NaturalGain(50);
        jump.jump(1);

        chargeRate = 50;
        ki.Charging(chargeRate);

        int overDriveCost = 25;
        int overDriveGain = 40;
        int overDriveLoss = 4;
        overDrive2.overDrive(overDriveCost, overDriveGain, overDriveLoss);
        PlayerShootProjectiles_OnShoot(10);

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
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            projectileLocked = false;
        }
        if (Input.GetKey(KeyCode.Mouse0) && !PauseMenu.GameIsPaused && currentMana > cost)
        {
            ManaAdjust(-cost);
            chargeTime += 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && !PauseMenu.GameIsPaused && projectileLocked == false)
        {
            Spawner();
            projectileLocked = true;
        }
        if (Input.GetKey(KeyCode.Mouse0) && cost > currentMana && projectileLocked == false)
        {
            Spawner();
            projectileLocked = true;
        }

        void Spawner()
        {
            int modifier = (int) (chargeTime * currentPower * 0.00012f);
            Vector3 scale = new Vector3(modifier, modifier, modifier);
            pfBullet.localScale = scale;
            Transform bulletTransform = Instantiate(pfBullet, projectileStart.position, Quaternion.identity);

            Vector3 shootDir = (target.position - projectileStart.position).normalized;
            bulletTransform.GetComponent<Projectile>().Setup(shootDir, chargeTime);
            chargeTime = 1;
        }
    }
}

