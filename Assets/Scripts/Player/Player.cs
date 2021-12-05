using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public HealthBar healthBar;
    public int maxHealth = 10000;
    public int currentHealth;

    public ManaBar manaBar;
    public int maxMana = 10000;
    public int currentMana;

    public PowerBar powerBar;
    public int basePower = 10000;
    public int currentPower = 10000;
    public int maxPower = 10000;

    public Transform projectileStart;
    public Transform target;

    public bool jumpingUp = false;
    public bool flying = false;
    public bool overDrive = false;
    public bool poweringUp = false;

    [SerializeField] private Transform pfBullet;

    public Controller thisPlayer;

    void Start() {

        currentHealth = maxHealth;
        currentMana = maxMana;

        currentPower = basePower;

        //overDrivePower = basePower * 2;

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);
        powerBar.SetMaxPower(maxPower);
        powerBar.SetPower(basePower);


    }

    private int frames = 0;
    void Update() 
    {
        // Gameplay Methods
        // JumpUp(0);
        // OverDrive(5);
        // ChargeUp(10);
        // Fly(1);

        //if (overDrive) OverDrive(5, 5);
        // Constant Mana Regen
        frames++;
        if (frames == 1)
        {
            frames = 0;
            ManaAdjust(2);
        }
        powerBar.SetPower(basePower);

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
    }

    public void OverDrive(int powerGain, int powerReduction)
    {
        //never add more than the max
        if (currentPower < maxPower && overDrive) currentPower += powerGain;
        if (currentPower > basePower && !overDrive) currentPower -= powerReduction;
        powerBar.SetPower(currentPower);
        Debug.Log(currentPower);
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


    public bool GetOverDrive()
    {
        return overDrive;
    }

}

