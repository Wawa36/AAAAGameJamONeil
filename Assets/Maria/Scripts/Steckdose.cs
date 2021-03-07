using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steckdose : MonoBehaviour
{
    bool isAvailable = true;
    bool playerIsNear = false;
    bool isClickedOn = false;
    WireManager wireManager;

    private void Start()
    {
        wireManager = WireManager.instance;
    }

    void Update()
    {

            if (playerIsNear && isClickedOn && isAvailable)
            {
            isAvailable = false;
            if (wireManager.currentWire.wireStartPoint == Vector3.zero)
            {
                wireManager.currentWire.wireStartPoint = this.transform.position;
                print("A");
            }
            else
            {
                print("B");
                wireManager.currentWire.wireEndPoint = this.transform.position;
                wireManager.currentWire.GetComponent<Kabel>().MakeTheConnection();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsNear = false;
        }
    }
    private void OnMouseDown()
    {
        isClickedOn = true;
    }
    private void OnMouseUp()
    {
        isClickedOn = false;
    }
}
