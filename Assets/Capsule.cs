using UnityEngine;

public class Capsule : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    private int coins = 0;
    public float gravity = 9.81f; // Adjust this value as needed
    private float verticalVelocity = 0f;
    float horizontal;
    float vertical;
    bool jump;
    Vector3 forward;
    Vector3 right;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    public float additionalForce ;




    private CharacterController characterController;
    private Transform playerCameraTransform;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCameraTransform = GetComponentInChildren<Camera>().transform;
        additionalForce = 10f;
    }
    void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
         forward = playerCameraTransform.forward;
         right = playerCameraTransform.right;
        Jump();
    }

    void FixedUpdate()
    {
        // Rotate the character and camera with mouse input
        RotateWithMouse();

        // Move the character based on input relative to the camera direction
        MoveCharacter();
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");

        // Adjust the rotation based on mouse movement for both character and camera
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += mouseX * sensitivity;
        transform.rotation = Quaternion.Euler(rotation);

        // Rotate the camera around the player
        float cameraRotationY = playerCameraTransform.rotation.eulerAngles.y + mouseX * sensitivity;
        Vector3 offset = Quaternion.Euler(0, mouseX * sensitivity, 0) * (playerCameraTransform.position - transform.position);
        playerCameraTransform.position = transform.position + offset;
        playerCameraTransform.LookAt(transform.position);
    }

    void MoveCharacter()
    {
        forward.y = 0f; // Ignore vertical component
        right.y = 0f;

        Vector3 moveDirection = (forward.normalized * vertical + right.normalized * horizontal).normalized;

        // Check if the character is grounded
        if (jump)
        {
            moveDirection.y = jumpHeight;
        }
        else
        {
            moveDirection.y = -9.81f * Time.fixedDeltaTime*20;
        }


        // Move the character using CharacterController
        characterController.Move(moveDirection * speed * Time.fixedDeltaTime);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the collision is with a ball
        if (hit.gameObject.name == "Sphere")
        {
            // Calculate the direction from the capsule to the ball
            Vector3 directionToBall = hit.gameObject.transform.position - transform.position;

            // Apply additional force to the ball in the direction from the capsule to the ball
            Rigidbody ballRigidbody = hit.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {

                // Apply the force instantly using ForceMode.Impulse
                ballRigidbody.AddForce(directionToBall.normalized * additionalForce, ForceMode.Impulse);
            }
        }
    }


    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            Debug.Log(++coins);
        }
    }
}
