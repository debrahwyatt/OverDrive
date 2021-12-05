using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpVelocity;
    public Controller controller;
    public Player player;

    // Update is called once per frame
    public void jump(int jumpCost = 0)
    {
        bool jumping = player.jumping;
        float velocityY = controller.velocityY;
        float jumpFactor = 0.0025f;
        float maxJumpHeight = player.currentPower * jumpFactor;

        if (velocityY <= 0) jumping = false;
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            jumping = true;
            jumpVelocity = Mathf.Sqrt(-2 * player.gravity * maxJumpHeight);
            velocityY = jumpVelocity;
        }
        if (Input.GetKeyUp(KeyCode.Space) && jumping)
        {
            if (!player.isGrounded && jumping == true)
            {
                jumping = false;

                jumpVelocity = Mathf.Sqrt(-2 * player.gravity);
                velocityY = jumpVelocity;
            }
        }
        controller.velocityY = velocityY;
        player.jumping = jumping;
        if (player.jumping == true) player.ManaAdjust(-jumpCost);
    }
}
