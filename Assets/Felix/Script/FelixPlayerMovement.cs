using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelixPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public Vector3 EulerAngleVelocity = new Vector3(0,5,0);
    private Vector3 moveDir;
    private Rigidbody rigibody;

    private void Start()
    {
        rigibody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        moveDir = this.transform.forward * vertical;
    }

    private void FixedUpdate()
    {
        rigibody.MovePosition(rigibody.position + (moveDir * moveSpeed * Time.deltaTime));
        float horizontal = Input.GetAxis("Horizontal");
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * horizontal * Time.fixedDeltaTime);
        rigibody.MoveRotation(rigibody.rotation * deltaRotation);
    }
}
