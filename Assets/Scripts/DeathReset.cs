using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathReset : MonoBehaviour
{

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
