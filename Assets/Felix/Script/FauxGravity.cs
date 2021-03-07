using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravity : MonoBehaviour
{
    public float gravity = -10;
    public float roationSmoothness;
    public void Attract(Transform body)
    {
        Vector3 gravityDown = (body.position - new Vector3(body.position.x, 0, 0)).normalized;
        Vector3 gravityUp = gravityDown * -1;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
    }

    public void Orientate(Transform body)
    {
        Vector3 gravityDown = (body.position - new Vector3(body.position.x, 0, 0)).normalized;
        Vector3 gravityUp = gravityDown * -1;
        Vector3 bodyUp = body.up;


        Quaternion targetRotation = Quaternion.FromToRotation(body.up, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, roationSmoothness * Time.deltaTime);
    }
}
