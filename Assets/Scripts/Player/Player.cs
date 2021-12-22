using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public int currentHealth;
    public int currentMana;
    public int currentPower;
    public int gravity; // system static?
    public int rechargeRate;
    public int shootRate;
    public int startSpeed;
    public int maxSpeed;
    public int basePower = 10000;

    public int maxPower = 20000;
    private int maxHealth = 10000;
    private int maxMana = 10000;
    private int maxCharge = 11;

    public float currentCharge = 1f;
    public float airControlPercent = 1f;

    public bool flying = false;
    public bool isGrounded = true;
    public bool jumping = false;
    public bool overDriveOn = false;
    public bool poweringUp = false;
    public bool projectileLocked = true;

    // Initialize public class objects
    public HealthBar healthBar;
    public ManaBar manaBar;
    public PowerBar powerBar;
    public ChargeBar chargeBar;

    // Initialize private class objects
    private Ki ki;
    private Jump jump;
    private OverDrive overDrive;
    private Charge charge;
    private GameObject thisGameObject;
    private Teleport teleport;

    void Start() 
    {
        // QualitySettings.vSyncCount = 0; // Disable vSync
        Application.targetFrameRate = 60; // Set Framerate

        thisGameObject = GameObject.Find("Player");
        ki = thisGameObject.GetComponent<Ki>();
        jump = thisGameObject.GetComponent<Jump>();
        charge = thisGameObject.GetComponent<Charge>();
        teleport = thisGameObject.GetComponent<Teleport>();
        overDrive = thisGameObject.GetComponent<OverDrive>();

        currentHealth = maxHealth;
        currentMana = maxMana;
        currentCharge = 1f;

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);
        powerBar.SetMinMaxPower(basePower, maxPower);
        chargeBar.SetMaxCharge(maxCharge);

    }

    void Update() 
    {
        currentPower = powerBar.GetPower();

        if (PauseMenu.GameIsPaused) return;
        if (flying == true) gravity = 0;
        else gravity = (int) -(currentPower * 0.005f);

        rechargeRate = (int)(currentPower * 0.0025f);
        shootRate = (int)(currentPower * 0.0005f);

        startSpeed = (int) (0.003f * currentPower);
        maxSpeed = (int) (0.006f * currentPower);

        teleport.Tele(KeyCode.Mouse2);
        ki.NaturalGain(2);
        ki.Recharging(rechargeRate);
        if (!poweringUp)
        {
            charge.charge(shootRate);
            charge.shoot();
        }


        jump.jump();
        overDrive.overDrive();
        
        SetMana(currentMana);
        SetHealth(currentHealth);
        SetPower(currentPower);
        Debug.Log(currentCharge);
        SetCharge(currentCharge);
    }

    // Slider Conditions
    public void SetHealth(int health) 
    {
        currentHealth = health;
        healthBar.SetHealth(health);
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

            Application.Quit();
        }
    }
    public void SetMana(int mana)
    {
        if(mana >= maxMana) currentMana = maxMana;
        else currentMana = mana;
        manaBar.SetMana(mana);
    }
    public void SetPower(int power)
    {
        currentPower = power;
        powerBar.SetPower(power);
    }
    public void SetCharge(float charge)
    {
        if (charge >= maxCharge) currentCharge = charge = maxCharge;
        chargeBar.SetCharge(charge);
    }
}

