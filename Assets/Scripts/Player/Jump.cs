using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpVelocity;
    private int gravity = -40;
    private int jumpCost = 1;
    public Controller controller;
    public Player player;

    // Update is called once per frame
    public void jump()
    {
        bool jumping = player.jumping;
        float velocityY = controller.velocityY;
        float jumpFactor = 0.0001f;
        float maxJumpHeight = player.currentPower * jumpFactor;

        if (velocityY <= 0) jumping = false;
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded
)
        {
            jumping = true;
            player.ManaAdjust(-jumpCost);

            jumpVelocity = Mathf.Sqrt(-2 * gravity * maxJumpHeight);
            velocityY = jumpVelocity;
        }
        if (Input.GetKeyUp(KeyCode.Space) && jumping)
        {
            if (!player.isGrounded && jumping == true)
            {
                jumping = false;
                jumpVelocity = Mathf.Sqrt(-2 * gravity);
                velocityY = jumpVelocity;
            }
        }
        controller.velocityY = velocityY;
        player.jumping = jumping;
    }
}
