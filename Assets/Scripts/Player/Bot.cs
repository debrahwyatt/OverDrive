using UnityEngine;

public class Bot : MonoBehaviour
{

    public int currentHealth;
    public int currentMana;
    public int currentPower;
    public int gravity; // system static?
    public int chargeRate;
    public int shootRate;
    public int basePower = 10000;
    public int maxPower = 20000;
    public int startSpeed;
    public int maxSpeed;

    private int maxHealth = 10000;
    private int maxMana = 10000;

    public float airControlPercent = 1f;
    public float projectileCharge = 1f;

    public bool flying = false;
    public bool isGrounded = true;
    public bool jumping = false;
    public bool overDriveOn = false;
    public bool poweringUp = false;
    public bool projectileLocked = false;


    // Initialize private class objects
    private GameObject thisGameObject;

    void Start()
    {
        thisGameObject = GameObject.Find("Bot");

        currentHealth = maxHealth;
        currentMana = maxMana;

    }

    void Update()
    {
        if (PauseMenu.GameIsPaused) return;
        if (flying == true) gravity = 0;
        else gravity = (int)-(currentPower * 0.005f);

        chargeRate = (int)(currentPower * 0.0025f);
        shootRate = (int)(currentPower * 0.0005f);

        startSpeed = (int)(0.003f * currentPower);
        maxSpeed = (int)(0.006f * currentPower);

        SetMana(currentMana);
        SetHealth(currentHealth);
        SetPower(currentPower);
    }

    // Slider Conditions
    public void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0);
            Debug.Log("BOT DESTROYED");
        }
    }
    public void SetMana(int mana)
    {
        currentMana = mana;
    }
    public void SetPower(int power)
    {
        currentPower = power;
    }

}

