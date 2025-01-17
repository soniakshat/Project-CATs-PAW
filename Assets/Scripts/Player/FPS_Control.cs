using System.Collections;

using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class FPS_Control : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    [SerializeField] private GameObject _GOP;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    
    private bool canMove = true;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        Jump(movementDirectionY);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void Jump(float movementDirectionY)
    {
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("OuterBounds"))
        {
            if (_GOP.activeInHierarchy == false)
            {

                StartCoroutine(YouLoseAudioPlayer());

                // this.gameObject.GetComponentInParent<PlayerTankProjectileSpawner>().enabled = false;
                // this.gameObject.GetComponentInParent<PlayerTankControl>().enabled = false;
                // GameObject.FindGameObjectWithTag("OnScreenControls").SetActive(false);
            }
        }
    }

    IEnumerator YouLoseAudioPlayer()
    {
        _GOP.SetActive(true);
        _GOP.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(_GOP.GetComponent<AudioSource>().clip.length);

        // UnLock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(PlayerExplode());
    }



    [SerializeField] private GameObject _explosionParticleSystem;

    IEnumerator PlayerExplode()
    {
        this.gameObject.SetActive(false);
        Instantiate(_explosionParticleSystem, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0);
    }
}