using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMerged : MonoBehaviour
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
    public Vector3 EulerAngleVelocity = new Vector3(0, 50, 0);
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
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                Movement();
                break;
            case State.HookshotFlyingPlayer:
                HandleHookshotMovement();
                break;
        }
    }
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        moveDir = this.transform.forward * vertical;
    }

    private void Movement()
    {
        rigibody.MovePosition(rigibody.position + (moveDir * moveSpeed * Time.deltaTime));
        float horizontal = Input.GetAxis("Horizontal");
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * horizontal * Time.fixedDeltaTime);
        rigibody.MoveRotation(rigibody.rotation * deltaRotation);
    }

    private void HandleHookshotStart()
    {
        if (TestInputDownHookshot())
        {
            ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                hookshotPosition = raycastHit.point;
                state = State.HookshotFlyingPlayer;
            }
        }
    }

    private bool TestInputDownHookshot()
    {
        return Input.GetMouseButtonDown(0);
    }

    private void HandleHookshotMovement()
    {
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        float hookshotSpeedMin = 1;
        float hookshotSpeedMax = 5;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 1;

        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + hookshotDir * hookshotSpeed * hookshotSpeedMultiplier);


        float reachedHookshotPositionDistance = 1f;
        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("stopped");

            StopHookshot();
        }

        if (TestInputDownHookshot())
        {
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
