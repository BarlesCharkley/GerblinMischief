using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoller : MonoBehaviour
{
    [SerializeField] private KeyCode ragdollTestKey;
    [SerializeField] private float testDelay;
    [SerializeField] private Vector3 testForce;
    [Space]
    [SerializeField] private float mass = 0.1f;           // Mass of each bone
    [Space]
    [SerializeField] private Component[] boneRig;		// Contains the ragdoll bones

    private void Start ()
    {
        boneRig = gameObject.GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(ragdollTestKey))
        {
            StartCoroutine(EnableRagdoll(testDelay, testForce));
        }
    }

    public void DisableRagdoll()
    {
        foreach (Component ragdoll in boneRig)
        {
            if ((ragdoll.GetComponent<Collider>() != null) && ragdoll.GetComponent<Collider>() != this.GetComponent<Collider>())
            {
                ragdoll.GetComponent<Collider>().enabled = false;
                ragdoll.GetComponent<Rigidbody>().isKinematic = true;
                ragdoll.GetComponent<Rigidbody>().mass = 0.01f;
            }
        }
        GetComponent<Collider>().enabled = true;
    }

    public IEnumerator EnableRagdoll(float delay, Vector3 force)
    {
        yield return new WaitForSeconds(delay);

        foreach (Component ragdoll in boneRig)
        {
            if (ragdoll.GetComponent<Collider>() != null)
            ragdoll.GetComponent<Collider>().enabled = true;
            ragdoll.GetComponent<Rigidbody>().isKinematic = false;
            ragdoll.GetComponent<Rigidbody>().mass = mass;

            if (force.magnitude > 0)
                ragdoll.GetComponent<Rigidbody>().AddForce(force * UnityEngine.Random.value);
        }

        GetComponent<Animator>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;

        Debug.Log("ragdolled!");
    }
}
