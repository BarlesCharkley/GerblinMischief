using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAndSpin : MonoBehaviour
{

    public float xDPS = 0f;
    public float yDPS = 15f;
    public float zDPS = 0f;
    public float bobAmplitude = 0.5f;
    public float bobFrequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        Spin();
        Bob();
    }

    void Spin()
    {
        transform.Rotate(new Vector3(Time.deltaTime * xDPS, 0f, 0f), Space.World);
        transform.Rotate(new Vector3(0f, Time.deltaTime * yDPS, 0f), Space.World);
        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * zDPS), Space.World);
    }

    void Bob()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * bobFrequency) * bobAmplitude;

        transform.position = tempPos;
    }
}
