using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    //Rigidbody rigid;
    [SerializeField] Image img;
    [SerializeField] float jetpackForce = 2;
    [Range(0, 1)] float fuel = 1;
    float sizeDeltaX = 0;
    float sizeDeltaY = 0;

    private CharacterController characterController;

    void Start()
    {
        //rigid = this.GetComponent<Rigidbody>();
        sizeDeltaX = img.GetComponent<RectTransform>().sizeDelta.x;
        sizeDeltaY = img.GetComponent<RectTransform>().sizeDelta.y;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        JetpackControl();
    }

    void JetpackControl()
    {
        float jetpackPower = Input.GetAxis("Jetpack");
        if (jetpackPower != 0)
        {
            if (fuel > 0)
            {
                Debug.Log("YAHHAS");
                characterController.Move(transform.up * jetpackForce * Time.deltaTime);
                //rigid.AddForce(transform.up.normalized * jetpackPower * Random.Range(0, jetpackForce) * Random.Range(0f, 2f));
                fuel -= 0.003f;
            }
        }
        ReloadFuel();
        UpdateFuel();
    }
    void UpdateFuel()
    {
        img.color = Remap(fuel, 1, 0, Color.green, Color.red);
        img.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDeltaX, Remap(fuel, 1, 0, sizeDeltaY, 0));
    }
    void ReloadFuel()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            fuel = 1;
        }
    }
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    public Color Remap(float value, float from1, float to1, Color from2, Color to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
