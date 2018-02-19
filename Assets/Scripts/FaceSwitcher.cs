using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSwitcher : MonoBehaviour
{ 
    [SerializeField] private KeyCode nextEyes = KeyCode.E;
    [SerializeField] private KeyCode prevEyes = KeyCode.Alpha3;
    [Space]
    [SerializeField] private KeyCode nextMouth = KeyCode.Q;
    [SerializeField] private KeyCode prevMouth = KeyCode.Alpha1;
    [Space]
    [SerializeField] private Material eyeMaterial;
    [SerializeField] private Material mouthMaterial;
    [Space]
    [SerializeField] private Texture[] eyeTextures;
    [SerializeField] private Texture[] mouthTextures;

    private int currentEyes;
    private int currentMouth;

    void Start ()
    {
        SetEyes(0);
        SetMouth(0);
	}
	
	void Update ()
    {
        #region Test Inputs
        if (Input.GetKeyDown(nextEyes) && currentEyes < eyeTextures.Length)
        {
            SetEyes(currentEyes + 1);
        }

        if (Input.GetKeyDown(prevEyes) && currentEyes >= 0)
        {
            SetEyes(currentEyes - 1);
        }

        if (Input.GetKeyDown(nextMouth) && currentMouth < mouthTextures.Length)
        {
            SetMouth(currentMouth + 1);
        }

        if (Input.GetKeyDown(prevMouth) && currentMouth >= 0)
        {
            SetMouth(currentMouth - 1);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SetEyes(0);
            SetMouth(0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetEyes(1);
            SetMouth(1);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetEyes(2);
            SetMouth(2);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetEyes(3);
            SetMouth(3);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SetEyes(3);
            SetMouth(4);
        }
    }

    void SetEyes(int eyes)
    {
        eyeMaterial.mainTexture = eyeTextures[eyes];
        currentEyes = eyes;
        Debug.Log("Eyes " + currentEyes);
    }

    void SetMouth(int mouth)
    {
        mouthMaterial.mainTexture = mouthTextures[mouth];
        currentMouth = mouth;
        Debug.Log("Mouth " + currentMouth);
    }

    private void OnApplicationQuit()
    {
        SetEyes(0);
        SetMouth(0);
    }
}
