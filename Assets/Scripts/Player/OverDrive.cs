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


    /*    //Current speed to max ratio
        float manaAdjust = (staticMaxSpeed - currentVelocity) / maxSpeed;
            if (overDrive && currentMana <= 0) overDrive = false;
            if (Input.GetKey(KeyCode.LeftShift) && (currentMana > cost* 100 * 2 || overDrive))
            {
                if (overDrive == false && manaAdjust > 0)
                {
                    //mana dip is based on the speed percent to max
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    {
                        ManaAdjust((int)(-cost* 100 * manaAdjust));
                        currentVelocity = maxVelocity;
                    }
                }
                overDrive = true;
                ManaAdjust(-cost);
                OverDrive(10, 5);
            } 
            else
            {
            overDrive = false;
            OverDrive(10, 5);
            }
        }  */

