using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public float gravityConst;
    public GameObject player;

    void FixedUpdate()
    {
        Vector3 gravityDirection = player.transform.position - new Vector3(player.transform.position.x, 0, 0);
        Physics.gravity = gravityDirection.normalized * gravityConst;

        //player.transform.up = -gravityDirection.normalized;
       
    }
}
