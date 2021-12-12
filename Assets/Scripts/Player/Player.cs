using UnityEngine;

public class Player : MonoBehaviour {

    public int currentHealth;
    public int currentMana;
    public int currentPower;
    public int gravity; // system static?
    public int chargeRate;

    public int basePower = 10000;
    public int maxPower = 20000;
    private int maxHealth = 10000;
    private int maxMana = 10000;

    public float airControlPercent = 1f;
    public float projectileChargeTime = 1f;

    public bool flying = true;
    public bool jumping = false;
    public bool overDriveOn = false;
    public bool poweringUp = false;
    public bool isGrounded = true;
    public bool projectileLocked = false;

    // Initialize public class objects
    public HealthBar healthBar;
    public ManaBar manaBar;
    public PowerBar powerBar;
    public Transform projectileStart;
    public Transform target;

    // Initialize private class objects
    private GameObject thisGameObject;
    private Ki ki;
    private Jump jump;
    private OverDrive overDrive;
    private PlayerShoot playerShoot;

    void Start() 
    {
        QualitySettings.vSyncCount = 0; // Disable vSync
        Application.targetFrameRate = 60; // Set Framerate

        thisGameObject = GameObject.Find("Player");
        ki = thisGameObject.GetComponent<Ki>();
        jump = thisGameObject.GetComponent<Jump>();
        overDrive = thisGameObject.GetComponent<OverDrive>();
        playerShoot = thisGameObject.GetComponent<PlayerShoot>();

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
        overDrive.overDrive(overDriveCost, overDriveGain, overDriveLoss);

        playerShoot.Shoot(10);


        if (Input.GetKeyUp(KeyCode.G))
        {
            TakeDamage(100);
        }

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

}

