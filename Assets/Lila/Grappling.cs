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

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                Debug.Log("normal");
                break;
            case State.HookshotFlyingPlayer:
                Debug.Log("flying");
                HandleHookshotMovement();
                break;
        }
    }

    private void HandleHookshotStart()
    {
        if (TestInputDownHookshot())
        {
            Debug.Log("CLICKED");

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

        float hookshotSpeedMin = 10f;
        float hookshotSpeedMax = 20f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 5f;

        GetComponent<Rigidbody>().AddForce(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier);
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
