using UnityEngine;

public class OverDrive : MonoBehaviour
{
    public Controller controller;
    public Player player;

    public void overDrive(int cost, int powerGain, int powerReduction)
    {
        if (Input.GetKey(KeyCode.LeftShift) && player.currentMana > 50) player.overDriving = true;
        else player.overDriving = false;

        if (player.currentPower > player.basePower && !player.overDriving) player.currentPower -= powerReduction;
        if (player.currentPower < player.maxPower && player.overDriving) player.currentPower += powerGain;
        if (player.overDriving == true) player.ManaAdjust(-cost);

        player.SetPower(player.currentPower);
    }
}


