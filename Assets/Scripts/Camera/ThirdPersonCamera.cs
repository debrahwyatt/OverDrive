using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour 
{
    public bool lockCursor;

    public float mouseSensativity = 10;
    public float dstFromTarget = 3;

    public float rotationSmoothTime = 0.12f;
    // public Vector2 pitchMinMax = new Vector2(-40, 85);

    public Transform target;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    void Start() 
    {
        if(lockCursor) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void LateUpdate() 
    {
        yaw += Input.GetAxis ("Mouse X") * mouseSensativity;
        pitch -= Input.GetAxis ("Mouse Y") * mouseSensativity;
        // pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
    }
}
