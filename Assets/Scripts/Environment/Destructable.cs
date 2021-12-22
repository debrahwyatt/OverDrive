using UnityEngine;
using System.Text.RegularExpressions;

public class Destructable : MonoBehaviour
{
    public int currentHealth;

    void Start()
    {
        gameObject.tag = "Destructable";
        if (gameObject.name.Contains("bush")) currentHealth = 5;
        if (gameObject.name.Contains("tree")) currentHealth = 10;
        if (gameObject.name.Contains("building")) currentHealth = 10000;
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused) return;
        SetHealth(currentHealth);
    }

    // Slider Conditions
    public void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0);
        }
    }
}

