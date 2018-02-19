//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Klak.Motion;

//public class CinematicLeaf : MonoBehaviour
//{
//    [HideInInspector] public bool isGathering;
//    private CinematicsHandler cinematics;

//    void Awake()
//    {
//        cinematics = FindObjectOfType<CinematicsHandler>();
//        GetComponent<SmoothFollow>()._target = cinematics.teaTarget;
//        transform.parent = cinematics.teaTarget;
//        cinematics.leaves.Add(gameObject);
//    }

//    public void GatherAnim()
//    {
//        GetComponent<Animator>().SetTrigger("Gather");
//    }
//}
