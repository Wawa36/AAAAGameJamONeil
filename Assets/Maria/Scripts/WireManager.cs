using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    //List<Kabel> currentWires = new List<Kabel>();
    [SerializeField] GameObject wire;
    [HideInInspector] public Kabel currentWire;
    public static WireManager instance;
    void Start()
    {
        instance = this;
        InstantiateTheWire();
    }

    void Update()
    {

    }
    public void InstantiateTheWire()
    {
        currentWire = Instantiate(wire, Vector3.zero, Quaternion.identity).GetComponent<Kabel>();
    }
}
