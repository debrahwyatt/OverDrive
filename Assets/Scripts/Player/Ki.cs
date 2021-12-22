using UnityEngine;

public class Ki : MonoBehaviour
{
    private GameObject thisGameObject;
    private Player player;

    void Start()
    {
        thisGameObject = GameObject.Find("Player");
        player = thisGameObject.GetComponent<Player>();
    }

    private int frame = 0;
    public void NaturalGain(int gain)
    {
        // Constant Mana Regen
        frame++;
        if (frame == 1)
        {
            frame = 0;
            player.currentMana += gain;
        }
    }
    public void Recharging(int gain)
    {
        if (player.projectileLocked) return;
        
        if (Input.GetKey(KeyCode.E))
        {
            player.currentMana += gain;
            player.poweringUp = true;
        }
        else
        {
            player.poweringUp = false;
        }
    }
}
