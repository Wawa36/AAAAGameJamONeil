using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float speed = 1;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rigid.velocity = Vector3.zero;
        rigid.velocity = vertical * transform.forward.normalized * speed; //new Vector3(rigid.velocity.x, rigid.velocity.y, vertical*speed);
        this.transform.rotation = this.transform.rotation * Quaternion.AngleAxis(horizontal * 0.4f, transform.up);
    }
}
