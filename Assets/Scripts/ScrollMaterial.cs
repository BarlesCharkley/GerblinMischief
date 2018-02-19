using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMaterial : MonoBehaviour
{

    public float scrollSpeed = 0.5F;
    public Renderer rend;

    void Start ()
    {
        rend = GetComponent<Renderer>();
    }

    void Update ()
    {
        float scaleX = Mathf.Cos(Time.time) * 0.5F + 1;
        float scaleY = Mathf.Sin(Time.time) * 0.5F + 1;
        rend.material.SetTextureScale("_MainTex", new Vector2(scaleX, scaleY));
    }
}
