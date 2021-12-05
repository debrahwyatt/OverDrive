using UnityEngine;

public class OverDrive : MonoBehaviour
{
    public Controller controller;
    public Player player;

    public void overDrive(int cost, int gain, int reduction)
    {
        if (Input.GetKey(KeyCode.LeftShift) && player.currentMana > cost) player.overDrive = true;
        else player.overDrive = false;

        if (player.currentPower + gain < player.maxPower && player.overDrive)
        {
            player.currentPower += gain;
            player.ManaAdjust(-cost);
        }
        if (player.currentPower + gain > player.maxPower && player.overDrive)
        {
            player.currentPower = player.maxPower;
            player.ManaAdjust((int)(-cost * 0.4f));
        }

        if (player.currentPower - reduction > player.basePower && !player.overDrive) player.currentPower -= reduction;

        player.SetPower(player.currentPower);
    }
}


