using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{
    
    float startSpeed;
    float staticMaxSpeed;
    float maxSpeed;
    float overDriveSpeed;

    static float gravity = -40;
    private float playerGravity = gravity;

    //[Range(0,1)]
    float airControlPercent = 1f;

    float turnSmoothTime = 0.2f;
    float fastSmooth = 0.3f;
    float slowSmooth = 0.8f;
    float turnSmoothVelocity;

    float speedSmoothTime = 1f;
    float speedSmoothVelocity;

    float currentVelocity;
    float velocityY;
    float maxVelocity;

    Animator animator;
    Transform cameraT;
    CharacterController controller;

    public Player thisPlayer;


    // Start is called before the first frame update
    void Start() {

        startSpeed = 2f;
        maxSpeed = 10f;

        animator = GetComponent<Animator> ();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController> ();
 
    }


    // Update is called once per frame
    void Update() 
    {

        startSpeed = 2f;
        maxSpeed = 10f;

        //Player Input
        Vector2 inputDir;
        if (thisPlayer.poweringUp) inputDir = new Vector2(0, 0);
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
        float animationSpeedPercent = ((thisPlayer.overDrive) ? currentVelocity / (maxSpeed * fastSmooth) : currentVelocity / startSpeed * fastSmooth);
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
        if(controller.isGrounded == true)
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
        velocityY += Time.deltaTime * playerGravity;
        velocity = transform.forward * currentVelocity + Vector3.up * velocityY;

        //Combines z/x velocity with y velocity
        controller.Move(velocity * Time.deltaTime);
        currentVelocity = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded) velocityY = 0;
        
    }



    float GetModifiedSmoothTime(float smoothTime) {
        if (controller.isGrounded) {
            return smoothTime;
        }

        if (airControlPercent == 0) {
            return float.MaxValue;
        }

        return smoothTime / airControlPercent;
    }
}

