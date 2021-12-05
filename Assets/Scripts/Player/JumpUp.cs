using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    void Jumping(int cost)
    {
        float jumpVelocity;

        if (velocityY <= 0) jumpingUp = false;
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            jumpingUp = true;
            thisPlayer.ManaAdjust(-cost);

            jumpVelocity = Mathf.Sqrt(-2 * playerGravity * maxJumpHeight);
            velocityY = jumpVelocity;
        }
        if (Input.GetKeyUp(KeyCode.Space) && jumpingUp)
        {
            if (!controller.isGrounded && jumpingUp == true)
            {
                jumpingUp = false;
                jumpVelocity = Mathf.Sqrt(-2 * playerGravity);
                velocityY = jumpVelocity;
            }
        }
    }*/
}
