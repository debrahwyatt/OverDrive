using UnityEngine;

public class Controller : MonoBehaviour
{
    public float currentVelocity;
    public float velocityY;

    private float turnSmoothTime = 0.05f;
    private float fastSmooth = 0.3f;
    private float slowSmooth = 0.8f;
    private float turnSmoothVelocity;
    private float speedSmoothTime = 1f;
    private float speedSmoothVelocity;
    private float maxVelocity;

    private CharacterController characterController;
    private Animator animator;
    private Transform cameraT;

    private GameObject thisGameObject;
    private Player player;
    private Vector3 input;

    // Start is called before the first frame update
    void Start() 
    {
        cameraT = Camera.main.transform;

        thisGameObject = GameObject.Find("Player");
        player = thisGameObject.GetComponent<Player>();
        animator = thisGameObject.GetComponent<Animator>();
        characterController = thisGameObject.GetComponent<CharacterController> ();
    }


    // Update is called once per frame
    void Update() 
    {
        if (PauseMenu.GameIsPaused) return;
        player.isGrounded = characterController.isGrounded;

        //Player Input
        Vector3 inputDir;
        if (player.poweringUp) inputDir = new Vector3(0, 0, 0);
        else
        {
            input = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            Input.GetAxisRaw("Space") - Input.GetAxisRaw("CTRL")
            );
            inputDir = input.normalized;

        }

        //Call the movement script
        Move(inputDir);

        //Animator
        float animationSpeedPercent = ((player.overDriveOn) ? currentVelocity / (player.maxSpeed * fastSmooth) : currentVelocity / player.startSpeed * fastSmooth);
        speedSmoothTime = slowSmooth;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    public Vector3 getInput()
    {
        return input;
    }

    void Move(Vector3 inputDir) {

        // Orients controls based on camera perspective
        if (inputDir != Vector3.zero)
        {
            //Handels left to right rotation of the character
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up *
            Mathf.SmoothDampAngle(
                transform.eulerAngles.y, //current position
                targetRotation, //target
                ref turnSmoothVelocity, //current velocity get modified by this function
                GetModifiedSmoothTime(turnSmoothTime) // Smooth time
            );
        }

        // calculating the current max velocity
        if (characterController.isGrounded == true || player.flying)
        {

        }


        //Acceleration
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.Space) ||
            Input.GetKey(KeyCode.LeftControl))
        {
            speedSmoothTime = slowSmooth;
        }
        else speedSmoothTime = fastSmooth;

        if (player.overDriveOn) currentVelocity = maxVelocity;

         //Keeping the current velocity
         currentVelocity = Mathf.SmoothDamp(currentVelocity, maxVelocity, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));


        //Enables player gravity
        //if (player.flying) velocityY += Time.deltaTime * player.gravity;
        // if(player.flying) velocityY += Time.deltaTime * inputDir.z ;
        // Vector3 newVelocity = input.magnitude;

        // Debug.Log(inputDir);
        Vector3 velocity =
            transform.forward * currentVelocity + 
            transform.up * currentVelocity * inputDir[2] - // +up & -down
            Vector3.up * 0;

        Vector3 velocity2 = inputDir * currentVelocity;

        maxVelocity = player.maxSpeed * (inputDir.magnitude);
        //Combines z/x velocity with y velocity
        characterController.Move(velocity * Time.deltaTime);
        currentVelocity = new Vector3(characterController.velocity.x, characterController.velocity.z).magnitude;
        //currentVelocity = new Vector3(characterController.velocity.x, characterController.velocity.z, characterController.velocity.y).magnitude;

        if (characterController.isGrounded) velocityY = 0;
        
    }

    float GetModifiedSmoothTime(float smoothTime) {
        if (characterController.isGrounded) {
            return smoothTime;
        }

        if (player.airControlPercent == 0) {
            return float.MaxValue;
        }

        return smoothTime / player.airControlPercent;
    }
}

