using UnityEngine;

public class OverDrive : MonoBehaviour
{
    private GameObject thisGameObject;
    private Player player;

    int cost = 25;
    int gain = 40;
    int reduction = 4;

    void Start()
    {
        thisGameObject = GameObject.Find("Player");
        player = thisGameObject.GetComponent<Player>();
    }

    public void overDrive()
    {
        if (Input.GetKey(KeyCode.LeftShift) && player.currentMana >= cost) player.overDriveOn = true;
        else player.overDriveOn = false;

        if (player.currentPower + gain < player.maxPower && player.overDriveOn)
        {
            player.currentPower += gain;
            player.currentMana -= cost;
        }
        if (player.currentPower + gain > player.maxPower && player.overDriveOn)
        {
            player.currentPower = player.maxPower;
            player.currentMana -= (int)(cost * 0.4f);
        }

        if (player.currentPower - reduction > player.basePower && !player.overDriveOn) player.currentPower -= reduction;
    }
}


