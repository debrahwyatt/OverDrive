using UnityEngine;

public class Ki : MonoBehaviour
{
    public Player player;
    int frame = 0;

    public void Charging(int gain)
    {
        if (Input.GetKey(KeyCode.E))
        {
            player.ManaAdjust(gain);
            player.poweringUp = true;
        }
        else
        {
            player.poweringUp = false;
        }
    }

    public void NaturalGain(int gain)
    {
        // Constant Mana Regen
        frame++;
        if (frame == 1)
        {
            frame = 0;
            player.ManaAdjust(2);
        }
    }
}
