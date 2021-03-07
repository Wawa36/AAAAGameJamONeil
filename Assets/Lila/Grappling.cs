using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1f;

    //private CharacterController characterController;
    private float cameraVerticalAngle;
    private float characterVelocityY;
    private Vector3 characterVelocityMomentum;
    private Camera playerCamera;
    private State state;
    private Vector3 hookshotPosition;
    private Ray ray;

    //felix
    public float moveSpeed = 10;
    public Vector3 EulerAngleVelocity = new Vector3(0, 5, 0);
    private Vector3 moveDir;
    private Rigidbody rigibody;

    private void Start()
    {
        rigibody = this.GetComponent<Rigidbody>();
    }

    private enum State
    {
        Normal,
        HookshotFlyingPlayer,
    }

    private void Awake()
    {
        //characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        state = State.Normal;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                HandleCharacterLook();
                //Debug.Log("normal");
                break;
            case State.HookshotFlyingPlayer:
                Debug.Log("flying");
                HandleCharacterLook();
                break;
        }
    }


    private void FixedUpdate()
    {
        switch (state)
        {
            default:
            case State.Normal:
                Movement();
                break;
            case State.HookshotFlyingPlayer:
                HandleHookshotMovement();
                break;
        }

    }

    private void HandleCharacterLook()
    {
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(new Vector3(0f, lookX * mouseSensitivity, 0f), Space.Self);
        cameraVerticalAngle -= lookY * mouseSensitivity;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }

    private void Movement()
    {
        float vertical = Input.GetAxis("Vertical");
        moveDir = this.transform.forward * vertical;

        rigibody.MovePosition(rigibody.position + (moveDir * moveSpeed * Time.deltaTime));
        float horizontal = Input.GetAxis("Horizontal");
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * horizontal * Time.fixedDeltaTime);
        rigibody.MoveRotation(rigibody.rotation * deltaRotation);
    }

    private void HandleHookshotStart()
    {
        if (TestInputDownHookshot())
        {
            Debug.Log("CLICKED");


            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit))
            {
                hookshotPosition = raycastHit.point;
                state = State.HookshotFlyingPlayer;
            }
        }
    }

    private bool TestInputDownHookshot()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    private void HandleHookshotMovement()
    {
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        float hookshotSpeedMin = 0.1f;
        float hookshotSpeedMax = 1f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 1f;

        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + hookshotDir * hookshotSpeed * hookshotSpeedMultiplier);
        //characterController.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, hookshotPosition, .5f);


        float reachedHookshotPositionDistance = 3f;
        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            StopHookshot();
        }

        if (TestInputDownHookshot())
        {
            Debug.Log("CLICKED");
            StopHookshot();
        }

        if (TestInputJump())
        {
            float momentumExtraSpeed = 7f;
            characterVelocityMomentum = hookshotDir * hookshotSpeed * momentumExtraSpeed;
            float jumpSpeed = 40f;
            characterVelocityMomentum += Vector3.up * jumpSpeed;
            StopHookshot();
        }
    }

    private void StopHookshot()
    {
        Debug.Log("STOP");
        state = State.Normal;
        ResetGravityEffect();
    }

    private void ResetGravityEffect()
    {
        characterVelocityY = 0f;
    }

    private bool TestInputJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
