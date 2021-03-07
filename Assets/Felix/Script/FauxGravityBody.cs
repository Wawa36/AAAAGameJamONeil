using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{

    public FauxGravity fauxGravityAttraction;
    private Rigidbody rigidbody;
    private Transform myTransform;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
        myTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        fauxGravityAttraction.Attract(myTransform);
    }
}
