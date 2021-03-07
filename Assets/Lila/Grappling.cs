using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    private Transform camera;
    [SerializeField]
    private float maxDistance = 100f;
    [SerializeField]
    private LayerMask grappleLayer;
    private Rigidbody rb;

    private Ray ray;

    private State state;
    private Vector3 hookshotPosition;
    private float reachedHookshotPositionDistance;

    private enum State
    {
        Normal,
        HookshotFlyingPlayer
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                break;
            case State.HookshotFlyingPlayer:
                HandleHookshotMovement();
                break;

        }
        if (Input.GetMouseButtonDown(0))
        {
            HandleHookshotStart();
        }
    }

    void HandleHookshotStart()
    {
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, maxDistance, grappleLayer))
        {
            hookshotPosition = hit.point;
            state = State.HookshotFlyingPlayer;
        }
    }

    void HandleHookshotMovement()
    {
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
        float speedMultiplier = 2f;

        if(Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            state = State.Normal;
        }
    }

}
