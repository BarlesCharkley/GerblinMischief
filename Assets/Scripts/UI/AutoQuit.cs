using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoQuit : MonoBehaviour
{
    private void OnEnable()
    {
        Application.Quit();
        Debug.Log("Auto quitted baybee");
    }
}
