using UnityEngine;

public class Player : MonoBehaviour {

    public int currentHealth;
    public int currentMana;
    public int currentPower = 10000;

    public HealthBar healthBar;
    private int maxHealth = 10000;

    public ManaBar manaBar;
    private int maxMana = 10000;

    public PowerBar powerBar;
    public int basePower = 10000;
    public int maxPower = 20000;

    public float airControlPercent = 1f;

    public Transform projectileStart;
    public Transform target;

    public bool jumping = false;
    public bool flying = false;
    public bool overDriving = false;
    public bool poweringUp = false;
    public bool isGrounded = true;

    [SerializeField] private Transform pfBullet;

    public Controller thisPlayer;
    public OverDrive overDrive;

    void Start() {

        currentHealth = maxHealth;
        currentMana = maxMana;
        currentPower = basePower;

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);
        powerBar.SetMinMaxPower(basePower, maxPower);
        powerBar.SetPower(basePower);
    }

    private int frames = 0;
    void Update() 
    {

        // Constant Mana Regen
        frames++;
        if (frames == 1)
        {
            frames = 0;
            ManaAdjust(2);
        }

        overDrive.overDrive(5, 5, 2);
        

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

