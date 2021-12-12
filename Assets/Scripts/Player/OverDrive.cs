using UnityEngine;

public class OverDrive : MonoBehaviour
{
    private GameObject thisGameObject;
    private Player player;

    void Start()
    {
        thisGameObject = GameObject.Find("Player");
        player = thisGameObject.GetComponent<Player>();
    }

    public void overDrive(int cost, int gain, int reduction)
    {
        if (Input.GetKey(KeyCode.LeftShift) && player.currentMana >= cost) player.overDriveOn = true;
        else player.overDriveOn = false;

        if (player.currentPower + gain < player.maxPower && player.overDriveOn)
        {
            player.currentPower += gain;
            player.ManaAdjust(-cost);
        }
        if (player.currentPower + gain > player.maxPower && player.overDriveOn)
        {
            player.currentPower = player.maxPower;
            player.ManaAdjust((int)(-cost * 0.4f));
        }

        if (player.currentPower - reduction > player.basePower && !player.overDriveOn) player.currentPower -= reduction;

        player.SetPower(player.currentPower);
    }
}


