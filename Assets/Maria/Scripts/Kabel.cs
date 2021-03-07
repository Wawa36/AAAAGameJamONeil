using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kabel : MonoBehaviour
{
    public Vector3 wireStartPoint;
    public Vector3 wireEndPoint;

    public void MakeTheConnection()
    {
        if (wireStartPoint!=null&&wireEndPoint!=null)
        {
            print("making a wire");
            GetComponent<LineRenderer>().SetPositions(new Vector3[2] { wireStartPoint , wireEndPoint });
        }
        else
        {
            print("something went wrong here");
        }
    }
}
