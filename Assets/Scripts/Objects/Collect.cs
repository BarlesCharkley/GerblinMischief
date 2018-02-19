//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Collect : MonoBehaviour
//{
//    [SerializeField] public float baseLeafFollowSpeed = 9f;
//    [SerializeField] public float followIncrement = 2f;
//    [SerializeField] public float collectDistance = 7f;
//    [SerializeField] public Transform inventoryTarget;
//    [SerializeField] public Transform collectedParent;
//    [SerializeField] public string collectedLayer = "Collected Goodies";
//    [SerializeField] private int goodieGoal;
//    [SerializeField] private bool autoGoal = true;

//    public List<GameObject> collectedGoodies;
//    private SnailMove snailMove;
//    private CinematicsHandler cinematics;

//    private void Start()
//    {
//        snailMove = GetComponentInParent<SnailMove>();
//        cinematics = FindObjectOfType<CinematicsHandler>();

//        if (autoGoal)
//        {
//            AutoGoodieGoal();
//        }
//    }

//    public void AddToGoodies(GameObject goodieCollected)
//    {
//        collectedGoodies.Add(goodieCollected);
//        Debug.Log(collectedGoodies.Count.ToString() + " collected!");

//        if (collectedGoodies.Count >= goodieGoal)
//        {
//            cinematics.TheEnd();
//        }
//    }

//    private void AutoGoodieGoal()
//    {
//        GameObject[] allTheLeaves = GameObject.FindGameObjectsWithTag("Leaf");
//        goodieGoal = allTheLeaves.Length;
//    }
//}
