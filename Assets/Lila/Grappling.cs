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
    private Vector3 grapplePoint;
    private Rigidbody rb;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
            Debug.Log("StartedGrapple");
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, maxDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            //push towards point
            transform.position = hit.transform.position;
            Debug.Log("Grappled");
        }
        else
        {
            Debug.Log("NO GRAPPLE");
        }
    }

    void StopGrapple()
    {

    }

    void FollowTarget(Transform target, float distanceToStop, float speed)
    {
        var direction = Vector3.zero;
        if(Vector3.Distance(transform.position, target.position) > distanceToStop)
        {
            direction = target.position - transform.position;
            rb.AddRelativeForce(direction.normalized * speed, ForceMode.Force);
        }
    }
}
