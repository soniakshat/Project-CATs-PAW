using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstPersonControllerAndroid : MonoBehaviour
{
    [SerializeField] private GameObject _GOP;
    [SerializeField] private GameObject _PlayerControlCanvas;
    private float gravity = 10;
    private bool _collisionEntered = false;


    // References
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Slider _PlayerHealthSlider;
    [SerializeField] private TextMeshProUGUI _PlayerHealthValue;


    // Player settings
    private float cameraSensitivity = 50.0f;
    private Transform PlayerTransform;
    [SerializeField] private float moveSpeed = 500;
    private float originalMovespeed;
    [SerializeField] private float moveInputDeadZone;
    Vector2 movementDirection;
    private float _playerHealth = 100.0f;
    [SerializeField] private float _EnemyDamage = 10.0f;

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float lookXLimit = 45.0f;
    private float cameraPitch;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;

    private bool isRunning = false;
    [SerializeField] private Toggle isRunningToggle;

    private void Awake()
    {
        _playerHealth = 100.0f;
        _PlayerHealthSlider.value = _playerHealth;
        _PlayerHealthValue.text = _playerHealth.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraSensitivity = PlayerPrefs.GetInt("CameraSensitivity", 50);
        // print("Camera Sensitivity: " + cameraSensitivity);

        originalMovespeed = moveSpeed;
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }


    // Update is called once per frame
    void Update()
    {
        // Handles input
        GetTouchInput();

        // Check if is running
        isRunning = isRunningToggle.isOn;
        if (isRunning)
        {
            //isRunningToggle.GetComponentInChildren<Text>().text = $"RunningSpeed: {moveSpeed}";
            // print($"RunningSpeed: {moveSpeed}");
        }
        else
        {
            //isRunningToggle.GetComponentInChildren<Text>().text = $"WalkingSpeed: {moveSpeed}";
            // print($"WalkingSpeed: {moveSpeed}");
        }
        UpdateHealthSlider();
        if (_playerHealth <= 0)
        {
            StartCoroutine(YouLoseAudioPlayer());
        }
    }

    private void FixedUpdate()
    {
        // Move According to Inputs
        if (rightFingerId != -1)
        {
            // Ony look around if the right finger is being tracked
            // Debug.Log("Rotating");
            LookAround();
        }
        if (leftFingerId != -1)
        {
            // Ony move if the left finger is being tracked
            // Debug.Log("Moving");
            Move();
        }
    }

    void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        // Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        // Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId)
                    {

                        // calculating the position delta from the start position
                        moveInput = t.position - moveTouchStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -lookXLimit, lookXLimit);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {
        Run();
        // Don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        // Multiply the normalized direction by the speed
        movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
        Vector3 _3DMovement = new Vector3(movementDirection.x, movementDirection.y, 50f);
        // Move relatively to the local transform's direction
        //characterController.SimpleMove(transform.right * movementDirection.x + transform.forward * movementDirection.y);
        characterController.SimpleMove(transform.right * _3DMovement.x + transform.forward * _3DMovement.y);
    }

    public void Run()
    {
        if (isRunningToggle.isOn)
        {
            moveSpeed = originalMovespeed + 200;
        }
        else
        {
            moveSpeed = originalMovespeed;
        }
    }
    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     Rigidbody body = hit.collider.attachedRigidbody;
    // }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Enemy") && !_collisionEntered)
        {
            _collisionEntered = true;
            StartCoroutine(DecreaseHealth());
        }
    }
    private void OnCollisionExit(Collision other)
    {
        _collisionEntered = false;
    }
    IEnumerator DecreaseHealth()
    {
        while (_collisionEntered)
        {
            _playerHealth -= _EnemyDamage;
            // Debug.Log("Player Health: " + _playerHealth);
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateHealthSlider()
    {
        _PlayerHealthSlider.maxValue = 100f;
        _PlayerHealthSlider.value = _playerHealth;
        _PlayerHealthValue.text = _playerHealth.ToString();
    }

    IEnumerator YouLoseAudioPlayer()
    {
        _GOP.SetActive(true);
        _PlayerControlCanvas.SetActive(false);
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