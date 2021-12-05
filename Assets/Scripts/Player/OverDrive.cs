using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverDrive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void overDrive(int cost)
    {
/*        //Current speed to max ratio
        float manaAdjust = (staticMaxSpeed - currentVelocity) / maxSpeed;
        if (overDrive && currentMana <= 0) overDrive = false;
        if (Input.GetKey(KeyCode.LeftShift) && (currentMana > cost * 100 * 2 || overDrive))
        {
            if (overDrive == false && manaAdjust > 0)
            {
                //mana dip is based on the speed percent to max
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    ManaAdjust((int)(-cost * 100 * manaAdjust));
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

        }*/
    }
}
