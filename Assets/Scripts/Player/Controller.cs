using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static float gravity = -40;

    private float startSpeed;
    private float maxSpeed;

    private float airControlPercent = 1f;
    private float turnSmoothTime = 0.2f;
    private float fastSmooth = 0.3f;
    private float slowSmooth = 0.8f;
    private float turnSmoothVelocity;
    private float speedSmoothTime = 1f;
    private float speedSmoothVelocity;

    float currentVelocity;
    public float velocityY;
    float maxVelocity;

    public Player player;
    CharacterController characterController;
    Animator animator;
    Transform cameraT;
    public Jump jump;


    // Start is called before the first frame update
    void Start() {

        startSpeed = 2f;
        maxSpeed = 10f;

        animator = GetComponent<Animator> ();
        cameraT = Camera.main.transform;
        characterController = GetComponent<CharacterController> ();
 
    }


    // Update is called once per frame
    void Update() 
    {
        player.isGrounded = characterController.isGrounded;
        startSpeed = 2f;
        maxSpeed = 10f;
        jump.jump();

        //Player Input
        Vector2 inputDir;
        if (player.poweringUp) inputDir = new Vector2(0, 0);
        else
        {
            Vector2 input = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );
            inputDir = input.normalized;
        }

        //Call the movement script
        Move(inputDir);

        //Animator
        float animationSpeedPercent = ((player.overDrive) ? currentVelocity / (maxSpeed * fastSmooth) : currentVelocity / startSpeed * fastSmooth);
        if (Input.GetKey(KeyCode.LeftShift)) speedSmoothTime = fastSmooth; 
        else speedSmoothTime = slowSmooth;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }



    void Move(Vector2 inputDir) {

        // Orients controls based on camera perspective
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
                       
            transform.eulerAngles = Vector3.up *
            Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetRotation,
                ref turnSmoothVelocity,
                GetModifiedSmoothTime(turnSmoothTime)
            );
        }

        // calculating the current max velocity
        if(characterController.isGrounded == true)
        {
            maxVelocity = maxSpeed * inputDir.magnitude;
        }


        //Speeding up or slowing down
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D))
            speedSmoothTime = slowSmooth;
        else speedSmoothTime = fastSmooth;

        //Keeping the current velocity
        currentVelocity = Mathf.SmoothDamp(currentVelocity, maxVelocity, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        //Enables player gravity
        Vector3 velocity;
        velocityY += Time.deltaTime * gravity;
        velocity = transform.forward * currentVelocity + Vector3.up * velocityY;

        //Combines z/x velocity with y velocity
        characterController.Move(velocity * Time.deltaTime);
        currentVelocity = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

        if (characterController.isGrounded) velocityY = 0;
        
    }



    float GetModifiedSmoothTime(float smoothTime) {
        if (characterController.isGrounded) {
            return smoothTime;
        }

        if (airControlPercent == 0) {
            return float.MaxValue;
        }

        return smoothTime / airControlPercent;
    }
}

